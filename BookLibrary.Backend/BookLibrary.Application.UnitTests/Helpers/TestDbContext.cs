using Microsoft.EntityFrameworkCore;

using BookLibrary.Application.Interfaces;
using BookLibrary.Domain;

namespace BookLibrary.Application.UnitTests.Helpers;

public class TestDbContext : DbContext, IApplicationDbContext
{
    public TestDbContext(
        DbContextOptions<TestDbContext> options)
        : base(options) { }

    public DbSet<BookEntity> Books { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }
}
