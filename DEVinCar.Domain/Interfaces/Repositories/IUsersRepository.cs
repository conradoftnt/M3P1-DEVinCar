using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface IUsersRepository
{
    IQueryable<Sale> GetUserBuysById(int userId);

    IQueryable<Sale> GetSalesBySellerId(int sellerId);

    bool CheckEmailIsUsed(string email);

    void InsertSale(Sale sale);
}
