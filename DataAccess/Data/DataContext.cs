using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SuperHeroApi.DataAccess.Models;

namespace SuperHeroApi.DataAccess.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes => Set<SuperHero>();
    }
}
