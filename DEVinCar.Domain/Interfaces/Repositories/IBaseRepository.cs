namespace DEVinCar.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity, TKey>
{
    TEntity GetById(TKey id);
    List<TEntity> GetAll();
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TKey id);
    void Save();
}
