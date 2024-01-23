using Domain.Entities.Base;

namespace Domain.Entities;

public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>   
{   
    public DateTime CreatedDateTime { get; set; }   
    public String CreatedByName { get; set; }  
    public Int64 CreatedById { get; set; }
    public DateTime? UpdatedTime { get; set; }   
    public String? UpdatedByName { get; set; }
    public Int64? UpdateById { get; set; }
}