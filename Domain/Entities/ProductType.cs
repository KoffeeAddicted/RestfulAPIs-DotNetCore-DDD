using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public sealed class ProductType : DeleteEntity<Int64>
{
    public required String Name { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}