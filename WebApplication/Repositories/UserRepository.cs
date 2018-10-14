using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Todo: Move to using a database
        List<IUser> Users { get; set; } = new List<IUser>()
        {
            new User { Id = 1, Username = "bbluemel", GivenName = "Ben", FamilyName = "Bluemel" }
        };
        
        public IUser GetUser(int id)
        {
            return Users.SingleOrDefault(u => u.Id == id);
        }
    }
}