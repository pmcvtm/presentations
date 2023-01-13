using Swashbuckle.AspNetCore.Annotations;

namespace FishApi.Features;

public class OpinionatedEndpointBuilder
{
    private readonly IEndpointRouteBuilder _endpoints;
    private HttpVerb? _verb;
    private string? _route;
    private Delegate? _handler;
    private string _resourceName;

    public OpinionatedEndpointBuilder MapGet(string route, Delegate handler)
        => MapBase(HttpVerb.GET, route, handler);

    public OpinionatedEndpointBuilder MapPost(string route, Delegate handler)
        => MapBase(HttpVerb.POST, route, handler);

    public OpinionatedEndpointBuilder MapPut(string route, Delegate handler)
        => MapBase(HttpVerb.PUT, route, handler);

    public OpinionatedEndpointBuilder MapDelete(string route, Delegate handler)
        => MapBase(HttpVerb.DELETE, route, handler);

    public void Build()
    {
        if (_verb == null || _route == null || _handler == null)
            throw new Exception("Invalid endpoint registration. Verb, Route, and Handler must be specified");

        var builder = _verb switch
        {
            HttpVerb.GET => _endpoints.MapGet(_route, _handler),
            HttpVerb.POST => _endpoints.MapPost(_route, _handler),
            HttpVerb.PUT => _endpoints.MapPut(_route, _handler),
            HttpVerb.DELETE => _endpoints.MapDelete(_route, _handler),
            _ => throw new ArgumentOutOfRangeException($"Unconfigured HTTP verb for mapping: {_verb}")
        };

        builder.WithMetadata(new SwaggerOperationAttribute(GetDescription()));

        builder.WithResponseCode(500, "Internal server error. See the response body for details.");

        if (_route.Contains("id", StringComparison.InvariantCultureIgnoreCase))
            builder.WithResponseCode(404, "Not found. A resource with given identifier could not be found.");

        if (_verb is HttpVerb.PUT or HttpVerb.POST)
            builder.WithResponseCode(400, "Bad Request. See the response body for details.");
    }

    private enum HttpVerb { GET, POST, PUT, DELETE }

    private string GetDescription() =>
        _verb switch
        {
            HttpVerb.GET => _route!.Contains("id") ? $"Retrieves a specific {_resourceName.ToSingular()} based on the identifier." : $"Retrieves all {_resourceName}.",
            HttpVerb.POST => $"Creates {_resourceName} based on the supplied values.",
            HttpVerb.PUT => $"Updates {_resourceName} based on the resource identifier.",
            HttpVerb.DELETE => $"Deletes an existing {_resourceName.ToSingular()} using the resource identifier.",
            _ => throw new ArgumentOutOfRangeException($"Unconfigured HTTP verb for default description {_verb}")
        };
    private OpinionatedEndpointBuilder MapBase(HttpVerb verb, string route, Delegate handler)
    {
        if (_verb != null || _route != null || _handler != null)
            throw new Exception("Builder has already been initialized. Builder is valid for one endpoint only.");

        _verb = verb; _handler = handler; _route = route.Trim('/');
        _resourceName = _route.Split('/').First();
        return this;
    }

    public OpinionatedEndpointBuilder(IEndpointRouteBuilder endpoints) => _endpoints = endpoints;
}
