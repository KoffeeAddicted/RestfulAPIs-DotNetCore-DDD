using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IUserRepository
{
    void Insert(User user);
    void Update(User user);
    void Delete(User user);
}