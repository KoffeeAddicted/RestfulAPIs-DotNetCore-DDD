using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.Product
{
    public class ProductCreate
    {
        [Required(ErrorMessage = "Product name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(0, int.MaxValue)]
        public decimal ProductPrice { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Producttype is required")]
        public long ProductTypeId { get; set; }
    }
}
