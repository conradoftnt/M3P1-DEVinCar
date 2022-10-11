using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using DEVinCar.Api.Config;
using Microsoft.AspNetCore.Authorization;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/car")]
[Authorize(Roles = "Gerente")]
public class CarController : ControllerBase
{
    private readonly ICarsService _service;
    private readonly CacheService<Car> _cache;

    public CarController(ICarsService service, CacheService<Car> cache)
    {
        _service = service;
        _cache = cache;
    }

    [HttpGet("{carId}")]
    public ActionResult<Car> GetById([FromRoute] int carId)
    {

        Car car;

        if (!_cache.TryGetValue(carId.ToString(), out car))
        {
            car = _service.GetById(carId);
            _cache.Set(carId.ToString(), car);
        }

        return Ok(car);
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
        Car car = _service.Post(body);

        _cache.Set(car.Id.ToString(), car);

        return Created("api/car", car);
    }

    [HttpDelete("{carId}")]
    public ActionResult Delete([FromRoute] int carId)
    {
        _service.Delete(carId);

        _cache.Remove(carId.ToString());

        return NoContent();
    }

    [HttpPut("{carId}")]
    public ActionResult Put([FromBody] CarDTO carDto, [FromRoute] int carId)
    {
        _service.Put(carDto, carId);

        _cache.Remove(carId.ToString());

        return NoContent();
    }
}
