using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IBannerRepository
{
    Task<Banner?> GetLatest();
    void Add(Banner banner);
}