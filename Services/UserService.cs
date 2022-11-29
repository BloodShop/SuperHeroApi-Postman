using SuperHeroApi.DataAccess.Models;
using SuperHeroApi.Repositories;

namespace SuperHeroApi.Services
{
    public class UserService : IUserService
    {
        public UserModel Get(UserLogin userLogin)
        {
            UserModel user = UserRepository.Users.FirstOrDefault(o => o.Username.Equals(userLogin.Username, StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userLogin.Password));

            return user;
        }
    }
}
