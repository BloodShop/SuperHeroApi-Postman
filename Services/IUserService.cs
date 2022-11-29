using SuperHeroApi.DataAccess.Models;

namespace SuperHeroApi.Services
{
    public interface IUserService
    {
        public UserModel Get(UserLogin userLogin);
    }
}
