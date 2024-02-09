using BookLibrary.Application.Books.Dtos;

namespace BookLibrary.Application.Books;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync(); 
    Task<int> CreateAsync(CreateBookDto createBookDto);
    Task UpdateAsync(int bookId, UpdateBookDto updateBookDto);
    Task DeleteAsync(int id);
}
