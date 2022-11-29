using SuperHeroApi.DataAccess.Models;

namespace SuperHeroApi.Repositories
{
    public class UserRepository
    {
        public static List<UserModel> Users = new()
        {
            new()
            {
                Username = "alon_admin",
                Password = "MyPass_w0rd",
                EmailAddress = "alon.admin@email.com",
                GivenName = "Alon",
                Surname = "Kolyakov",
                Role = "Administrator"
            },
            new() { Username = "lydia_standard", EmailAddress = "lydia.standard@email.com", Password = "MyPass_w0rd", GivenName = "Elyse", Surname = "Burton", Role = "Standard" },
        };
    }
}
