using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IHistoryRepository
    {
        Task<IEnumerable<History>> getStoriesUserHistory(String ProviderToken);

        void Insert(History history);
        void Update(History history);
        void Delete(IEnumerable<History> history);
    }
}
