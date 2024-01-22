using Domain.Entities.Base;

namespace Domain.Entities;

public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>   
{   
    public DateTime CreatedDateTime { get; set; }   
    public string CreatedByName { get; set; }  
    public int CreatedById { get; set; }
    public DateTime? UpdatedTime { get; set; }   
    public string? UpdatedByName { get; set; }   
    public int? UpdateById { get; set; }
}