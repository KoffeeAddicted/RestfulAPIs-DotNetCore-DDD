namespace Domain.Entities.Base;

public interface IAuditEntity   
{   
    DateTime CreatedDateTime { get; set; }   
    string CreatedByName { get; set; }  
    int CreatedById { get; set; }
    DateTime? UpdatedTime { get; set; }   
    string? UpdatedByName { get; set; }   
    int? UpdateById { get; set; }
}   
public interface IAuditEntity<TKey> : IAuditEntity, IDeleteEntity<TKey>   
{   
}