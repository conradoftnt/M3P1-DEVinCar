using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;

namespace DEVinCar.Domain.Interfaces.Services;

public interface IUsersService
{
    List<User> Get(string name, DateTime? birthDateMax, DateTime? birthDateMin);
    User GetById(int id);
    List<Sale> GetUserBuysById(int userId);
    List<Sale> GetSalesBySellerId(int sellerId);
    User Post(UserDTO userDto);
    Sale PostSaleUserId(int userId, SaleDTO body);
    Sale PostBuyUserId(int userId, BuyDTO body);
    void Delete(int userId);
}
