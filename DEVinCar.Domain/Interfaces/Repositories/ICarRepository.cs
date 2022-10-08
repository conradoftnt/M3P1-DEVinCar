using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface ICarRepository : IBaseRepository<Car, int>
{
    bool IsSaled(int id);

    bool NameUsed(string name);
}