using FluentValidation;
using SuperHeroApi.Filters;
using SuperHeroApi.Validators;
using SuperHeroApi.Extensions;
using SuperHeroApi.DataAccess.Data;
using SuperHeroApi.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using SuperHeroApi.DataAccess.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.EndPoints
{
    public static class SuperHeroesEndpoints
    {
        public static void MapSuperHeroEndpoints(this WebApplication app)
        {
            app.MapGet("/superheroes/list", List).Produces<List<SuperHero>>(statusCode: 200, contentType: "application/json");
            app.MapGet("/superheroes/get/{id}", Get).Produces<SuperHero>();
            app.MapPost("/superheroes/create", Create).AddEndpointFilter(async (ctx, next) => await Validate(ctx, next))
                .Accepts<SuperHero>("application/json")
                .Produces<SuperHero>(statusCode: 200, contentType: "application/json");
            app.MapPut("/superheroes/update", Update).AddEndpointFilter(async (ctx, next) => await Validate(ctx, next))
                  .Accepts<SuperHero>("application/json")
                  .Produces<SuperHero>(statusCode: 200, contentType: "application/json");
            app.MapDelete("/superheroes/delete/{id}", Delete);
        }

        static async ValueTask<object> Validate(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = (IValidator<SuperHero>)context?.Arguments[1];
            var superHero = (SuperHero)context?.Arguments[2];

            var result = await validator.ValidateAsync(superHero);
            if (!result.IsValid)
            {
                var errors = string.Join(' ', result.Errors);
                return Results.Problem(errors);
            }
            return await next(context);
        }

        public static async Task<IResult> List(DataContext db, IMapper mapper)
        {
            var result = await db.SuperHeroes.ToListAsync();
            return Results.Ok(result.Select(hero => mapper.Map<SuperHeroDto>(hero)));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]
        public static async Task<IResult> Get(DataContext db, int id) =>
            await db.SuperHeroes.FindAsync(id) is SuperHero superHero
                ? Results.Ok(superHero)
                : Results.NotFound();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public static async Task<IResult> Create(DataContext db, SuperHeroDto newHero, IValidator<SuperHero> validator, IMapper mapper)
        {
            var hero = mapper.Map<SuperHero>(newHero);
            db.SuperHeroes.Add(hero);
            await db.SaveChangesAsync();

            return Results.Created($"/superheroes/{hero.Id}", hero);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public static async Task<IResult> Update(DataContext db, SuperHero updateSuperHero, IValidator<SuperHero> validator)
        {
            var superHero = await db.SuperHeroes.FindAsync(updateSuperHero.Id);

            if (superHero is null) return Results.NotFound();

            superHero.Name = updateSuperHero.Name;
            superHero.FirstName = updateSuperHero.FirstName;
            superHero.LastName = updateSuperHero.LastName;
            superHero.Place = updateSuperHero.Place;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public static async Task<IResult> Delete(DataContext db, int id)
        {
            if (await db.SuperHeroes.FindAsync(id) is SuperHero superHero)
            {
                db.SuperHeroes.Remove(superHero);
                await db.SaveChangesAsync();
                return Results.Ok(superHero);
            }

            return Results.NotFound();
        }
    }

}
