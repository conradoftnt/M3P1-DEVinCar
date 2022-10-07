using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DEVinCar.Infra.Data.Repositories;

public class SalesRepository : BaseRepository<Sale, int>, ISalesRepository
{
    public SalesRepository(DevInCarDbContext context) : base(context)
    {}

    public override Sale GetById(int id)
    {
        return _context.Sales
            .Include(s => s.Cars)
            .Include(s => s.UserBuyer)
            .Include(s => s.UserSeller)
            .FirstOrDefault(s => s.Id == id);
    }

    public bool HaveSaleId(int saleId)
    {
        return _context.Sales.Any(s => s.Id == saleId);
    }

    public SaleCar GetSaleCarById(int saleCarId)
    {
        return _context.SaleCars.Find(saleCarId);
    }

    public bool HaveAddressId(int addressId)
    {
        return _context.Addresses.Any(a => a.Id == addressId);
    }

    public Car GetCarById(int carId)
    {
        return _context.Cars.Find(carId);
    }

    public void InsertSaleCar(SaleCar saleCar)
    {
        _context.SaleCars.Add(saleCar);
    }

    public void InsertDeliver(Delivery delivery)
    {
        _context.Deliveries.Add(delivery);
    }

    public void UpdateSaleCar(SaleCar saleCar)
    {
        _context.SaleCars.Update(saleCar);
    }
}
