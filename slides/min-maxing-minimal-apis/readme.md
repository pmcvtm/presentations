﻿---
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