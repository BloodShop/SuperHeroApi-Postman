using System.ComponentModel.DataAnnotations;

namespace SuperHeroApi.DataAccess.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
