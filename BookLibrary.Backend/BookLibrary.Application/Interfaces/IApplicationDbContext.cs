using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using BookLibrary.Domain;

namespace BookLibrary.Application.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    int SaveChanges();

    DbSet<BookEntity> Books { get; set; }

    DbSet<AuthorEntity> Authors { get; set; }
}
