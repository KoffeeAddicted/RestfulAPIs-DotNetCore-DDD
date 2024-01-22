using Domain.Entities.Base;

namespace Domain.Entities;

public abstract class DeleteEntity<TKey> : EntityBase<TKey>, IDeleteEntity<TKey>   
{   
    public Boolean IsDeleted { get; set; }   
}   