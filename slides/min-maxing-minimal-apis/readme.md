---
title: 'Min-Maxing .NET 6 Minimal APIs'
theme: simple
revealOptions:
  transition: none
#  controls: false
  progress: false
  # navigationMode: linear
css:
- overrides.css
- atom-one-dark-reasonable.min.css
---

# Mix-Maxing .NET 6 Minimal APIs

---

- min-maxing principles
- minimal api foundations
- important integrations
- the min-max
- questions

---

<!-- .slide: data-background-color="#dbd1b3" -->
<!-- .slide: data-transition="slide" -->

<div style="color:#5a3d2b;font:oblique 1em 'Vibur', sans-serif">Patrick McVeety-Mill "is"</div>
<div style="color:#5a3d2b;font:normal 1.4em 'Bungee Shade', cursive;line-height:1em;padding-bottom:2rem" >Loud & Abrasive</div>

<div style="display:flex; align-items: center;">
  <ul style="flex:5">
    <li style="color:#e73602">he/him</li>
    <li style="color:#e5771e">likes ⛺ 🎶 🏊‍♂️ 🎨 🍻 🤙</li>
    <li style="color:#87842c">Eng. Manager @ <strong style="color:#cf47ff">Accenture</strong></li>
    <li style="color:#2a9d8f">@pmcvtm</li>
    <li style="color:#264653">@loudandabrasive</li>
  </ul>
  <div style="flex:4">
    <a href="https://loudandabrasive.com/card">
      <img src="assets/pmcvtm-qr-code.jpg" alt="QR code which directs to https://loudandabrasive.com/card">
    </a>
  </div>
</div>

---

## Principles of Min-Maxing

---

➕ What is Min-Maxing? ➖

> Min-Maxing is the art of optimizing a character's abilities during creation by maximizing the most important skills and attributes, while minimizing the cost. Done by strategic decrease of stats believed to be less important, exploiting overpowered but legal combinations of the Game System...

Note: From tvtropes.org

Personally I associate this most with Dungeons and Dragons, but it can be done in any game with player statistics or exploitable rules. In D&D you typically become more powerful by playing the game and gaining experience points. That takes time. A min-maxer will instead find rules which act as shortcuts to those benefits.

----

🧚‍♂️ Example: The Xvart 🐷

![Two blue pug-nosed goblin-like creatures](assets/xvart.png)

Note: As an example, this is a Xvart. In D&D, you might play as a human, or a strong orc, or an elegant elf. You can also play as a Xvart. Xvarts are very small, ugly, relatively low-intelligence creatures in comparison to most of the other playable character types. Because of this, when you elect to play as a Xvart, you get _other_ benefits, like extra levels, to compensate and keep you "even" with other players. You're 1 foot tall and smell bad, but you can also sneak around and have magic at a level much higher than your comrades. It _can_ be frowned upon in some circles. It feels like cheating.

----

➕ What is Min-Maxing? ➖

> Seen from a purely mathematical and gamist perspective, it's an elegant process of minimum expenditure for maximum result.

> It is **getting the most by doing the least.**

Note: Now, **that** is an attitude I can get behind. I am incredibly lazy - in that I want to minimize the
amount of trivial minutia that I'm doing, so I can focus on the good stuff.

---

⬆️ The Goals of Min-Maxing ⬆️

- Reduce overhead of building application
- Focus on features / "important" concerns
- Still deliver a comprehensive application

---

📉 The Principles of Min-Maxing📈

- Minimize repeated, trivial, or obvious tasks
- Leverage conventions leaving room for exceptions
- Exploit without breaking - know when to walk away

---

## Minimal API Foundations

Note: If you're familiar with web development, these will look familiar! If you know .NET, even better.

---

👋 Hello World 🌎

```csharp []
var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();

app.MapGet("/hello", () => "Hello World!");













app.Run();
```
<!-- .element: class="stretch" -->

----

👋 Hello to Who ❓

```csharp [8,10,11|8|10,11]
var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();

app.MapGet("/hello", () => "Hello World!");
app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");

app.MapPost("/hello", ([FromBody] HelloRecipient person)
  => $"Hello {person.Name}!");









app.Run();
```
<!-- .element: class="stretch" -->

Note: We can parameterize the endpoints with route values or URL queries,
or submit a body. And here we get a taste of what actual functionality looks like.

