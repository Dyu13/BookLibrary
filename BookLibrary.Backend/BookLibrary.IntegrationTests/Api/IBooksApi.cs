using BookLibrary.Application.Books.Dtos;
using Refit;

namespace BookLibrary.IntegrationTests.Api;

[Headers("Content-Type: application/json")] // TODO: add API key and maybe jwt token
public interface IBooksApi
{
    [Get("/api/books")]
    public Task<List<BookDto>> GetAllBooks();
}
