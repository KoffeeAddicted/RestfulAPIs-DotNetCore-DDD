using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IUserRepository
{

    Task<User?>GetUser(String ProviderToken);

    void Insert(User user);
    Task UpdateAsync(User user);
    void Delete(User user);
}