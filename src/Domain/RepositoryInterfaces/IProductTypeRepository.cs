using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetAll();
        Task<ProductType> GetById(long id);
        void Insert (ProductType productType);
        void Update (ProductType productType);
        void DeleteById (long id);
    }
}
