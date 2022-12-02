namespace Template.Domain.Entities;

public interface IBaseEntity
{
    Guid Id { get; set; }
}

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}