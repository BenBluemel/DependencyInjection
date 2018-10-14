using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IUserRepository
    {
        IUser GetUser(int id);
    }
}
