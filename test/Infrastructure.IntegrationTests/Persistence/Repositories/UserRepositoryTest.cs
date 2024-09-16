using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IntegrationTests.Persistence.Repositories;

public class UserRepositoryTest
{
    private AppDbContext DbContext { get; }
    private UserRepository UserRepository { get; }
    private UnitOfWork UnitOfWork { get; }

    public UserRepositoryTest()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        DbContext = new AppDbContext(options);
        DbContext.Database.EnsureCreated();

        UserRepository = new UserRepository(DbContext);
        UnitOfWork = new UnitOfWork(DbContext);
    }

    [Fact]
    public async Task ShouldAddAsyncWhenUserIsValid()
    {
        // Arrange
        var user = new User("John", "john@email.com");

        // Act
        await UserRepository.AddAsync(user);
        await UnitOfWork.CommitAsync();

        // Assert
        var result = await DbContext.Users.FindAsync(user.Id);
        Assert.Equal(user, result);
    }

    [Fact]
    public async Task ShouldThrowDbUpdateExceptionWhenUserEmailIsDuplicated()
    {
        // Arrange
        var user1 = new User("John", "john@email.com");
        var user2 = new User("Johnny", user1.Email);

        // Act
        await UserRepository.AddAsync(user1);
        await UnitOfWork.CommitAsync();

        await UserRepository.AddAsync(user2);
        var act = UnitOfWork.CommitAsync;

        // Assert
        await Assert.ThrowsAsync<DbUpdateException>(act);
    }
}