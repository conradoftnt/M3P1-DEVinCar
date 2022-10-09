using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;

namespace DEVinCar.Domain.Interfaces.Services;

public interface IDeliverService
{
    List<Delivery> Get(int? addressId, int? saleId);
}