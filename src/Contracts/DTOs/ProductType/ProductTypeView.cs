using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.ProductType
{
    public class ProductTypeView : EntityBase<long>
    {
        public string? Name { get; set; }
    }
}
