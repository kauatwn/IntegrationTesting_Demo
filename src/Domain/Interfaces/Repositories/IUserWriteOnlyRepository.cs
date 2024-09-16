using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserWriteOnlyRepository
{
    Task AddAsync(User user);
}