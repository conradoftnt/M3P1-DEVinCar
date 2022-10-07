using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface ISalesRepository
{

    bool HaveSaleId(int saleId);

    SaleCar GetSaleCarById(int saleCarId);

    bool HaveAddressId(int addressId);

    Car GetCarById(int carId);

    void InsertSaleCar(SaleCar saleCar);

    void InsertDeliver(Delivery delivery);

    void UpdateSaleCar(SaleCar saleCar);
}