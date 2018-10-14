using WebApplication.Models;

namespace WebApplication.Managers
{
    public interface IUserManager
    {
        IUser GetUser(int userId);
    }
}