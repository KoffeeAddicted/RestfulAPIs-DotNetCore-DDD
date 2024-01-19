using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class ProductTypeNotFoundException : NotFoundException
    {
        public ProductTypeNotFoundException(long id)
        : base($"The product type with the id {id} was not found.")
        {
        }
    }
}
