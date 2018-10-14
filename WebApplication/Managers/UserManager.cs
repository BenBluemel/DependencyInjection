using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Managers
{
    public class UserManager : IUserManager
    {
        protected IUserRepository UserRepository { get; set; }
        public UserManager(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public IUser GetUser(int userId)
        {
            return UserRepository.GetUser(userId);
        }
    }
}