namespace Domain.Entities.Base;

public interface IDeleteEntity
{
    Boolean IsDeleted { get; set; }
}   
   
public interface IDeleteEntity<TKey> : IDeleteEntity, IEntityBase<TKey>   
{   
}   