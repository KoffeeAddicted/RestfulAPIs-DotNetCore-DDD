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

    public async Task<User?> GetUser(String ProviderToken)
    {
        return await _genericRepository.Table
            .Where(s => s.ProviderToken == ProviderToken)
            .FirstOrDefaultAsync();
    }

    public void Insert(User user)
    {
        _genericRepository.Insert(user);
    }

    public async Task UpdateAsync(User user)
    {
         await _genericRepository.UpdateAsync(user);
    }

    public void Delete(User user)
    {
        _genericRepository.Delete(user);
    }

}