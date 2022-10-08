using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface IAddressesRepository : IBaseRepository<Address, int>
{
    Delivery GetRelation(int id);
}