using FluentValidation;
using Microsoft.Extensions.Logging;

namespace SuperHeroApi.Filters
{
    public class ValidationFilter<T> : IRouteHandlerFilter where T : class
    {
        readonly IValidator<T> _validator;
        //readonly ILogger _logger;

        public ValidationFilter(IValidator<T> validator/*, ILoggerFactory loggerFactory*/)
        {
            _validator = validator;
            //_logger = loggerFactory.CreateLogger<ValidationFilter<T>>();
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
                //_logger.LogWarning(validationError);
                return Results.Problem(errors);
            }

            // now the actual endpoint execution
            return await next(context);
        }
    }
}
