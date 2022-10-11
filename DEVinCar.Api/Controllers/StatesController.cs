using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.ViewModels;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/state")]
[Authorize]
public class StatesController : ControllerBase
{
    private readonly IStatesService _service;

    public StatesController(IStatesService service)
    {
        _service = service;
    }

    [HttpPost("{stateId}/city")]
    public ActionResult<int> PostCity(
        [FromRoute] int stateId,
        [FromBody] CityDTO cityDTO
    )
    {
        return Created("api/{stateId}/city", _service.PostCity(stateId, cityDTO));
    }

    [HttpPost("{stateId}/city/{cityId}/address")]
    public ActionResult<int> PostAdress(
        [FromRoute] int stateId,
        [FromRoute] int cityId,
        [FromBody] AdressDTO body)
    {
        return Created($"api/state/{stateId}/city/{cityId}/", _service.PostAdress(stateId, cityId, body));
    }

    [HttpGet("{stateId}/city/{cityId}")]

    public ActionResult<GetCityByIdViewModel> GetCityById
    (
        [FromRoute] int stateId,
        [FromRoute] int cityId
    )
    {

        Tuple<City, State> response = _service.GetCityById(stateId, cityId);

        City city = response.Item1;
        State state = response.Item2;

        GetCityByIdViewModel body = new GetCityByIdViewModel(
            city.Id,
            city.Name,
            state.Id,
            state.Name,
            state.Initials
        );

        return Ok(body);
    }

    [HttpGet("{stateId}")]
    public ActionResult<GetStateByIdViewModel> GetStateById(
            [FromRoute] int stateId
        )
    {
        State state = _service.GetStateById(stateId);

        GetStateByIdViewModel body = new GetStateByIdViewModel(
            state.Id,
            state.Name,
            state.Initials
            );

        return Ok(body);
    }

    [HttpGet]
    public ActionResult<List<GetStateViewModel>> Get([FromQuery] string name)
    {

        List<State> statesList = _service.Get(name);

        List<GetStateViewModel> getStateViewModels = new List<GetStateViewModel>();

        statesList.ForEach(state =>
            {
                GetStateViewModel getState = new GetStateViewModel(state.Id, state.Name, state.Initials);

                state.Cities.ForEach(city =>
                {
                    getState.Cities.Add(city.Name);
                });

                getStateViewModels.Add(getState);
            });

        return Ok(getStateViewModels);
    }

    [HttpGet("{stateId}/city")]
    public ActionResult<GetCityByIdViewModel> GetCityByStateId(
        [FromRoute] int stateId,
        [FromQuery] string name
       )

    {
        IQueryable<City> stateCities = _service.GetCityByStateId(stateId, name);

        List<GetCityByIdViewModel> body = 
            stateCities
               .Select(c => new GetCityByIdViewModel(
                   c.Id,
                   c.Name,
                   c.State.Id,
                   c.State.Name,
                   c.State.Initials))
               .ToList();

        return Ok(body);
    }
}

