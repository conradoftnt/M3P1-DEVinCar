using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;

namespace DEVinCar.Domain.Interfaces.Services;

public interface ISalesService
{
    Sale GetItensSale(int saleId);
    void PostSale(SaleCarDTO body, int saleId);
    int PostDeliver(int saleId, DeliveryDTO body);
    void PatchAmount(int saleId, int carId, int amount);
    void PatchUnitPrice(int saleId, int carId, decimal unitPrice);
}
