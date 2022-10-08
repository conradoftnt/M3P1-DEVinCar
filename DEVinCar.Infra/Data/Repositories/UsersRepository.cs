using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Repositories;

namespace DEVinCar.Infra.Data.Repositories;

public class UsersRepository : BaseRepository<User, int>, IUsersRepository
{
    public UsersRepository(DevInCarDbContext context) : base(context)
    {}

    public IQueryable<Sale> GetUserBuysById(int userId)
    {
        return _context.Sales.Where(s => s.BuyerId == userId);
    }

    public IQueryable<Sale> GetSalesBySellerId(int sellerId)
    {
        return _context.Sales.Where(s => s.SellerId == sellerId);
    }

    public bool CheckEmailIsUsed(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }

    public void InsertSale(Sale sale)
    {
        _context.Sales.Add(sale);
    }
}
