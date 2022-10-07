using DEVinCar.Domain.Interfaces.Repositories;

namespace DEVinCar.Infra.Data.Repositories;

public class BaseRepository <TEntity, TKey> : IBaseRepository <TEntity, TKey> where TEntity : class 
{
    protected readonly DevInCarDbContext _context;

    public BaseRepository(DevInCarDbContext context)
    {
        _context = context;
    }

    public virtual TEntity GetById(TKey id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public virtual List<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public virtual void Insert(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public virtual void Delete(TKey id)
    {
        TEntity entity = GetById(id);
        _context.Set<TEntity>().Remove(entity);
    }

    public virtual void Save()
    {
        _context.SaveChanges();
    }
}