using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories;

public interface ILoginRepository
{
    User GetUserByEmail(string email);
}
