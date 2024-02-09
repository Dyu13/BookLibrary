using Microsoft.AspNetCore.Mvc;

using BookLibrary.Application.Books;
using BookLibrary.Application.Books.Dtos;

namespace BookLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService; // TODO: make use of mediatR instead of injecting the service directly and there make use of ILogger
    }

    //TODO: once app is growing, maybe an Api.ViewModels and use FluentValidation (maybe even different models for request/response)
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var books = await _bookService.GetAllAsync();

            return Ok(books);
        }
        catch (Exception ex)
        {
            var exceptionType = ex.GetType();
            if (exceptionType != typeof(ArgumentNullException) &&
                exceptionType != typeof(ArgumentException))
            {
                throw;
            }

            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBookDto createBookDto)
    {
        try
        {
            var bookId = await _bookService.CreateAsync(createBookDto);

            return CreatedAtAction(nameof(CreateAsync), bookId);
        }
        catch (Exception ex)
        {
            var exceptionType = ex.GetType();
            if (exceptionType != typeof(ArgumentNullException) &&
                exceptionType != typeof(ArgumentException))
            {
                throw;
            }

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateBookDto updateBookDto)
    {
        try
        {
            await _bookService.UpdateAsync(id, updateBookDto);

            return Ok();
        }
        catch (Exception ex)
        {
            var exceptionType = ex.GetType();
            if (exceptionType != typeof(ArgumentNullException) &&
                exceptionType != typeof(ArgumentException))
            {
                throw;
            }

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await _bookService.DeleteAsync(id);

            return Ok();
        }
        catch (Exception ex)
        {
            var exceptionType = ex.GetType();
            if (exceptionType != typeof(ArgumentNullException) &&
                exceptionType != typeof(ArgumentException))
            {
                throw;
            }

            return BadRequest(ex.Message);
        }
    }
}