----

👋 Hello Services 🔀

```csharp [2,13,14]
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IHelloService>();


var app = builder.Build();

app.MapGet("/hello", () => "Hello World!");
app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");

app.MapPost("/hello", ([FromBody] HelloRecipient person)
  => $"Hello {person.Name}!");

app.MapPut("/hello", ([FromBody] HelloRecipient person,
  [FromService] IHelloService service) => service.SayHello(person));






app.Run();
```
<!-- .element: class="stretch" -->

Note: We can register services to the Inversion of Control container and then
use them in our handlers. That includes ones we write, or ones from libraries like
Entity Framework or middleware providers like...

----

👋 Hello Middleware 🧱

```csharp [3,18,19|16|]
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IHelloService>();
builder.Services.AddDefaultIdentity<IdentityUser>();

var app = builder.Build();

app.MapGet("/hello", () => "Hello World!");
app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");

app.MapPost("/hello", ([FromBody] HelloRecipient person)
  => $"Hello {person.Name}!");

app.MapPut("/hello", ([FromBody] HelloRecipient person,
  [FromService] IHelloService service) => service.SayHello(person));

app.MapGet("/hug", () => "Hello good friend!").RequireAuthorization()

app.UseAuthentication();
app.UseAuthorization();

app.Run();
```
<!-- .element: class="stretch" -->

Note: ASP.NET Identity! We can add middleware the same as in MVC apps, and most work.
The ones that don't it's typically due to the difference in contexts available to
endpoints vs. Controllers.

If you've worked with MVC and then Razor Pages, you may
have encountered some similar minor differences.

---

👍 Nice Stuff 🆒

- straightforward and fluent
- **visible** behavior and config
- natural fit for **Vertical Slice Architecture**

----

🗼 Sidebar: Vertical Slices 🏭

