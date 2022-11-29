using FluentValidation;
using SuperHeroApi.DataAccess.Models;
using SuperHeroApi.DataAccess.Models.Dto;

namespace SuperHeroApi.Validators
{
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
}
