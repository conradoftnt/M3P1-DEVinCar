using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Repositories;

namespace DEVinCar.Infra.Data.Repositories;

public class DeliverRepository : BaseRepository<Delivery, int>, IDeliverRepository
{
    public DeliverRepository(DevInCarDbContext context) : base(context)
    {}

    
}