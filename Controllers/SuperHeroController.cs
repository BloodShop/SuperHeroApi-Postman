using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.DataAccess.Data;
using SuperHeroApi.DataAccess.Models;

namespace SuperHeroApi.Controllers
{
    [Route("api/super")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        //static List<SuperHero> mockHeroes = new List<SuperHero>
        //    {
        //        new SuperHero { Id = 1, Name = "Spider Man", FirstName = "Peter", LastName = "Parker", Place = "Arad" },
        //        new SuperHero { Id = 2, Name = "Ironamn", FirstName = "Tony", LastName = "Stark", Place = "Long Island" }
        //    };
        readonly DataContext _ctx;

        public SuperHeroController(DataContext context)
        {
            this._ctx = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get() => Ok(await _ctx.SuperHeroes.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _ctx.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _ctx.SuperHeroes.Add(hero);
            await _ctx.SaveChangesAsync();
            return Ok(await _ctx.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _ctx.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _ctx.SaveChangesAsync();
            return Ok(await _ctx.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await _ctx.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            _ctx.SuperHeroes.Remove(dbHero);
            await _ctx.SaveChangesAsync();
            return Ok(await _ctx.SuperHeroes.ToListAsync());
        }
    }
}
