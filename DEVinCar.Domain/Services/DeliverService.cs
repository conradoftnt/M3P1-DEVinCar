using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Exceptions;


namespace DEVinCar.Domain.Services;

public class DeliverService : IDeliverService
{
    private IDeliverRepository _repository;

    public DeliverService(IDeliverRepository repository)
    {
        _repository = repository;
    }

    public List<Delivery> Get(int? addressId, int? saleId)
    {
        
        IQueryable<Delivery> deliveryList = _repository.GetAll().AsQueryable();

        if (addressId.HasValue)
        {
            deliveryList = deliveryList.Where(a => a.AddressId == addressId);
        }

        if (saleId.HasValue)
        {
            deliveryList = deliveryList.Where(s => s.SaleId == saleId);
        }

        if (!deliveryList.ToList().Any())
        {
            throw new NoContentException("No delivery found in database!");
        }

        return deliveryList.ToList();
    }
}
