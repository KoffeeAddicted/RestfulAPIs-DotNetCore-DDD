using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.ProductType
{
    public class ProductTypeCreate
    {
        [Required(ErrorMessage = "Proudct's type name is required")]
        public string ProductName { get; set; } = string.Empty;
    }
}
