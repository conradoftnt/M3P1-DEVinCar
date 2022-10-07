using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Repositories;

namespace DEVinCar.Infra.Data.Repositories;

public class CarRepository : BaseRepository <Car, int>, ICarRepository
{
    public CarRepository(DevInCarDbContext context) : base(context)
    {        
    }

    public bool NameUsed(string name)
    {
        return _context.Cars.Any(c => c.Name == name);
    }

    public bool IsSaled(int id)
    {
        return _context.SaleCars.Any(s => s.CarId == id);
    }
}