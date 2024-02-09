using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using Moq;
using AutoMapper;

using BookLibrary.Application.Books;
using BookLibrary.Application.Books.Dtos;
using BookLibrary.Application.Interfaces;
using BookLibrary.Application.UnitTests.Helpers;
using BookLibrary.Domain;

namespace BookLibrary.Application.UnitTests
{
    [Collection("SequentialDb")] // For future test classes that make use of the db contextg
    public class BookServiceTests
    {
        private readonly IApplicationDbContext _dbContext;

        public BookServiceTests()
        {
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase($"Test-{Guid.NewGuid}");
            _dbContext = new TestDbContext(builder.Options);

            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllAsync_GetBooks_List()
        {
            // Arrange
            var author = new AuthorEntity { Name = "Author 1" };
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            var book = new BookEntity 
            { 
                Title = "Book 1",
                Description = "Description 1", 
                Image = new byte[0], 
                Authors = new List<AuthorEntity> { author } 
            };
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<BookDto, BookEntity>()
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors == null ? new List<string>() : src.Authors.Select(a => a.Name))));
            var mapper = config.CreateMapper();

            var sut = new BookService(mapper, _dbContext);

            // Act
            var result = sut.GetAllAsync().Result;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public async Task CreateAsync_NullDto_ArgumentNullException()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;

            var sut = new BookService(mapper, _dbContext);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateAsync(null));
        }

        [Fact]
        public async Task CreateAsync_CreateBook_BookId()
        {
            // Arrange
            var author = new AuthorEntity { Name = "Author 1" };
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<CreateBookDto, BookEntity>()
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors == null ? new List<string>() : src.Authors.Select(a => a.Name))));
            var mapper = config.CreateMapper();

            var sut = new BookService(mapper, _dbContext);

            var createBookDto = new CreateBookDto
            {
                Title = "Book 1",
                Description = "Description 1",
                Image = new byte[0],
                Authors = new List<int> { author.AuthorId }
            };

            // Act
            var result = sut.CreateAsync(createBookDto).Result;

            // Assert
            Assert.True(result > 0);
        }

        [Theory]
        [InlineData(1, "null")]
        [InlineData(1, "{ \"BookId\": 2, \"Title\": \"Title\", \"Description\": \"Description\", \"Image\": \"iVBORw0K\" }")]
        [InlineData(1, "{ \"BookId\": 1, \"Title\": \"Title\", \"Description\": \"Description\", \"Image\": \"iVBORw0K\" }")]
        [InlineData(0, "{ \"BookId\": 2, \"Title\": \"Title\", \"Description\": \"Description\", \"Image\": \"iVBORw0K\" }")]
        public async Task UpdateAsync_InvalidDto_ArgumentNullException(int bookId, string json)
        {
            // Arrange
            var updateBookDto = JsonSerializer.Deserialize<UpdateBookDto>(json);
            
            var mapper = new Mock<IMapper>().Object;

            var sut = new BookService(mapper, _dbContext);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.UpdateAsync(bookId, updateBookDto));
        }

        [Fact]
        public async Task UpdateAsync_UpdateBook_Success()
        {
            // Arrange
            var author = new AuthorEntity { Name = "Author 1" };
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            var book = new BookEntity
            {
                Title = "Book 1",
                Description = "Description 1",
                Image = new byte[0],
                Authors = new List<AuthorEntity> { author }
            };
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<UpdateBookDto, BookEntity>()
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ReverseMap());
            var mapper = config.CreateMapper();

            var updateBookDto = new UpdateBookDto
            {
                BookId = book.BookId,
                Title = "Book 2",
                Description = "Description 2",
                Image = new byte[0]
            };

            var sut = new BookService(mapper, _dbContext);

            // Act
            await sut.UpdateAsync(book.BookId, updateBookDto);

            // Assert
            var updatedBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.BookId == book.BookId);
            Assert.NotNull(updatedBook);
            Assert.Equal(updateBookDto.Title, updatedBook.Title);
            Assert.Equal(updateBookDto.Description, updatedBook.Description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task DeleteAsync_InvalidId_ArgumentNullException(int id)
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;

            var sut = new BookService(mapper, _dbContext);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.DeleteAsync(id));
        }

        [Fact]
        public async Task DeleteAsync_DeleteBook_Success()
        {
            // Arrange
            var author = new AuthorEntity { Name = "Author 1" };
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            var book = new BookEntity
            {
                Title = "Book 1",
                Description = "Description 1",
                Image = new byte[0],
                Authors = new List<AuthorEntity> { author }
            };
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            var mapper = new Mock<IMapper>().Object;

            var sut = new BookService(mapper, _dbContext);

            // Act
            await sut.DeleteAsync(book.BookId);

            // Assert
            var deletedBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.BookId == book.BookId);
            Assert.Null(deletedBook);
        }
    }
}