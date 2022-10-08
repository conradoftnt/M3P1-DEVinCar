using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Exceptions;

namespace DEVinCar.Domain.Services;

public class SalesService : ISalesService
{
    ISalesRepository _repository;

    public SalesService(ISalesRepository repository)
    {
        _repository = repository;
    }

    public Sale GetItensSale(int saleId)
    {
        Sale sale = _repository.GetById(saleId);

        if (sale == null)
            throw new NotFoundException("No sale found in database!");

        return sale;
    }

    public void PostSale(SaleCarDTO body, int saleId)
    {

        decimal unitPrice;

        if (_repository.GetCarById(body.CarId) == null)
            throw new NotFoundException("No car with entered id found!");

        if (!_repository.HaveSaleId(saleId))
            throw new NotFoundException("No sale with entered id found!");

        if (body.CarId <= 0)
            throw new BadRequestException("Car id invalid!");

        if (body.UnitPrice <= 0 || body.Amount <= 0)
            throw new BadRequestException("Unit price or amount invalid!");


        if (body.UnitPrice == null)
            unitPrice = _repository.GetCarById(body.CarId).SuggestedPrice;

        else
            unitPrice = (decimal)body.UnitPrice;

        if (body.Amount == null)
            body.Amount = 1;

        var saleCar = new SaleCar
        {
            Id = saleId,
            Amount = body.Amount,
            CarId = body.CarId,
            UnitPrice = unitPrice,
            SaleId = saleId
        };

        _repository.InsertSaleCar(saleCar);

        _repository.Save();
    }

    public int PostDeliver(int saleId, DeliveryDTO body)
    {

        if (!_repository.HaveSaleId(saleId))
        {
            throw new NotFoundException("No sale with entred id found!");
        }

        if (!_repository.HaveAddressId(body.AddressId))
        {
            throw new NotFoundException("No address with entred id found!");
        }

        var now = DateTime.Now.Date;
        if (body.DeliveryForecast < now)
        {
            throw new BadRequestException("The delivery forecast entered is invalid!");
        }

        if (body.DeliveryForecast == null)
        {
            body.DeliveryForecast = DateTime.Now.AddDays(7);
        }

        var deliver = new Delivery
        {
            AddressId = (int)body.AddressId,
            SaleId = saleId,
            DeliveryForecast = (DateTime)body.DeliveryForecast
        };

        _repository.InsertDeliver(deliver);

        _repository.Save();

        return deliver.Id;
    }

    public void PatchAmount(int saleId, int carId, int amount)
    {

        if (!_repository.HaveSaleId(saleId))
            throw new NotFoundException("No sale with entred id found!");

        SaleCar car = _repository.GetSaleCarById(carId);
        if (car == null)
            throw new NotFoundException("No car with entred id found!");

        if (amount <= 0)
        {
            throw new BadRequestException("The amount must be greater than 0!");
        }

        car.Amount = amount;

        _repository.UpdateSaleCar(car);

        _repository.Save();
    }

    public void PatchUnitPrice(int saleId, int carId, decimal unitPrice)
    {

        if (!_repository.HaveSaleId(saleId))
            throw new NotFoundException("No sale with entred id found!");

        SaleCar car = _repository.GetSaleCarById(carId);
        if (car == null)
            throw new NotFoundException("No car with entred id found!");

        if (unitPrice <= 0)
        {
            throw new BadRequestException("The unit price must be greater than 0!");
        }

        car.UnitPrice = unitPrice;

        _repository.UpdateSaleCar(car);

        _repository.Save();
    }
}
