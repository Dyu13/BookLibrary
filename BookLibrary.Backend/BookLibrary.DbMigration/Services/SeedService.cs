using BookLibrary.Application.Interfaces;
using BookLibrary.Domain;

namespace BookLibrary.DbMigration.Services;

public class SeedService : ISeedService
{
    private readonly IApplicationDbContext _dbContext;

    public SeedService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
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

        var book2 = new BookEntity
        {
            Title = "Book 2",
            Description = "Description 2",
            Image = new byte[0],
            Authors = new List<AuthorEntity> { author }
        };
        await _dbContext.Books.AddAsync(book2);

        await _dbContext.SaveChangesAsync();
    }
}
