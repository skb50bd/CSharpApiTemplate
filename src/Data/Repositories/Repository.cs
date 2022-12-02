using Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Template.Data.Repositories;

public class Repository<T> : IRepository<T> where T: class, IBaseEntity
{
    protected AppDbContext DbCtx;
    protected DbSet<T> Set => DbCtx.Set<T>();
    public IQueryable<T> Table => Set;

    public Repository(AppDbContext dbCtx)
    {
        DbCtx = dbCtx;
    }

    public virtual async Task<T?> GetById(Guid id) => 
        await Table.FirstOrDefaultAsync(x => x.Id == id);

    public virtual async Task<List<T>> GetAll() => 
        await Table.ToListAsync();

    public virtual async Task Add(T item, bool saveChanges = true)
    {
        await Set.AddAsync(item);

        if (saveChanges)
        {
            await DbCtx.SaveChangesAsync();
        }
    }

    public virtual async Task Update(T item, bool saveChanges = true)
    {
        Set.Update(item);

        if (saveChanges)
        {
            await DbCtx.SaveChangesAsync();
        }
    }

    public virtual async Task Delete(Guid id, bool saveChanges = true)
    {
        var item = await Table.FirstAsync(i => i.Id == id);
        Set.Remove(item);

        if (saveChanges)
        {
            await DbCtx.SaveChangesAsync();
        }
    }
}