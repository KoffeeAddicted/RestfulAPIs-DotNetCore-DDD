using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default);
        Task<Product> GetById(long id, CancellationToken cancellationToken = default);
        void Insert (Product product);
        void Update (Product product);
        void Delete (long id);
    }
}
