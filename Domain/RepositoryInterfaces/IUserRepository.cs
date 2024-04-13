using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IUserRepository
{

    Task<User?>GetUser(String ProviderToken);

    void Insert(User user);
    void UpdateAsync(User user);
    void Delete(User user);
}