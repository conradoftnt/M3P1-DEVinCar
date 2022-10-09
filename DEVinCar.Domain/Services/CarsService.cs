using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Exceptions;

namespace DEVinCar.Domain.Services;

public class CarsService : ICarsService
{
    private readonly ICarRepository _repository;

    public CarsService(ICarRepository repository)
    {
        _repository = repository;
    }

    public Car GetById(int carId)
    {
        Car car = _repository.GetById(carId);

        if (car == null) 
            throw new NotFoundException($"The car with ID: {carId} not found.");

        return car;
    }

    public List<Car> Get(string name, decimal? priceMin, decimal? priceMax)
    {
        IQueryable<Car> carsList = _repository.GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            carsList = carsList.Where(c => c.Name.Contains(name));
        }
        if (priceMin > priceMax)
        {
            throw new BadRequestException($"The min price must be greater than max price!");
        }
        if (priceMin.HasValue)
        {
            carsList = carsList.Where(c => c.SuggestedPrice >= priceMin);
        }
        if (priceMax.HasValue)
        {
            carsList = carsList.Where(c => c.SuggestedPrice <= priceMax);
        }
        if (!carsList.ToList().Any())
        {
            throw new NoContentException("No cars found in database.");
        }

        return carsList.ToList();
    }

    public Car Post(CarDTO body)
    {
        if (body.SuggestedPrice <=0)
        {
            throw new BadRequestException("The suggested price need be most than 0!");
        }

        if (_repository.NameUsed(body.Name))
        {
            throw new BadRequestException("The name already be used!");
        }
        
        var car = new Car
        {
            Name = body.Name,
            SuggestedPrice = body.SuggestedPrice,
        };

        _repository.Insert(car);

        _repository.Save();

        return car;
    }

    public void Delete(int carId)
    {
        
        Car car = _repository.GetById(carId);
        
        if (car == null)
        {
            throw new NotFoundException("The id entered does not belong to any car!");
        }
        if (_repository.IsSaled(carId))
        {
            throw new BadRequestException("The car has a sale attached to it!");
        }

        _repository.Delete(carId);

        _repository.Save();
    }

    public void Put(CarDTO carDto, int carId)
    {
        
        Car car = _repository.GetById(carId);

        if (car.Name != carDto.Name)
            throw new BadRequestException("The entered car name does not match the entered id!");
        if (car == null)
            throw new NotFoundException("No car found with id entered!");
        if (carDto.Name.Equals(null) || carDto.SuggestedPrice.Equals(null))
            throw new BadRequestException("Name and suggest price are required!");
        if (carDto.SuggestedPrice <= 0)
            throw new BadRequestException("The suggested price need be most than 0!");

        car.Name = carDto.Name;
        car.SuggestedPrice = carDto.SuggestedPrice;

        _repository.Update(car);

        _repository.Save();
    }
}
