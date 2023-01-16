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

# Min-Maxing .NET Minimal APIs

---

**Min-Maxing .NET Minimal APIs**

- principles of min-maxing
- foundations of minimal api
- getting large
- the min-max
- the future
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
    <li style="color:#87842c">Eng. Manager @ <strong style="color:#cf47ff;font-family: Arial Black">Accenture</strong></li>
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

Note: Now, **that** is an attitude I can get behind.
I am an incredibly lazy - developer. Not in that I don't like to work -- OK maybe in that --
but more importantly in that I want to minimize the amount of trivial minutia that I'm doing.
I want to focus on the good stuff.

---

⬆️ The Goals of Min-Maxing ⬆️

- Reduce overhead of building application
- Focus on features / "important" concerns
- Still deliver a comprehensive application

---

📉 The Principles of Min-Maxing 📈

- Favor larger **up-front** effort to ongoing minutia
- Leverage **conventions** leaving room for **exceptions**
- Exploit without breaking - **know when to walk away**

Note: In all of these, we want to be pragmatic and know our limits

---

## Minimal API Foundations

Note: If you're familiar with web development, these will look familiar! If you know .NET, even better.

---

🤏 Minimal APIs 

- introduced in ASP.NET Core 6.0
- REST-style web requests w/o MVC scaffolding
- brings (formal) .NET web frameworks up to present day

Note:

- These came out in 2021?
- Allow folks to leverage all the proven, developed middleware without extra cruff
- Other languages and runtimes were here already - JS/Node/Express; many in python; Ruby Sinatra
  - .NET had `Nancy` and more recently `Carter` but nothing "official"

---

🐠 Our Sample API 🌿

- Manage Aquariums (CRUD)
- Manage Fish (CRUD)
- Manage Decor (CRUD)
- Feed the Fish (Action)
- Clean the Aquarium (Status, Action)

Note: As we continue talking about Minimal APIs we're going to use a Fish Aquarium management
application as our example. This is an API with more than type of consumer - someone
might have a CLI for it, or a web app with a UI. We don't know.
But it has these basic features:

(CRUD is Create Read Update Delete)

---

<!-- .slide: data-background-image="assets/nolan-krattinger-ADcXaqQ9vOM-unsplash.jpg", data-background-size="cover" -->

<!-- Image source: Nolan Krattinger on https://unsplash.com/photos/ADcXaqQ9vOM -->

Note: An image of a fishbowl with a goldfish cramped in it.

So here's what we can get from that tutorial as far as our fish!

---

🐡 Our Requirements 🦈

- Well- and self- documented
- Secure and validated endpoints
- Open for expansion and extension

Note:

Since we don't know our consumers, we want best-in-class clear documentation.
We of course need secure endpoints and robust request validation
We need to get these features added in a way we can easily add more,
  or extend the inner workings to support other components

---

<!-- Screenshot here -->

---

👩‍💻 Sidebar: Sample Code 👨‍💻

