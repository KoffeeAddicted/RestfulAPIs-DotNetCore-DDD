using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> getStoriesCommentByUser(Int64 StoryId);

       // Task<Comment> addStoryCommentByUser(Int64 StoryId, String ProviderToken, String Message, Int64 Rating);
        void Insert(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}
