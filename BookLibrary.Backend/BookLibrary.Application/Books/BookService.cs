using Microsoft.EntityFrameworkCore;
using AutoMapper;

using BookLibrary.Application.Books.Dtos;
using BookLibrary.Application.Interfaces;
using BookLibrary.Domain;

namespace BookLibrary.Application.Books;

public class BookService : IBookService
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public BookService(
        IMapper mapper,
        IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<int> CreateAsync(CreateBookDto createBookDto)
    {
        if (createBookDto == null)
            throw new ArgumentNullException(nameof(createBookDto));

        var bookEntity = _mapper.Map<BookEntity>(createBookDto);

        await _dbContext.Books.AddAsync(bookEntity);
        await _dbContext.SaveChangesAsync();

        return bookEntity.BookId;
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0)
            throw new ArgumentNullException(nameof(id));

        var bookEntity = await _dbContext.Books.FirstOrDefaultAsync(b => b.BookId == id);

        if (bookEntity == null)
            throw new ArgumentNullException(nameof(bookEntity));

        _dbContext.Books.Remove(bookEntity);
        await _dbContext.SaveChangesAsync();
    }

    // TODO: Implement filters
    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await _dbContext.Books
            .AsNoTracking()
            .Include(b => b.Authors)
            .ToListAsync();

        var bookList = _mapper.Map<IEnumerable<BookDto>>(books);

        return bookList;
    }

    public async Task UpdateAsync(int bookId, UpdateBookDto updateBookDto)
    {
        if (updateBookDto == null)
            throw new ArgumentNullException(nameof(updateBookDto));

        if (updateBookDto.BookId == 0 || bookId != updateBookDto.BookId)
            throw new ArgumentNullException(nameof(updateBookDto.BookId));

        var bookEntity = await _dbContext.Books.FirstOrDefaultAsync(b => b.BookId == bookId);        

        if (bookEntity == null)
            throw new ArgumentNullException(nameof(bookEntity));

        _mapper.Map(updateBookDto, bookEntity);

        _dbContext.Books.Update(bookEntity); // This is not necessary, but it's a good practice to show that we are updating the entity
        await _dbContext.SaveChangesAsync();
    }
}