- Available at [github.com/pmcvtm/presentations](https://github.com/pmcvtm/presentations)
- Linked directly from THAT site under resources
- Representative, but **incomplete**
- Showing code for concepts _(you can copy/paste later)_

Note:

- Solution is built out, with commit history going through what we've doing today.
- Does not include **everything** we're talking about, but should give you an idea
- Going to be moving quickly over code samples today; the goal is to understand the concepts, not get this all down directly

---

🏫 Tutorial Speedrun 🏃‍♂️

```csharp [|1,5,21|7|8,10-11|2,13-14|3,16,18-19|]
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IFishService>();
builder.Services.AddDefaultIdentity<IdentityUser>();

var app = builder.Build();

app.MapGet("/fish", () => "Hello Fish!");
app.MapGet("/fish/{name}", (string name) => $"Hello {name}!");

app.MapPost("/fish", ([FromBody] Fish fish)
  => $"Hello {fish.Name}!");

app.MapPut("/fish", ([FromBody] Fish fish,
  [FromService] IFishService service) => service.SayHello(fish));

app.MapPost("/fish/feed", () => "Om nom nom").RequireAuthorization()

app.UseAuthentication();
app.UseAuthorization();

app.Run();
```
<!-- .element: class="stretch" -->

Note: Let's run through the tutorial for anyone who hasn't seen it or needs a referesher.

- This is `Main()` in `Program.cs`. No wrapping class, and no `Startup`. All in one
- Here are our core parts: building up the web app, and running it
- We can register an endpoint - a Route and Verb an Http Request comes through to, and our response to it
- Endpoints can be parameterized either by URL query or request body
- Services can be registered to our IoC container and injected into our response delegates
- Middleware works just like it used to, we register it, configure our endpoints with them, and then tell the app to use them

---

<!-- .slide: data-background-image="assets/ahmed-zayan-URaZrRvKQqM-unsplash.jpg", data-background-size="cover" -->

<!-- Image source: Ahmed Zayan on https://unsplash.com/photos/URaZrRvKQqM -->

Note: An image of a fishbowl with a goldfish cramped in it.

So here's what we can get from that tutorial as far as our fish!

---

👍 Nice Stuff 🆒

- straightforward, fluent syntax for **visible** behavior
- _FAST!_ to write, but also to run
- natural fit for **Vertical Slice Architecture**

Notes:

- straightforward to implement, with less "magic" than MVC
- .NET 6 is super performant, with minimal APIs gaining on MVC as well
- an architectural "pit of success" for organizing your code in a manageable way

----

🗼 Sidebar: Vertical Slices 🏭

![Diagram of several "layers" of an application (UI, Application, Domain, DB) and a "slice" running vertically across them](https://jimmybogardsblog.blob.core.windows.net/jimmybogardsblog/3/2018/Picture0030.png)
Source: [jimmybogard.com](https://jimmybogard.com/vertical-slice-architecture)

Note: Made popular by Jimmy Bogard - If you think about our concerns when implementing
a feature in an application or system, we typically have these layers. And a historically
common pattern would be to organize code and build wide-reaching components around these layers.

Vertical slices are instead organized around distinct requests, where you encapsulate
all of the concerns required for a given feature or activity _across_ layers,
instead of within them.

If you have used MVC and organized your controllers into `Feature Folders` or used the `Mediatr` library
you may be familiar with this. Additionally if you've hopped over to Razor Pages, those are similarly
set up to "fall" into this organizational pattern.

---

## Getting Large

Note: Let's start addressing the actual concerns we have in our api; the features and
infrastructure we need to get by

---

### Important Integrations

---


🔒 Security / Auth 🔑

- uses ASP.NET middleware **just great**
- auth applied per-endpoint by policy
<!-- .element: style="padding-bottom: 1em" -->

1. set up auth middleware and policies
2. add authorization scopes to each endpoint

----

🔒 Auth Middleware Setup 🔑

```csharp [|1-3|5|6-12]
services.AddAuthentication()
  //Whatever Auth You Want
;

services.AddAuthorization(opt => {
  foreach(var scope in ApiScope.GetAll())
  {
      var scopePolicy = new AuthorizationPolicyBuilder()
        .RequireClaim(SecurityConstants.Claims.Scope, scope.Name)
        .Build();

      opt.AddPolicy(scopePolicy);
  }
});
```

Note: Here we set up authentication and authorization by middleware in our services buildup

1. any authentication middleware will do - OIDC with jwts, identity sign-in, etc
2. for authorization, we'll loop through all our defined scopes and add each as a policy
   this makes it easy to reference later

----

🔐 Auth Endpoint Configuration 🗝

```csharp [|1,5|3,7|9-10]
app.MapGet("/aquariums", (IAquariumService service)
        => service.GetAquariums())
   .RequireAuthorization(ApiScopes.AquariumManagement);

app.MapPost("/aquariums", (Aquarium request,
    IAquariumService service) => service.AddAquarium(request))
   .RequireAuthorization(ApiScopes.AquariumManagement);

app.MapGet("/fish", (IFishService) => service.GetFishes())
   .RequireAuthorization(SecurityConstants.Scopes.FishManagement);
```
<!-- .element: style="height:80%" -->

Notes:

1. We have 2 aquarium endpoints
2. Each will require the `AquariumManagement Scope`
3. Our "get fish" endpoint requires the `FishManagement` scope

---

📜 Documentation ✍

- leverages OpenAPI metadata
- or integrates with Swagger / Swashbuckle / NSwag
- tries its hardest to infer (but better to be explicit)
- configured per-endpoint:
<!-- .element: style="padding-bottom: 1em" -->

1. Add descriptive metadata for request/responses
2. Add descriptive metadata for Swagger docs
3. _(Set up Swagger at startup)_

Note: This will get us all our nice possible response codes and descriptions listed out in our docs,
and any schema-linking for matching types... a big help for our consumers writing their apps

----

📜 Adding Built-In HttpMetadata ✍

```csharp [1-2|3|4|5-7]
app.MapGet("/aquariums", (IAquariumService service) => service.GetAquariums())
   .RequireAuthorization(ApiScopes.AquariumManagement)
   .WithTags("aquariums")
   .Produces(200, responseType: typeof(Aquarium[]))
   .Produces(401)
   .Produces(500);
```

Notes: Here's our get-all aquariums endpoint, with the security

1. First a tag, to group this with other "aquarium" requests
2. On success produces a 200 with an array of Aquariums
3. Also can produce 401, or 500 (you can imagine other endpoints will have other responses too)

----

📜 Adding Swagger Annotations ✍

```csharp [|4-5]
app.MapGet("/aquariums", (IAquariumService service) => service.GetAquariums())
   .RequireAuthorization(ApiScopes.AquariumManagement)
   .WithTags("aquariums")
   .WithMetadata(new SwaggerOperationAttribute("Retrieve all Aquariums"));
   .WithMetadata(new SwaggerResponseAttribute(403, "Forbidden. User is not authorized for this endpoint"));
   .Produces(200, responseType: typeof(Aquarium[]))
   .Produces(401)
   .Produces(500);
```

Note: Add the operation description to our Swagger doc as well as a 403 with some more info on what that means

As an aside...

----

👖 MetaData "Belt and Suspenders" 🧷

```csharp []
public static RouteHandlerBuilder WithResponse<T>(
  this RouteHandlerBuilder builder, int code, string? description = null)
{
  builder.Produces(code, responseType: typeof(T));
  builder.WithMetadata(new SwaggerResponseAttribute(code, description, typeof(T)));
  return builder;
}
```

```csharp []
app.MapGet("/aquariums", (IAquariumService service) => service.GetAquariums())
   .WithResponse<Aquarium[]>(200, "Return all fish aquariums")
```

Note: I am nervous about covering all the bases, so I use this helper method which adds a response
code both using the Http Metadata and using Swagger attributes. This is the type'd version for
responses with a body - we also have one without.

---

🔢 Versioning 🆙

- integrates with [`Asp.Versioning.Http`](https://www.nuget.org/packages/Asp.Versioning.Http)
- compatible with OpenAPI definitions
- **only supports version by URL query string**

<!-- .element: style="padding-bottom: 1em" -->

1. Define our versions at startup
2. Register each endpoint for its versions

----

🔢 With .NET ASP.NET Versions 🆙

```csharp []
app.DefineApi()
   .HasApiVersion( 1.0 )
   .HasApiVersion( 2.0 )
   .ReportApiVersions();
```

```csharp [|9-10]
app.MapGet("/aquariums", (IAquariumService service) => service.GetAquariums())
   .RequireAuthorization(ApiScopes.AquariumManagement)
   .WithTags("aquariums")
   .WithMetadata(new SwaggerOperationAttribute("Retrieve all Aquariums"));
   .WithResponse<Aquarium[]>(200, "All fish aquariums")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(403, "Forbidden.  User is not authorized for this endpoint")
   .WithResponse(500, "Internal server error.")
   .HasDeprecatedApiVersion( 0.9 )
   .HasApiVersion( 1.0 );
```

Note: You can also use OpenAPI Versions

1. Define versions at app startup
2. Add those versions to the endpoint

I personally like storing these in some static context to make it easier / guarantee consistency.

Also as a side note - part of the motivator for our big min-maxing solution was due to versioning;
at the time (and maybe now) only URL query versioning was supported, and we wanted our versions in
the route directly. I won't be talking about that explicitly, but you can probably see how it fits in.

---

🤝 All Together Now 👐

```csharp [|1|2|3-4|5-8|9-10|]
app.MapGet("/aquariums", (IAquariumService service) => service.GetAquariums())
   .RequireAuthorization(ApiScopes.AquariumManagement)
   .WithTags("aquariums")
   .WithMetadata(new SwaggerOperationAttribute("Retrieve all Aquariums"));
   .WithResponse<Aquarium[]>(200, "All fish aquariums")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(403, "Forbidden.  User is not authorized for this endpoint")
   .WithResponse(500, "Internal server error.")
   .HasDeprecatedApiVersion( 0.9 )
   .HasApiVersion( 1.0 );
```

Note: Here's our whole endpoint for getting aquariums:

1. Route and verb registration
2. Authorization
3. Descriptive metadata for Swagger
4. Response metadata for Swagger
5. Versions

----

🔂 Again 👐

```csharp [|1|4-5|6|]
app.MapPost("/aquariums", (Aquarium request, IAquariumService service) => service.CreateAquarium(request))
   .RequireAuthorization(ApiScopes.AquariumManagement)
   .WithTags("aquariums")
   .WithMetadata(new SwaggerOperationAttribute("Create a new Aquarium"));
   .WithResponse<Aquarium>(201, "Aquarium created")
   .WithResponse(400, "Bad Request. The request was invalid and cannot be completed. See the response body for details")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(403, "Forbidden.  User is not authorized for this endpoint")
   .WithResponse(500, "Internal server error.")
   .HasDeprecatedApiVersion( 0.9 )
   .HasApiVersion( 1.0 );
```
Note: Again!

1. For our POST / create method
2. Mostly the same, slightly different descriptions
3. Note we also have a 400 in the mix here

----

🔁 And Again 👐

```csharp [|1|9|]
app.MapPut("/aquariums/{id}",(int id, Aquarium request, IAquariumService service) => service.UpdateAquarium(id, request))
   .RequireAuthorization(ApiScopes.AquariumManagement)
   .WithTags("aquariums")
   .WithMetadata(new SwaggerOperationAttribute("Updates an existing Aquarium"));
   .WithResponse<Aquarium>(200, "Aquarium updated")
   .WithResponse(400, "Bad Request. The request was invalid and cannot be completed. See the response body for details")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(403, "Forbidden.  User is not authorized for this endpoint")
   .WithResponse(404, "Not found. The resource identifier is invalid")
   .WithResponse(500, "Internal server error.")
   .HasDeprecatedApiVersion( 0.9 )
   .HasApiVersion( 1.0 );
```

Note: Again!

1. For PUT / update ... more or less the same
2. We have a 404 not found response here now

----

🔁 And Again 😰

```csharp [|1|8|]
app.MapGet("/aquariums/{id}",(int id, IAquariumService service) => service.GetAquarium(id))
   .RequireAuthorization(ApiScopes.AquariumManagement)
   .WithTags("aquariums")
   .WithMetadata(new SwaggerOperationAttribute("Fetches an existing Aquarium"));
   .WithResponse<Aquarium>(200, "Aquarium retrieved")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(403, "Forbidden.  User is not authorized for this endpoint")
   .WithResponse(404, "Not found. The resource identifier is invalid")
   .WithResponse(500, "Internal server error.")
   .HasDeprecatedApiVersion( 0.9 )
   .HasApiVersion( 1.0 );
```

Note: Again!

1. For GET a single aquarium
2. We have a 404 but not the 400...
    slightly different descriptions...

----

🔁 And Again 😩

```csharp [|1|2-3|4-5|6-8|9|]
app.MapGet("/fish", (IFishService service) => service.GetFishes())
   .RequireAuthorization(ApiScopes.FishManagement)
   .WithTags("fish")
   .WithMetadata(new SwaggerOperationAttribute("Fetches all fish"));
   .WithResponse<FishModel[]>(200, "All fish tracked by the system")
   .WithResponse(401, "Unauthorized. The request requires authentication")
   .WithResponse(403, "Forbidden.  User is not authorized for this endpoint")
   .WithResponse(500, "Internal server error. An unhandled error occurred on the server. See the response body for details.")
   .HasApiVersion( 1.0 );
```

Note: And now we start on our **Fish** endpoints

1. Get at the fish/ route will use a different service I guess
2. Now we need the fish auth scope and doc grouping
3. Different return model and descriptions
4. Same universal responses
5. Fish are v1 only so that's a little different

---

<!-- .slide: data-background-image="assets/thomas-park-21hpHNuO5II-unsplash.jpg", data-background-size="cover" -->

<!-- Image source: Thomas Park on https://unsplash.com/photos/21hpHNuO5II -->

Note: An image of a somewhat dirty fish tank full of goldfish

And we're going to keep doing this for all of our endpoints.
So we've grown from our little fishbowl - but it's messy, and very repetitious.


---

## The Min-Max

---

💀 The Pains ⛑️

- repeating code to generate same or similar results
- entirely around `RouteHandlerBuilder` extensions
- patterns emerge by:
  - resource
  - HTTP method

----

💧 Patterns: By Resource 🐠

- the **authorization scopes** we require
- the **groups** for our endpoints
- the **nouns** in our documentation

----

🕸 Patterns: By HTTP Method 🆗

- the **kinds of responses** we might return
- the **verbs** in our documentation

Note: Whether a 404 makes sense, and which CRUD action we're taking

---

↔ `RouteHandlerBuilder` 🚧

- designed for extension methods
  - `sealed` and has few props
- any "state" is in untrustworthy `List<Object> MetaData`
- we _might could_ parse out what we need

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

### Even Larger

---

🌳 Growing with `OpinionatedBuilder` 🪓

---

👋 When To Walk Away (Or Not Start) 🚶‍♂️

---

## Questions

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
