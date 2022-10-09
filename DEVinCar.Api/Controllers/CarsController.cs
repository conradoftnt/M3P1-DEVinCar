using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/car")]
public class CarController : ControllerBase
{
    private readonly ICarsService _service;

    public CarController(ICarsService service)
    {
        _service = service;
    }

    [HttpGet("{carId}")]
    public ActionResult<Car> GetById([FromRoute] int carId)
    {
        return Ok(_service.GetById(carId));
    }

    [HttpGet]
    public ActionResult<List<Car>> Get(
        [FromQuery] string name,
        [FromQuery] decimal? priceMin,
        [FromQuery] decimal? priceMax
    )
    {
        return Ok(_service.Get(name, priceMin, priceMax));
    }

    [HttpPost]
    public ActionResult<Car> Post(
        [FromBody] CarDTO body
    )
    {
        return Created("api/car", _service.Post(body));
    }

    [HttpDelete("{carId}")]
    public ActionResult Delete([FromRoute] int carId)
    {
        _service.Delete(carId);

        return NoContent();
    }

    [HttpPut("{carId}")]
    public ActionResult Put([FromBody] CarDTO carDto, [FromRoute] int carId)
    {
        _service.Put(carDto, carId);

        return NoContent();
    }
}
