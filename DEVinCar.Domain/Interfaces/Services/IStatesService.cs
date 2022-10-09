using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;

namespace DEVinCar.Domain.Interfaces.Services;

public interface IStatesService
{
    int PostCity(int stateId, CityDTO cityDTO);
    int PostAdress(int stateId, int cityId, AdressDTO body);
    Tuple<City, State> GetCityById(int stateId, int cityId);
    State GetStateById(int stateId);
    List<State> Get(string name);
    IQueryable<City> GetCityByStateId(int stateId, string name);
}
