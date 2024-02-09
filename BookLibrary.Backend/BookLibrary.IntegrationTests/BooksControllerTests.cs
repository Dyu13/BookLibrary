using Microsoft.EntityFrameworkCore;
using Refit;

using BookLibrary.Infrastructure.Persistence;
using BookLibrary.IntegrationTests.Api;
using BookLibrary.Domain;

namespace BookLibrary.IntegrationTests
{
    public class BooksControllerTests
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IBooksApi _booksApi;

        public BooksControllerTests()
        {
            var options = new DbContextOptionsBuilder<BookLibraryDbContext>()
                .UseSqlServer("Server=localhost,1434;Initial Catalog=BookLibrary;User Id=SA;Password=TestDb2024;Trust Server Certificate=true")
                .Options;
            
            _dbContext = new BookLibraryDbContext(options);

            _booksApi = RestService.For<IBooksApi>("http://localhost:15100");
        }

        [Fact]
        public async Task GetAll_Books_List()
        {
            // Arrange
            var expectedMinBooksCount = 1;

            // TODO: add more test data

            // Act
            var response = await _booksApi.GetAllBooks();

            // Assert
            Assert.NotNull(response);
            Assert.True(expectedMinBooksCount <= response?.Count);
        }
    }
}