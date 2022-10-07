namespace DEVinCar.Domain.Interfaces.Repositories;

public interface ICarRepository
{
    bool IsSaled(int id);

    bool NameUsed(string name);
}