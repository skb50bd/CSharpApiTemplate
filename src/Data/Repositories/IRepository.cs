using Template.Domain.Entities;

namespace Template.Data.Repositories;

public interface IRepository<T> where T: IBaseEntity
{
    IQueryable<T> Table { get; }
    Task<T?> GetById(Guid id);
    Task<List<T>> GetAll();
    Task Add(T item, bool saveChanges = true);
    Task Update(T item, bool saveChanges = true);
    Task Delete(Guid id, bool saveChanges = true);
}