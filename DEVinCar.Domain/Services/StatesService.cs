using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Exceptions;

namespace DEVinCar.Domain.Services;

public class StatesService : IStatesService
{
    private readonly IStatesRepository _repository;

    public StatesService(IStatesRepository repository)
    {
        _repository = repository;
    }

    public int PostCity(int stateId, CityDTO cityDTO)
    {
        
        var state = _repository.GetById(stateId);

        if (state == null)
        {
            throw new NotFoundException("State id not found!");
        }

        if (_repository.HaveCityInState(state.Id, cityDTO.Name))
        {
            throw new BadRequestException("The city name already exist in this state id!");
        }

        City city = new City
        {
            Name = cityDTO.Name,
            StateId = stateId,
        };

        City cityResponse = _repository.InsertCity(city);

        _repository.Save();

        return cityResponse.Id;
    }

    public int PostAdress(int stateId, int cityId, AdressDTO body)
    {
        
        State state = _repository.GetById(stateId);
        City city = _repository.GetCityById(cityId);

        if (state == null)
            throw new NotFoundException("State id not found!");

        if (city == null)
            throw new NotFoundException("City id not found!");

        if (city.StateId != state.Id)
            throw new BadRequestException("The city id not included in this state id!");

        var address = new Address
        {
            CityId = cityId,
            Street = body.Street,
            Number = body.Number,
            Cep = body.Cep,
            Complement = body.Complement,
            City = city

        };

        Address addressResponse = _repository.InsertAddress(address);

        _repository.Save();

        return addressResponse.Id;
    }

    public Tuple<City, State> GetCityById(int stateId, int cityId)
    {
        
        State state = _repository.GetById(stateId);
        City city = _repository.GetCityById(cityId);

        if (state == null)
        {
            throw new NotFoundException("State id not found!");
        }

        if (city == null)
        {
            throw new NotFoundException("City id not found!");
        }

        if (city.StateId != state.Id)
        {
            throw new BadRequestException("The city id not included in this state id!");
        }

        return new Tuple<City, State>(city, state);
    }

    public State GetStateById(int stateId)
    {
        
        State state = _repository.GetById(stateId);

        if (state == null)
        {
            throw new NotFoundException("State id not found!");
        }

        return state;
    }

    public List<State> Get(string name)
    {
        
        IQueryable<State> statesList = _repository.GetAll().AsQueryable();

        if(!string.IsNullOrEmpty(name)) {
            statesList = statesList.Where(s => s.Name.ToUpper().Contains(name.ToUpper()));
        }
        if(!statesList.Any()) {
            throw new NoContentException("No one state found in database!"); 
        }

        return statesList.ToList();
    }

    public IQueryable<City> GetCityByStateId(int stateId, string name)
    {
        
        State state = _repository.GetById(stateId);

        IQueryable<City> stateCities = _repository.GetCitiesWithStateId(stateId);
        
        if (state == null)
            throw new NotFoundException("State id not found!");

        if (!stateCities.Any())
        {
            throw new NoContentException("No one city with state id found in database!"); 
        }
        
        if (!string.IsNullOrEmpty(name))
        {
            var stateCitiesQuery = stateCities.Where(c => c.Name.ToUpper().Contains(name.ToUpper()));

            if (stateCitiesQuery.Count() == 0)
            {
                throw new NoContentException("No one city found with entred name!"); 
            }

            return stateCitiesQuery;
        }

        return stateCities;
    }
}
