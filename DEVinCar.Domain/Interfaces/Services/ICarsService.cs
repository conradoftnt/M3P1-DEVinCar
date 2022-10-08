using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;

namespace DEVinCar.Domain.Interfaces.Services;

public interface ICarsService
{
    Car GetById(int carId);

    List<Car> Get(string name, decimal? priceMin, decimal? priceMax);

    Car Post(CarDTO body);

    void Delete(int carId);

    void Put(CarDTO carDto, int carId);
}
