using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DEVinCar.Infra.Data.Repositories;

public class AddressesRepository : BaseRepository <Address, int>, IAddressesRepository
{
    public AddressesRepository(DevInCarDbContext context) : base(context)
    {}

    public Delivery GetRelation(int id)
    {
        return _context.Deliveries.FirstOrDefault(d => d.AddressId == id);
    }

    public override List<Address> GetAll()
    {
        return _context.Addresses.Include(a => a.City).ToList();
    }

    public override Address GetById(int id)
    {
        return _context.Addresses.Include(a => a.City).FirstOrDefault(a => a.Id == id);
    }
}