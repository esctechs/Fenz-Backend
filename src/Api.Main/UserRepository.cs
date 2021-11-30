using System.Collections.Generic;
using System.Linq;
using Fenz.Application;

namespace Api.Main;

public static class UserRepository
{
    public static User Get(string username, string password)
    {
        var users = new List<User>
        {
            new User { Id = 1, Username = "batman", Password = "batman", Role = "manager" },
            new User { Id = 2, Username = "robin", Password = "robin", Role = "employee" }
        };
        return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
    }
}
