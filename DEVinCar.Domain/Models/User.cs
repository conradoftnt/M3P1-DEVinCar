using DEVinCar.Domain.Enuns;

namespace DEVinCar.Domain.Models
{
    public class User
    {
        public int Id {get; set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Roles Role { get; set; }

        public User()
        {
            
        }
        public User(int id, string email, string password, string name, DateTime birthDate, Roles role)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            BirthDate = birthDate;
            Role = role;
        }
    }
}