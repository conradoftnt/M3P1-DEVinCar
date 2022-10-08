using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Repositories;

namespace DEVinCar.Infra.Data.Repositories;

public class StatesRepository : BaseRepository<State, int>, IStatesRepository
{
    public StatesRepository(DevInCarDbContext context) : base(context)
    {}

    public bool HaveCityInState(int stateId, string cityName)
    {
        return _context.Cities.Any(c => c.StateId == stateId && c.Name == cityName);
    }

    public void InsertCity(City city)
    {
        _context.Cities.Add(city);
    }

    public City GetCityById(int cityId)
    {
        return _context.Cities.Find(cityId);
    }

    public void InsertAddress(Address address)
    {
        _context.Addresses.Add(address);
    }

    public IQueryable<City> GetCitiesWithStateId(int stateId)
    {
        return _context.Cities.Where(c => c.StateId == stateId);
    }
}