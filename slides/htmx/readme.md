---
title: 'HTMX: Develop Complex Front-Ends Without Complex Workflows'
theme: simple
revealOptions:
  transition: none
  progress: false
  slideNumber: false
css:
- overrides.css
- atom-one-dark-reasonable.min.css
---

# HTMX

Develop Complex Front-Ends

Without Complex Workflows

---

#### Agenda

- resources
- hypermedia
- htmx + .net
- why / not
- questions

---

<!-- .slide: data-background-color="#dbd1b3" -->
<!-- .slide: data-transition="slide" -->

<div style="color:#5a3d2b;font:normal 1.4em 'Bungee Shade', cursive;line-height:1em;padding-bottom:2rem">Patrick McVeety-Mill</div>

<div style="display:flex; align-items: center;">
  <ul style="flex:5">
    <li style="color:#e73602">he/him</li>
    <li style="color:#e5771e">independent consultant</li>
    <li style="color:#87842c">likes ⛺  📻  🧑‍🌾  🎨  🍻  🤙</li>
    <li style="color:#2a9d8f">@pmcvtm</li>
    <li style="color:#264653">@loudandabrasive</li>
  </ul>
</div>

Notes: https://pmcvtm.com/linkinbio

---

### Resources & Further Reading


- [_Hypermedia Systems_](https://hypermedia.systems/) book
  - by Carson Gross, Adam Stepinski, Deniz Akşimşek

- **Kahlid Abuhakmeh**'s [blog](https://khalidabuhakmeh.com/tag/htmx/) and [series for JetBrains](https://www.jetbrains.com/guide/dotnet/tutorials/htmx-aspnetcore/)

- O'Reilly [_Server-Driven Web Apps with htmx_](https://www.oreilly.com/library/view/server-driven-web-apps/9798888651193/)

Notes: Start with the sources for this talk

- book - free online by authors of HTMX
- my first exposure was the blog
- new from O'Reilly
- won't be doing much deep dive into code or full solutions, so go look at those

---

## What Is Hypermedia

---

> **Hypermedia** is a media that includes non-linear branching from one location in the media to another. The prefix “hyper-” derives from the Greek prefix “ὑπερ-” which means “beyond” or “over”, indicating that hypermedia goes beyond normal, passively consumed media like magazines and newspapers.

Notes: From _Hypermedia Systems_ https://hypermedia.systems/hypermedia-a-reintroduction/

Pronounced "Ee-per"

---

> A **hypermedia control** is an element in a hypermedia that describes or controls some sort of interaction, often with a remote server, by encoding information about that interaction directly and **completely within itself.**

Notes: What differentiates from regular media - the ability to change it and move freely. In practice, these are our links and forms. The fact that it is all self-contained is an important ethos for how we approach the rest of the system

---

### A Hypermedia System has:

- **HTML** &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <!-- .element: class="fragment" --> form of hypermedia
- **HTTP** &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<!-- .element: class="fragment" --> communication protocol
- **whatever** &nbsp;&nbsp; <!-- .element: class="fragment" --> server to deliver hypermedia by request
- **broswer** &nbsp;&nbsp;&nbsp;&nbsp; <!-- .element: class="fragment" --> client to interpret those responses

Notes:

-  The **HT** is for **H**yper**t**ext

---

### REST & HATEOAS

**H**ypermedia
**A**s
**T**he
**E**ngine
**O**f
**A**pplication
**S**tate

with a Uniform Interface:

- messages are self-describing
- media encodes current state & possible changes


Notes: "Representational State Transfer" from Roy Fielding's disertation in 2000

- Things like client/server, caching, using URLs
- The important takeaway for is the **The uniform interface**
- Messages contain **all information** that is needed to request or respond
- Representations present the state as well as what can and can't be done
- **without** side-channels

Won't get into it all here, but there's more in the book:
https://hypermedia.systems/components-of-a-hypermedia-system/#_rest

----

#### REST

- client-server architecture

- stateless (requests have all they need)
- allows for caching
- layered system (allowed of servers)
- optionally allows Code-On-Demand (scripting)
- uses a **uniform interface**

----

#### The Uniform Interface

- resource IDs (urls)
- data and metadata representation of resources
- self-describing messages
- hypermedia as the engine of application state

---


### Hypermedia as an API

```url
/fish/season/1
```

```html
<h1>Fish</h1>
<h2>Season 1<h2>
<nav>
  <a disabled>Previous Season</a>
  <a href="/fish/season/2" title="Season 2">Next Season</a>
</nav>
<a href="fish/season/1/add">Add a fish</a>
<ul>
  <li>Thlump <a href="fish/1/feed">Feed</a> <a href="fish/season/1/remove/1">Remove</a></li>
  <li>Old Blue <a href="fish/season/1/remove/2">Remove</a></li>
</ul>
</body>
```
<!-- .element: class="fragment" -->

Notes: URL for a resource -> response

- navigation, info on what is valid
- actions we can take related to this resource
- the actual data
- their state and actions: feeding, removal

This is _not_ a typical API today

---

### Single Page Applications

- AJAX to communicate with the server
- JSON to present data
- JavaScript _representations_ of model to render interface

Notes:

- (not hypermedia controls)
- (not self-describing messages or media)
- storing a separate model and then "reacting" with DOM updates

---

VS a typical JSON resource

```url
/fish/season/1
```

```json
{
  "season": 1,
  "fish": [
    { "Id": 1, "Name": "Thlump", "Status": "Hungry" },
    { "Id": 2, "Name": "Old Blue", "Status": "Well Fed" },
  ]
}
```

Notes:

- No navigation
- No indication of what actions to take
  - What is valid based on state of fish
- All would require documentation paired with additional requests

---

#### How Did We Get Here

- poor UX from "traditional" apps
- JS advances quickly
- HATEOAS in JSON is painful
- HTML advances slowly

Notes:

- UX of plain HTML apps stunk
- DX of adding javascript stunk
- needs for JS increased rapidly
- JS ecosystem improves
- passing along links or form rules is difficult to interpret
  - _browsers_ are very advanced
- HTML is relatively unchanged - can't even do non-GET or POST

---

## HTMX

---

### History

- created by [Carson Gross](https://bigsky.software/cv/)
- "extension" of [intercoolerjs](https://intercoolerjs.org/)
- kinship with [unpoly](https://unpoly.com/) and [hotwire / turbo](https://hotwired.dev/)

<!-- See also a comparison of them: -->

----

Sidebar: Progressive Enhancement

- content-focused design strategy
- sites work _without_ JavaScript
- UX is enhanced by scripted functionality

Notes: Work with traditional links and forms, but use fancy JS for a nicer experience

- Impossible to do with SPAs
- The main point of `unploy`... and a natural fit for HTMX & Hotwire


---

### Basics

---

#### What is HTMX?

- low-level, lightweight JavaScript framework
- ...for modenrn UX in multi-page apps
- an "e**x**tension" of HTML
- HTML attribute-driven
- enhanced with headers, events, and more

Notes: Designed with hypermedia systems in mind

---

#### Installation

It's a script!

`164 kb` (`50 kb` minified)

_"Smaller than the favicon"_

----

Put it on your webpage:

```html
<!-- from CDN -->
<script src="https://unpkg.com/htmx.org@2.0.3" integrity="sha384-0895/pl2MU10Hqc6jd4RvrthNlDiE9U1tWmX7WRESftEDRosgxNsQG/Ze9YMRzHq" crossorigin="anonymous"></script>

<!-- or downloaded -->
<script src="/path/to/htmx.min.js"></script>
```

----

Install it with your tool:

```log
npm install htmx.org@2.0.3
```

```javascript
import 'htmx.org';
```

---

#### An Extension of HTML

```html
<a  hx-get="/fish/season/2"
    hx-trigger="click"
    hx-target="#fish-list"
    hx-swap="outerHTML"
>
  Next Season
</a>
```

Notes: Here's our navigation link from before

- added many new, custom attributes
- our href- the resource we're linking to
- `hx-get` make a get request
- `hx-trigger` on click
- take the response and `target` or find the `fish-list`
- `hs-swap` that whole content out

---

What's Going On

- listens for the event
- makes the request with AJAX
- puts the response where you say

Notes:

- all as defined by attributes
- enables using full spectrum of verbs

---

In-line table edits

```html [|3|10-13]
<table class="table delete-row-example">
  <!-- <thead>...</thead> -->
  <tbody hx-target="closest tr" hx-swap="outerHTML">
    @foreach(var fish in Model.Fishes)
    {
      <tr>
        <td>@fish.Name</td>
        <td>@fish.Species</td>
        <td>
          <button hx-get="@Url.Page("/fish/edit", "Row", new { fish.Id })"
                  hx-trigger="click">
            Edit
          </button>
        </td>
      </tr>
    }
  </tbody>
</table>
```

Notes:

- difficult scenario since `form` is invalid inside `tr`
  - could do `colspan` but it'd be difficult to style consistently
- top-level attributes effect children
- button to "get" the edit partial (Row handler)

----

Server-Side (RazorPage)

```csharp [|7,13]
public class Edit(DataTank db) : PageModel
{
    //...
    public PartialViewResult OnGetRow(int id)
    {
        var fish = db.GetFish(id);
        return Partial("EditRow", fish);
    }

    public PartialViewResult OnPutRow(FishModel fish)
    {
        db.EditFish(fish);
        return Partial("Row", fish);
    }
}
```

----

"EditRow" Partial

```html [|2-3|1,5-7|8-11]
<tr hx-trigger='cancel' hx-get="@Url.Page("/fish", "Row", new { Model.Id })">
    <td><input asp-for="Name" /></td>
    <td><input asp-for="Species" /></td>
    <td>
    <button hx-get="@Url.Page("/fish", "Row", new { Model.Id })">
        Cancel
    </button>
    <button hx-put="@Url.Page("/fish/edit", "Row", new { Model.Id })"
            hx-include="closest tr">
        Save
    </button>
    </td>
</tr>
```

Notes:

- inputs in the columns
- cancel buttons _htmx-get_ to replace the "form"
- row listens for cancel too
- save issues a _put_ with `hx-include`

----

Component Re-Use

```html [|6]
<table class="table delete-row-example">
  <!-- <thead>...</thead> -->
  <tbody hx-target="closest tr" hx-swap="outerHTML">
    @foreach(var fish in Model.Fishes)
    {
        @Html.Partial("Row", fish);
    }
  </tbody>
</table>
```

---

Flexible & Low-Level

- requests
  - `hx-get` · `hx-post` · `hx-put` · `hx-delete`
- triggers:
  - `click` · `changed` · `custom` · `polling`
- target:
  - `this` · `selectors` · `closest` · `next`
- swap
  - `innerHTML` · `outerHTML` · `textContent`
  - `beforebegin` · `afterbegin`
  - `beforeend` · `afterend`
  - `delete` · `none`
<!-- .element: class="r-stretch" -->

Notes: HTMX is pretty low level

- request methods for all our ajaxes
- triggers for any valid event, or custom
  - can be filtered by source or target
- target: this, or selectors with nice extensions
- swap: where does it go - neat ones like delete which removes

---

And More

- `hx-boost`
- `hx-push-state`
- `hx-select`
- `hx-vals`

Notes:

- `boost` makes any link or form into an AJAX request and swaps the body
  - prevents flash of unstyled content
  - can put it top-level and override
- `push-state` updates URL/history
- `select` lets you filter down only a certain part of the response
  - handy when partial is extra
- `vals` let you set values on request with JS
- also built-in for loaing/waiting indicators, confirm dialogs, and more

---

### Handling Complexities

```html
<h1>Fish</h1>
<h2>Season 1<h2>
<nav>
  <a disabled>Previous Season</a>
  <a  hx-get="/fish/season/2" hx-trigger="click"
    hx-target="#fish-list" hx-swap="outerHTML">
    Next Season</a>
</nav>
<a href="fish/season/1/add">Add a fish</a>
<ul id="fish-list">
  <li>Thlump <a href="fish/1/feed">Feed</a> <a href="fish/season/1/remove/1">Remove</a></li>
  <li>Old Blue <a href="fish/season/1/remove/2">Remove</a></li>
</ul>
</body>
```

Notes: Here's our example from before, with the new HTMX-powered link

- we're updating the `fish-list`, but what about our other links?

----

Expanding the target

- change `/fish/season/2` to return more than the partial

```html
<a  hx-get="/fish/season/2" hx-trigger="click"
    hx-target="body" hx-swap="outerHTML">
    Next Season</a>
```

----

Just Boost it

```html
<a  href="/fish/season/2" hx-boost>Next Season</a>
```

----

Out-of-Band Swap

- update multiple chunks of html from one response
- add `hx-swap-oob="true"` to other elements
- our response partial:

```html
<ul><!-- fish --></ul>

<h2 id ="subtitle" hx-swap-oob="true">Season @Model.Season<h2>
<nav id="season-nav" hx-swap-oob="true">
  <!-- fresh links -->
</nav>
```

Notes: The `ul` list will be swapped as normal.
The _other_ elements with the magiv `hx-swap-oob` will find their respective IDs and update as well.
Some folks may not like it

----

Dispach Events with Response

- specify `HX-Trigger` response headers to trigger event
- use that event as `hx-trigger` and update

Notes: There's lots of other metadata and actions you can take with both
request and response headers in HTMX. The ASP.NET core extensions make this easy

---

### Going Further

---

- lots of [extensions](https://htmx.org/extensions/)
- [HyperScript](https://hyperscript.org/docs/)
- introducing FE state (e.g. [Alpine JS](https://alpinejs.dev/))
- HTMX JavaScript API

Notes:

- Many extensions... error handling nice things like time-out removal, disabling triggers
- Some are supported by the team
  - preload, websockets/server-sent-events, more
- HyperScript is a standalone JS library inspired by HyperCard
  - enhances in-line JS; DSL helpers
  - I'm not totally sold on it
- you can add other JS libraries... or even do a SPA in a context when it's necessary
- HTMX has a rich JavaScript API for interacting on the code side (not just in HTML)

---

### HTMX ❤️ .NET

---

#### Strong Server-Side Frameworks

ASP.NET Core Is Great

- MVC
- RazorPage / Handlers
- Giraffe (F#)

Notes:

- gets a bad wrap but .NET is pretty great
  - model binding, easy interacting with context, lots of middleware, and the rest
- MVC should be familiar - you end up writing more Actions which return Partials
- Razor Pages are a bit different, wherein you're adding handlers, specifing more `@page` routes
  - (You can use both together)
- Giraffe is a functional-style library atop ASP.NET Core, cleanest
- Carter?
- **All have extensions**

---

#### Templating

- Partials
- TagHelpers + ViewComponents
- Razor Components
- Blazor?

Notes:

These help you write advanced HTML with less code, while still attached to a "Web 1.0" server-returns page-or-partial model.

Blazor static server-side rendering comes _close_ to endpoints returning HTML, but even with custom render code [it gets caught in its own component model](https://andrewlock.net/exploring-the-dotnet-8-preview-rendering-blazor-components-to-a-string/). This article also mentions a library for returning Razor from "Minimal" endpoints called [RazorSlices](https://github.com/DamianEdwards/RazorSlices) which even mentions HTMX as a use-case.

---

#### Hot Reload

- developing hypermedia requires fast restart
- ruled out MVC, Spring, et al of yore
- `dotnet watch` <!-- .element: class="fragment" -->

----

##### dotnet watch

```log
dotnet watch 🔥 Hot reload enabled. For a list of supported edits, see https://aka.ms/dotnet/hot-reload.
  💡 Press "Ctrl + R" to restart.
dotnet watch 🔧 Building...
dotnet watch 🚀 Started
[17:39:01 INF] HTTP GET / responded 302 in 27.4664 ms
[17:39:13 INF] HTTP GET /aquariums/1/fish/ responded 200 in 24.6850 ms
dotnet watch ⌚ File changed: ./src/CoinsQuest/Pages/Fish/FishProfileDetail.cshtml.
dotnet watch 🔥 Hot reload of changes succeeded.
[17:40:29 INF] HTTP GET /aquariums/1/fish/ responded 200 in 16.5764 ms
dotnet watch ⌚ File changed: ./src/CoinsQuest/Pages/Fish/FishProfileDetail.cshtml.
dotnet watch 🔥 Hot reload of changes succeeded.
[17:40:29 INF] HTTP GET /aquariums/1/fish/ responded 200 in 17.6160 ms
```
---

## Why

- **simple** workflow
- extremely flexible
- fast, accessible
- expressive to develop

---

## Why Not

---

### HTMX Sucks

![A coffee mug that says "HTMX Sucks"](https://imgproxy.fourthwall.com/6g3D_dCDXkRJ8E0QeAWJWuAc_Ig8SlpG46W_g2u3yoY/w:900/sm:1/enc/NzU1MjIwZTgzMjFl/MjYwZRQlOOmICrFe/PveI4mdQBvUtfzys/K_qw6-DMm7gEI2ai/PKrckr06l1Fug4C7/-_914NQ3M7K0cshc/K55r2XqBMguFKPXx/zHnEIHDHiy1NAJVI/vP5oapBzJxAqt4Be/VohpBEIgmCHQ_0mJ/kskjnzdkWtuLNLS-/F7lgjIEeD-BPzD_T/3rEKdT9S-dQiBOQw/fu7hJnVuiDJVj25f/46f-wmqIiuo.webp)
<!-- .element: class="r-stretch" -->

---

- a [somewhat serious meme](https://htmx.org/essays/htmx-sucks/)
- strong reaction to the new paradigm
- "disruption" of an industry

---

#### Ill-Suited Scenarios

- constant, full-page real-time updates
  - document collabs, games
- **tons** of front-end state

Notes: Otherwise... it works great. And you can always make HTMX the default, and introduce
other frameworks

---

#### HTMX 💔 .NET

- not _the best_ templating
- not _the best_ server endpoints
- annoying magic (like `AntiForgeryToken`s)

Notes:

- Especially tag helpers - Razor is missing the nice component-style syntax
- pretty verbose... minimal APIs don't support templating OOTB
  - hopefully you're organizing code with some mediator
- requires some extra global filters or window-wide handling

---

> javascript fatigue:
>
> longing for a hypertext
>
> already in hand

---

<!-- .slide: data-background-color="#dbd1b3" -->

<div style="color:#5a3d2b;font:normal 2em 'Bungee Shade', cursive;line-height:1em;padding-bottom:2rem">Thanks</div>

<div style="display:flex; align-items: center;">
  <ul style="flex:5">
    <li>Say hi! 👋</li>
    <li style="color:#264653">@pmcvtm</li>
    <li style="color:#264653">patrick@pmcvtm.com</li>
  </ul>
</div>