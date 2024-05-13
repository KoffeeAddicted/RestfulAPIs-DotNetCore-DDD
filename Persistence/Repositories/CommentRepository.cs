using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly IGenericRepository<Comment> _genericRepository;

    public CommentRepository(IGenericRepository<Comment> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IEnumerable<Comment>> getStoriesCommentByUser(Int64 StoryId)
    {
        return await _genericRepository.Table
            .Include(x => x.User)
            .Where(s => s.StoryId == StoryId)
            .ToListAsync();
    }


    public void Insert(Comment comment)
    {
        _genericRepository.Insert(comment);
    }

    public void Update(Comment comment)
    {
        _genericRepository.Update(comment);
    }

    public void Delete(Comment comment)
    {
        _genericRepository.Delete(comment);
    }

}