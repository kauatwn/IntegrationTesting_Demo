using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserWriteOnlyRepository
{
    private AppDbContext DbContext { get; } = dbContext;

    public async Task AddAsync(User user)
    {
        await DbContext.Users.AddAsync(user);
    }
}