![Diagram of several "layers" of an application (UI, Application, Domain, DB) and a "slice" running vertically across them](https://jimmybogardsblog.blob.core.windows.net/jimmybogardsblog/3/2018/Picture0030.png)
Source: [jimmybogard.com](https://jimmybogard.com/vertical-slice-architecture)

Note: Made popular by Jimmy Bogard - If you think about our concerns when implementing
a feature in an application or system, we typically have these layers. And a historically
common pattern would be to organize and build around these layers.

Vertical slices are instead organized around distinct requests, where you encapsulate
all of the concerns required for a given feature or activity _across_ layers,
instead of within them.

---

🐠 Our Sample API 🌿

- Manage Fish Tanks (CRUD)
- Manage Fish (CRUD)
- Manage Decor (CRUD)
- Feed the Fish (Action)
- Clean the Tank (Status, Action)

Note: As we continue talking about Minimal APIs we're going to use a Fish Tank management
application as our example. This is an API with more than type of consumer - someone
might have a CLI for it, or a web app with a UI. We don't know.
But it has these basic features:

---

🐡 Our Requirements 🦈

- Well-documented
- Secure, validated
- Room to grow

----

<!-- Screenshot here -->

---

## Getting Large

---
🔒 Security / Auth 🔑

- uses ASP.NET middleware **just great**
- authorization is per-endpoint
- authorization applied by policy (or rich type)

----

Auth Endpoint Configuration

```csharp [|1,4|2,6|8|9]
app.MapGet("/aquariums", (IAquariumService service) => service.Getaquariums())
   .RequireAuthorization(SecurityConstants.Scopes.AquariumManagement);

app.MapPost("/aquariums", (Aquarium request,
    IAquariumService service) => service.AddAquarium(request))
   .RequireAuthorization(SecurityConstants.Scopes.AquariumManagement);

app.MapGet("/aquariums", (IFishService) => service.GetFishes())
   .RequireAuthorization(SecurityConstants.Scopes.FishManagement);
```
<!-- .element: style="height:80%" -->

----

Auth Middleware Setup

```csharp [|1-3|5|6-12]
services.AddAuthentication()
  //Whatever Auth You Want
;

services.AddAuthorization(opt => {
  foreach(var scope in SecurityConstants.Scopes)
  {
      var scopePolicy = new AuthorizationPolicyBuilder()
        .RequireClaim(SecurityConstants.Claims.Scope, scopeName)
        .Build();

      opt.AddPolicy(scopePolicy);
  }
});
```

Note: Here we set up authentication and authorization by middleware
Looping through so each of our scopes has a respective policy

---

📜 Documentation ✍

- integrates nicely with Swagger / Swashbuckle
- leverages OpenAPI metadata
- configured per-endpoint
- tries its hardest to infer (but better to be explicit)

----

Using Built-In HttpMetadata

```csharp [2|4-5|3]
app.MapGet("/aquariums", (IAquariumService service) => service.Getaquariums())
  .WithTags("aquariums")
  .Produces(200, "Get all aquariums" responseType: typeof(Aquarium[]))
  .Produces(401, "Unauthorized. The request requires authentication")
  .Produces(500, "Unexpected error");
```

----

Using Swagger Annotations

```csharp [|2-5]
app.MapGet("/aquariums", (IAquariumService service) => service.Getaquariums())
   .WithMetadata(new SwaggerResponseAttribute(200,
     type: typeof(Aquarium[]) ))
   .WithMetadata(new SwaggerResponseAttribute(401))
   .WithMetadata(new SwaggerResponseAttribute(500));
```

----

Using "Belt and Suspenders"

```csharp
public static RouteHandlerBuilder WithResponse<T>(
  this RouteHandlerBuilder builder, int code, string? description = null)
{
  builder.Produces(code, responseType: typeof(T));
  builder.WithMetadata(new SwaggerResponseAttribute(code, 
    description, typeof(T)));
  return builder;
}
```

```csharp
app.MapGet("/aquariums", (IAquariumService service) => service.Getaquariums())
   .WithResponse<Aquarium[]>(200, "Return all fish aquariums")
```

---

🔢 Versioning 🆙

- integrates with [`Asp.Versioning.Http`](https://www.nuget.org/packages/Asp.Versioning.Http)
- compatible with OpenAPI definitions
- **only supports version by URL query string**

----

With .NET ASP.NET Versions

```csharp []
app.DefineApi()
   .HasApiVersion( 1.0 )
   .HasApiVersion( 2.0 )
   .ReportApiVersions();
```

```csharp
app.MapGet("/aquariums", (IAquariumService service) => service.Getaquariums())
   .HasDeprecatedApiVersion( 0.9 )
   .HasApiVersion( 1.0 );
````

Note: You can also use OpenAPI Versions

---

All Together Now:

```csharp[|1|2|3-5|6-7]
app.MapGet("/aquariums", (IAquariumService service) => service.Getaquariums())
   .RequireAuthorization(SecurityConstants.Scopes.AquariumManagement);
   .WithResponse<Aquarium[]>(200, "Retrieves all aquariums")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(500, "Internal server error. An unhandled error occurred on the server. See the response body for details")
   .HasDeprecatedApiVersion( ApiConstants.Versions.V09 )
   .HasApiVersion( ApiConstants.Versions.V1 )
```

----

Again:

```csharp [|4]
app.MapPost("/aquariums", (Aquarium request, IAquariumService service) => service.CreateAquarium(request))
   .RequireAuthorization(SecurityConstants.Scopes.AquariumManagement)
   .WithResponse<Aquarium>(200, "Creates new Aquarium")
   .WithResponse(400, "Bad Request. The request was invalid and cannot be completed. See the response body for details")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(500, "Internal server error. An unhandled error occurred on the server. See the response body for details")
   .HasApiVersion( ApiConstants.Versions.V1 );
```

----

And Again:

```csharp [|5-6]
app.MapPut("/aquariums/{id}",(int id, Aquarium request, IAquariumService service)
        => service.UpdateAquarium(id, request))
   .RequireAuthorization(SecurityConstants.Scopes.AquariumManagement)
   .WithResponse<Aquarium>(200, "Updates the Aquarium specified by ID")
   .WithResponse(400, "Bad Request. The request was invalid and cannot be completed. See the response body for details")
   .WithResponse(404, "Not found. The resource identifier is invalid")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(500, "Internal server error. An unhandled error occurred on the server. See the response body for details")
   .HasApiVersion( ApiConstants.Versions.V1 );
```

----

And Again:

```csharp [|1-3]
app.MapGet("/fish", (IFishService service) => service.GetFish())
   .RequireAuthorization(SecurityConstants.Scopes.FishManagement)
   .WithResponse<FishModel[]>(200, "Retrieves all Fish")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(500, "Internal server error. An unhandled error occurred on the server. See the response body for details.")
   .HasApiVersion( ApiConstants.Versions.V1 );
```

Note: This is exhausting!

---

## The Min-Max

---

💀 The Pains ⛑️

- repeating code to generate same or similar results
- patterns emerge by:
  - resource
  - HTTP method
- entirely around `RouteHandlerBuilder` extensions

----

Patterns: By Resource

----

Patterns: By HTTP Method

---

`RouteHandlerBuilder`

- designed forextension methods
  - `sealed` and has few props
- info is in big `List<Object> MetaData`
- we _could_ parse out the parts we need

---

### We can build it 👷‍

---

`OpinionatedEndpointBuilder`

- tracks HTTP verb + resource name
- sets conventional defaults
- provides optional overrides

---

💀 Bare Bones 🦴

```csharp [|1|8,9|3-5,11-17|6,15]
public class OpinionatedEndpointBuilder
{
    private HttpVerb? _verb;
    private string? _route;
    private Delegate? _handler;
    private string _resourceName;

    public OpinionatedEndpointBuilder(IEndpointRouteBuilder endpoints)
      => _endpoints = endpoints;

    private OpinionatedEndpointBuilder MapEndpoint
      (HttpVerb verb, string route, Delegate handler)
    {
        _verb = verb; _handler = handler; _route = route.Trim('/');
        _resourceName = _route.Split('/').First();
        return this;
    }

    //...
}
```
<!-- .element: class="stretch" -->

Notes: 

- Constructor takes `IEndpointRouteBuilder`
- Tracks `verb`, `route`, `handler`
- **private** method to set those values
  - also track the `resource name` from the URL


----

💀 Bare Bones: Endpoint Registration 🔀

```csharp [5-16]
public class OpinionatedEndpointBuilder
{
    // ...

    public OpinionatedEndpointBuilder MapGet(string route, Delegate handler)
        => MapEndpoint(HttpVerb.GET, route, handler);

    public OpinionatedEndpointBuilder MapPost(string route, Delegate handler)
        => MapEndpoint(HttpVerb.POST, route, handler);

    public OpinionatedEndpointBuilder MapPut(string route, Delegate handler)
        => MapEndpoint(HttpVerb.PUT, route, handler);

    public OpinionatedEndpointBuilder MapDelete(string route, Delegate handler)
        => MapEndpoint(HttpVerb.DELETE, route, handler);

    //...
}
```
<!-- .element: class="stretch" -->

----

💀 Bare Bones: `Build()` 🚧

```csharp [5-19|10-17]
public class OpinionatedEndpointBuilder
{
    // ...

    public void Build()
    {
        if (_verb == null || _route == null || _handler == null)
            throw new Exception("Invalid endpoint registration");

        var builder = _verb switch
        {
            HttpVerb.GET => _endpoints.MapGet(_route, _handler),
            HttpVerb.POST => _endpoints.MapPost(_route, _handler),
            HttpVerb.PUT => _endpoints.MapPut(_route, _handler),
            HttpVerb.DELETE => _endpoints.MapDelete(_route, _handler),
            _ => throw new NotImplementedException
                ($"Unconfigured HTTP verb for mapping: {_verb}")
        };
    }

    //...
}
```
<!-- .element: class="stretch" -->

Notes: And like any builder, we're going to need a `build()` method!
Here we pass through our builder methods into the `EndpointRouteBuilder`.

----

Refactoring `IFeature` ➡ `Feature`

```csharp [|3|4-5|]
public abstract class Feature
{
    //formerly void MapEndpoints(IEndpointRouteBuilder endpoints)
    public abstract void ConfigureEndpoint
      (OpinionatedEndpointBuilder builder);
}
```

```csharp [|3|5-7|]
application.UseEndpoints(endpoints =>
{
    foreach (var feature in GetFeatures().ToList())
    {
        var builder = new OpinionatedEndpointBuilder(endpoints);
        feature.ConfigureEndpoint(builder);
        builder.Build();
    }
});
```

---

🔑 Refactored Security Scopes 🔏

```csharp [|1-7|9-15|]
private ApiScope[] _scopes;

public OpinionatedEndpointBuilder RequireScopes(params ApiScope[] scopes)
{
    _scopes = scopes;
    return this;
}

private bool allowAnonymous = false;

public OpinionatedEndpointBuilder AllowAnonymous()
{
    _allowAnonymous = true;
    return this;
}
```

---

🔑 Refactored Security Scopes 🔏

```csharp [|6-7|8-9|10-13|]
public void Build()
{
    var builder = _verb switch //...
    // ...

    if(_allowAnonymous)
        builder.AllowAnonymous();
    else if(_scopes.Length > 0)
        builder.RequireAuthorization(_scopes);
    else
    {
        var scopes = ApiScopes.GetForResource(_resourceName);
        builder.RequireAuthorization(scopes);
    }
}
```

---

🏗 Refactored Metadata 📑

- leverage endpoint info builder
- track _new_ contextual info in builder
- conventional defaults + overrides

----

💬 Refactored Responses 🔢

```csharp [|3|6|8-12|14-15|17-18|]
public void Build()
{
    var builder = _verb switch //...
    // ...

    builder.WithResponseCode(500, "Internal server error. See response body for details");

    if(!_allowAnonymous)
    {
        builder.WithResponseCode(401, "Unauthorized. Request requires authentication");
        builder.WithResponseCode(403, "Forbidden. User is not authorized for this endpoint");
    }

    if (_route.Contains("id"))
        builder.WithResponseCode(404, "Not found. A resource with given identifier could not be found.");

    if (_verb is HttpVerb.PUT or HttpVerb.POST)
        builder.WithResponseCode(400, "Bad Request. See the response body for details.");

    // ...
}
```
<!-- .element: class="stretch" -->

----

💬 Refactored Responses 🆗

```csharp [|1-5|7-12|]
public OpinionatedEndpointBuilder WithResponse(int code, string description)
{
    _routeOptions.Add(b => b.WithResponse(code, description));
    return builder;
}

public OpinionatedEndpointBuilder WithResponse<T>(int code, string? description = null)
{
    _routeOptions.Add(b => b.Produces(code, responseType = typeof(T)));
    _routeOptions.WithMetadata(new SwaggerResponseAttribute(code, description, typeof(T)));
    return builder;
}
```

---

💬 Descriptions 📄

```csharp [|1-6|8-17|19-20]
private string? _description;
public OpinionatedEndpointBuilder WithDescription(string description)
{
    _description = description;
    return this;
}

private string GetDescriptionOrDefault() => _description ?? _verb switch
{
  HttpVerb.GET => _route!.Contains("id")
    ? $"Retrieves a specific {_resourceName.ToSingular()} based on the identifier."
    : $"Retrieves all {_resourceName}.",
  HttpVerb.POST => $"Creates {_resourceName} based on the supplied values.",
  HttpVerb.PUT => $"Updates {_resourceName} based on the resource identifier.",
  HttpVerb.DELETE => $"Deletes an existing {_resourceName.ToSingular()} using the resource identifier.",
  _ => throw new ArgumentOutOfRangeException($"Unconfigured HTTP verb for default description {_verb}")
};

//Inside Build()...
builder.WithMetadata(new SwaggerOperationAttribute(GetDescriptionOrDefault()));
```
<!-- .element: class="stretch" -->

----

1️⃣ Sidebar: `ToSingular()` 😂

```csharp
public static string ToSingular(this string input) =>
  input switch
  {
      "fish" => "fish",
      "fishes" => "fish",
      _ => input.Remove(input.Length - 1, 1)
  }
```

---

🎣 Catch All 🥅

```csharp [|1|2-6|8-14]
private readonly List<Action<RouteHandlerBuilder>> _routeOptions = new();
public OpinionatedEndpointBuilder WithRouteOption(Action<RouteHandlerBuilder> routeHandlerBuilderAction)
{
    _routeOptions.Add(routeHandlerBuilderAction);
    return this;
}

public void Build()
{
    var builder = _verb switch //...
    // ...
    foreach (var endpointAction in _routeOptions)
        endpointAction(builder);
}
```
---

### Results

---

↩ Starting Endpoint 😓

```csharp []
public class AddAquarium
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost(endpoints, "/aquariums", Handle)
            .RequireAuthorization(ApiScope.AquariumManagement)
            .WithMetadata(new SwaggerOperationAttribute("Create a new Aquarium"));
            .WithResponse<Aquarium>(201, "Aquarium created")
            .WithResponseCode(500, "Internal server error. See response body for details");
            .WithResponseCode(401, "Unauthorized. Request requires authentication");
            .WithResponseCode(403, "Forbidden. User is not authorized for this endpoint");
    }

    public Task<IResult> Handle(Validator validator, IFishContext db, Request request)
    {
        //...
    }
}
```
<!-- .element: class="stretch" -->

Note: Here was our starting point. And obviously some of these could be refactored into
helper methods (like for common response codes) but still, I have to remember to add those in.

---

🔚 Conventional Endpoint 😄

```csharp []
public class AddAquarium
{
    public void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapPost("/aquariums", Handle)
               .WithResponse<Aquarium>(201);
    }

    public Task<IResult> Handle(Validator validator, IFishContext db, Request request)
    {
        //...
    }
}
```

---

🔚 Exceptional Endpoint 😸

```csharp []
public class CleanAquarium
{
    public void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapPost("/aquariums/{id}/clean", Handle)
               .WithScopes(ApiScopes.Cleaner)
               .WithDescription("Cleans the aquarium to remove waste and unwanted growth")
               .WithResponse(200, "Aquarium cleaned");
    }

    public Task<IResult> Handle(int id, IFishContext db)
    {
        //...
    }
}
```

Note: Here is an exceptional case

- add custom scope + description
- specify the 200
- 404 is valid and will get picked up from ID param

---

## The Future

---

### .NET 7

---

📦 Route Groups 🖇

- allows grouping by prefix
- designed with auth and metadata in mind
- _still repeating some code_

----

📦 Route Groups in Action🖇

```csharp []
app.MapGroup("public/todos")
    .MapTodosApi()
    .WithDefaultResponseCodes()
    .AllowAnonymous();

