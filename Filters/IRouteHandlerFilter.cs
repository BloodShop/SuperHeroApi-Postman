namespace SuperHeroApi.Filters
{
    public interface IRouteHandlerFilter
    {
        ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next);
    }
}
