using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface IUsersRepository : IBaseRepository<User, int>
{
    IQueryable<Sale> GetUserBuysById(int userId);

    IQueryable<Sale> GetSalesBySellerId(int sellerId);

    bool CheckEmailIsUsed(string email);

    void InsertSale(Sale sale);
}
