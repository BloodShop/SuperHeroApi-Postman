# SuperHeroApi - Minimal api using carter

## CarterModule implementation and passing the endpoint to the base

```javascript
public class SuperHeroesEndpoints : CarterModule
{
    public SuperHeroesEndpoints() : base("/superheroes") { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/list", List).Produces<List<SuperHero>>(statusCode: 200, contentType: "application/json");

        app.MapGet("/get/{id}", Get).Produces<SuperHero>();
        app.MapPost("/create", Create).AddEndpointFilter(async (ctx, next) => await Validate(ctx, next))
            .Accepts<SuperHero>("application/json")
            .Produces<SuperHero>(statusCode: 200, contentType: "application/json");
        app.MapPut("/update", Update).AddEndpointFilter(async (ctx, next) => await Validate(ctx, next))
              .Accepts<SuperHero>("application/json")
              .Produces<SuperHero>(statusCode: 200, contentType: "application/json");
        app.MapDelete("/delete/{id}", Delete);
    }
    
    ...

}
```

## Declaration of validtion foreach superHero

```javascript
public class SuperHeroValidator : AbstractValidator<SuperHero> 
{
    public SuperHeroValidator()
    {
        RuleFor(o => o.Name).NotNull().NotEmpty().MinimumLength(3).NotEqual("string");
        RuleFor(o => o.FirstName).NotNull().NotEmpty().MinimumLength(3).NotEqual("string");
        RuleFor(o => o.LastName).NotNull().NotEmpty().MinimumLength(3).NotEqual("string");
        RuleFor(o => o.Place).NotNull().NotEmpty().MinimumLength(3).NotEqual("string");
    }
}

public class SuperHeroDtoValidator : AbstractValidator<SuperHeroDto>
{
    public SuperHeroDtoValidator()
    {
        RuleFor(o => o.Name).NotNull().NotEmpty().MinimumLength(3).NotEqual("string");
        RuleFor(o => o.FirstName).NotNull().NotEmpty().MinimumLength(3).NotEqual("string");
        RuleFor(o => o.LastName).NotNull().NotEmpty().MinimumLength(3).NotEqual("string");
        RuleFor(o => o.Place).NotNull().NotEmpty().MinimumLength(3).NotEqual("string");
    }
}
```

## Filter validator for checking currect parameters passed to endpoints

```javascript
public class ValidationFilter<T> : IRouteHandlerFilter where T : class
{
    readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var arg = context.Arguments.SingleOrDefault(p => p.GetType() == typeof(T));
        if (arg is null) return Results.BadRequest("The parameter is invalid.");

        var result = await _validator.ValidateAsync((T)arg);
        if (!result.IsValid)
        {
            var errors = string.Join(' ', result.Errors);
            return Results.Problem(errors);
        }

        return await next(context);
    }
}
```

## Short video of using postman request api with token authorization
https://www.screencast.com/t/E3igDLQE
https://stackoverflow.com/a/74616217/19827098
