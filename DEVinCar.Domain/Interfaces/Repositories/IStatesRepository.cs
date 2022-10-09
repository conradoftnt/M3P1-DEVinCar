using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface IStatesRepository : IBaseRepository<State, int>
{
    bool HaveCityInState(int stateId, string cityName);

    City InsertCity(City city);

    City GetCityById(int cityId);

    Address InsertAddress(Address address);

    IQueryable<City> GetCitiesWithStateId(int stateId);
}
