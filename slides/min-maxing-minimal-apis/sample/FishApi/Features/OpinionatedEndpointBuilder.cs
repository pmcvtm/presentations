namespace FishApi.Features;

public class OpinionatedEndpointBuilder
{
    private readonly IEndpointRouteBuilder _endpoints;
    private HttpVerb? _verb;
    private string? _route;
    private Delegate? _handler;

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
    }

    private enum HttpVerb { GET, POST, PUT, DELETE }

    private OpinionatedEndpointBuilder MapBase(HttpVerb verb, string route, Delegate handler)
    {
        if (_verb != null || _route != null || _handler != null)
            throw new Exception("Builder has already been initialized. Builder is valid for one endpoint only.");

        _verb = verb; _route = route; _handler = handler;
        return this;
    }

    public OpinionatedEndpointBuilder(IEndpointRouteBuilder endpoints) => _endpoints = endpoints;
}
