using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;

namespace DEVinCar.Infra.Data.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly DevInCarDbContext _context;

    public LoginRepository(DevInCarDbContext context)
    {
        _context = context;
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }
}
