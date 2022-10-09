using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Exceptions;

namespace DEVinCar.Domain.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repository;

    public UsersService(IUsersRepository repository)
    {
        _repository = repository;
    }

    public List<User> Get(string name, DateTime? birthDateMax, DateTime? birthDateMin)
    {

        IQueryable<User> usersList = _repository.GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            usersList = usersList.Where(c => c.Name.Contains(name));
        }

        if (birthDateMin.HasValue)
        {
            usersList = usersList.Where(c => c.BirthDate >= birthDateMin.Value);
        }

        if (birthDateMax.HasValue)
        {
            usersList = usersList.Where(c => c.BirthDate <= birthDateMax.Value);
        }

        if (!usersList.ToList().Any())
        {
            throw new NoContentException("No one user found in database!");
        }

        return usersList.ToList();
    }

    public User GetById(int id)
    {
        User user = _repository.GetById(id);
        if (user == null)
            throw new NotFoundException("User id not found!");

        return user;
    }

    public List<Sale> GetUserBuysById(int userId)
    {
        List<Sale> sales = _repository.GetUserBuysById(userId).ToList();

        if (sales == null || sales.Count() == 0)
        {
            throw new NoContentException("No one sale found in this user id!");
        }

        return sales;
    }

    public List<Sale> GetSalesBySellerId(int sellerId)
    {
        
        List<Sale> sales = _repository.GetSalesBySellerId(sellerId).ToList();

        if (sales == null || sales.Count() == 0)
        {
            throw new NoContentException("No one sale found in this seller id!");
        }

        return sales;
    }

    public User Post(UserDTO userDto)
    {
        if (_repository.CheckEmailIsUsed(userDto.Email))
            throw new BadRequestException("This email already be used!");

        User newUser = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            BirthDate = userDto.BirthDate
        };

        _repository.Insert(newUser);

        _repository.Save();

        return newUser;
    }

    public Sale PostSaleUserId(int userId, SaleDTO body)
    {
        
        if (body.BuyerId <= 0)
            throw new BadRequestException("Buyer id invalid!");

        User user = _repository.GetById(userId);

        if (user == null)
            throw new NotFoundException("User id not found!");

        User buyer = _repository.GetById(body.BuyerId);

        if (buyer == null)
            throw new NotFoundException("Buyer id not found!");

        if (body.SaleDate == null)
            body.SaleDate = DateTime.Now;

        Sale sale = new Sale
        {
            BuyerId = body.BuyerId,
            SellerId = userId,
            SaleDate = body.SaleDate,
        };

        _repository.InsertSale(sale);

        _repository.Save();

        return sale;
    }

    public Sale PostBuyUserId(int userId, BuyDTO body)
    {
        User user = _repository.GetById(userId);

        if (user == null)
            throw new NotFoundException("User id not found!");

        var seller = _repository.GetById(body.SellerId);

        if (seller == null)
            throw new NotFoundException("Seller id not found!");

        if (body.SaleDate == null)
        {
            body.SaleDate = DateTime.Now;
        }

        var buy = new Sale
        {
            BuyerId = userId,
            SellerId = body.SellerId,
            SaleDate = body.SaleDate,
        };

        _repository.InsertSale(buy);

        _repository.Save();

        return buy;
    }

    public void Delete(int userId)
    {
        User user = _repository.GetById(userId);

        if (user == null)
            throw new NotFoundException("User id not found!");

        _repository.Delete(userId);

        _repository.Save();
    }
}
