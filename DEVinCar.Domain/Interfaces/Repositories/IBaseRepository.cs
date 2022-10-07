namespace DEVinCar.Domain.Interfaces.Repositories;

public interface IBaseRepository <TEntity, TKey> where TEntity : class
{
    TEntity GetById(TKey id);

    List<TEntity> GetAll();

    void Insert(TEntity entity);

    void Delete(TKey id);

    void Save();
}