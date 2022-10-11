using DEVinCar.Domain.Exceptions;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Services;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _repository;

    public LoginService(ILoginRepository repository)
    {
        _repository = repository;
    }

    public User CheckLogin(string email, string password)
    {
        User user = _repository.GetUserByEmail(email);

        if (user == null)
        {
            throw new BadRequestException($"The email '{email}' is not being using!");
        }

        if (user.Password != password)
        {
            throw new BadRequestException($"The password is incorrect!");
        }

        return user;
    }
}