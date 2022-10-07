using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface IAddressesRepository
{
    Delivery GetRelation(int id);
}