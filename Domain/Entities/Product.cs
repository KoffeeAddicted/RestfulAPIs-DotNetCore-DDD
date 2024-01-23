namespace Domain.Entities;

public sealed class Product : AuditEntity<Int64>
{
    public required String Name { get; set; }
    public required Decimal Price { get; set; }
    public required Int64 ProductTypeId { get; set; }
    public required ProductType ProductType { get; set; }
}