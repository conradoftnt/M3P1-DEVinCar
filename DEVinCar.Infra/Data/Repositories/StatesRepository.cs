using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DEVinCar.Infra.Data.Repositories;

public class StatesRepository : BaseRepository<State, int>, IStatesRepository
{
    public StatesRepository(DevInCarDbContext context) : base(context)
    {}

    public bool HaveCityInState(int stateId, string cityName)
    {
        return _context.Cities.Any(c => c.StateId == stateId && c.Name == cityName);
    }

    public City InsertCity(City city)
    {
        _context.Cities.Add(city);
        return city;
    }

    public City GetCityById(int cityId)
    {
        return _context.Cities.Find(cityId);
    }

    public Address InsertAddress(Address address)
    {
        _context.Addresses.Add(address);
        return address;
    }

    public IQueryable<City> GetCitiesWithStateId(int stateId)
    {
        return _context.Cities.Where(c => c.StateId == stateId);
    }

    public override List<State> GetAll()
    {
        return _context.States.Include(s => s.Cities).ToList();
    }
}