using Microsoft.EntityFrameworkCore;

using BookLibrary.Application.Interfaces;
using BookLibrary.Domain;

namespace BookLibrary.Infrastructure.Persistence;

public class BookLibraryDbContext : DbContext, IApplicationDbContext
{
    // Constructor with no arguments is required and it is used when adding/removing migrations from Infrastructure
    public BookLibraryDbContext()
    {
        
    }

    public BookLibraryDbContext(
        DbContextOptions<BookLibraryDbContext> options)
        : base(options)
    {
        // Database.Migrate();
    }

    // It is required to override this method when adding/removing migrations from Infrastructure
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer();
        }
    }

    public DbSet<BookEntity> Books { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }
}
