using Domain.Interfaces;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    private AppDbContext DbContext { get; } = dbContext;

    public async Task CommitAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}