using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IGenericRepository<User> _genericRepository;

    public UserRepository(IGenericRepository<User> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<User> GetUser(String ProviderToken)
    {
        return await _genericRepository.Table
            .FirstOrDefaultAsync(s => s.ProviderToken == ProviderToken);
    }

    public void Insert(User user)
    {
        _genericRepository.Insert(user);
    }

    public void Update(User user)
    {
        _genericRepository.Update(user);
    }

    public void Delete(User user)
    {
        _genericRepository.Delete(user);
    }

}