app.MapGroup("/private/todos")
    .MapTodosApi()
    .WithDefaultResponseCodes()
    .WithAuthResponseCodes()
    .RequireAuthorization();

public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder group)
{
    group.MapGet("/", GetAllTodos).WithDescription("Get all the Todos");
    group.MapGet("/{id}", GetSingleTodo).WithDescription("Get a todo");
    group.MapPost("/", AddTodo).WithDescription("Create a todo");
    group.MapPut("/{id}", EditToDo).WithDescription("Edit a todo");
    group.MapDelete("/{id}", DeleteToDo).WithDescription("Remove a todo");

    return group;
}
```
<!-- .element: class="stretch" -->

---

🔻 Endpoint Filters 🌟

- like filters in MVC
- applied individually or to groups
- use case: validation

----

🔻 Endpoint Filters in Action 🌟

```csharp
app.MapGet("/colorSelector/{color}", ColorName)
    .AddEndpointFilter(async (invocationContext, next) =>
    {
        var color = invocationContext.GetArgument<string>(0);

        if (color == "Red")
            return Results.Problem("Red not allowed!");

        return await next(invocationContext);
    });

// or

app.MapGet("/colorSelector/{color}", ColorName)
   .AddEndpointFilter<RedNotAllowedFilter>();
```

---

🌟 Filters and Groups Together 🤝

```csharp []
var all = app
  .MapGroup("/")
  .WithUniversalResponses()
  .AddEndpointFilter<ValidationEndpointFilter>();

var aquariums = all.MapGroup("aquariums")
  .RequireAuthorization("AquariumManagement")
  .MapGet("/", GetAquariums)
  //...
```

Note: I haven't used .NET 7 filters yet, but I could see them
coming together like this. I don't know if I like it more than
the opinionated builder. But for more smaller use cases,
I can dig it. Definitely will use Filters for some things.

---
<!-- .slide: data-background-color="#dbd1b3" -->

<div style="color:#5a3d2b;font:normal 2em 'Bungee Shade', cursive;line-height:1em;padding-bottom:2rem">Thanks</div>

<div style="display:flex; align-items: center;">
  <ul style="flex:5">
    <li>Say hi! 👋</li>
    <li style="color:#264653">@pmcvtm</li>
    <li style="color:#264653">@loudandabrasive</li>
  </ul>
  <div style="flex:4">
    <a href="https://loudandabrasive.com/card">
      <img src="assets/pmcvtm-qr-code.jpg" alt="QR code which directs to https://loudandabrasive.com/card">
    </a>
  </div>
</div>
