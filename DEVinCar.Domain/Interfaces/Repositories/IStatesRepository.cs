using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface IStatesRepository
{
    bool HaveCityInState(int stateId, string cityName);

    void InsertCity(City city);

    City GetCityById(int cityId);

    void InsertAddress(Address address);

    IQueryable<City> GetCitiesWithStateId(int stateId);
}
