using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Services;

public interface ILoginService
{
    User CheckLogin(string email, string password);
}
