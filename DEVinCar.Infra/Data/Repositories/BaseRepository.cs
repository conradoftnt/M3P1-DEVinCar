using Microsoft.EntityFrameworkCore;
using DEVinCar.Domain.Interfaces.Repositories;

namespace DEVinCar.Infra.Data.Repositories;

public class BaseRepository <TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class 
{
    protected readonly DevInCarDbContext _context;
    protected DbSet<TEntity> table;

    public BaseRepository(DevInCarDbContext context)
    {
        _context = context;
        table = _context.Set<TEntity>();
    }

    public virtual TEntity GetById(TKey id)
    {
        return table.Find(id);
    }

    public virtual List<TEntity> GetAll()
    {
        return table.ToList();
    }

    public virtual void Insert(TEntity entity)
    {
        table.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public virtual void Delete(TKey id)
    {
        TEntity entity = GetById(id);
        table.Remove(entity);
    }

    public virtual void Save()
    {
        _context.SaveChanges();
    }
}