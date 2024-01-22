using Domain.Entities.Base;

namespace Domain.Entities;

public abstract class EntityBase<TKey> : IEntityBase<TKey>   
{   
    public virtual TKey Id { get; set; }   
}   