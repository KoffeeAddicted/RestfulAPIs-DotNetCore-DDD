namespace Domain.Entities.Base;

public interface IEntityBase<TKey>   
{   
    TKey Id { get; set; }   
}   