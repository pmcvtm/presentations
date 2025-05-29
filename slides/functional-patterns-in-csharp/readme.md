---
title: 'Refactoring to Functional Patterns in C#'
theme: simple
revealOptions:
  transition: none
  progress: false
  slideNumber: false
css:
- overrides.css
---
<!-- .slide: data-background-color="#A4D4B4" -->


# Refactoring to Functional Patterns

(in C#)

Notes: Thanks to Ashish and Daniel for inviting me, and to Laura for setting things up.

---
<!-- .slide: data-background-color="#A4D4B4" -->

### Agenda

- resources
- "functional patterns"
- tools at our disposal
- in action
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

- MSDN Docs on New C# Features

- F# for fun and profit

Notes: Start with the sources for this talk

- ways to whet your pallette on functional programming
-

---

## Why Refactor?

- to build our own understanding
- to enable automated testing
- to reduce friction with current or impending change

Notes:

Refactoring is changing code without changing business behavior

- Jimmy Bogard has a nice talk about using re- and de-factoring tools just dig around without sharing the changes with anyone
- Often in legacy code, the piece you need to test is caught up in something else
- Refactoring can be grease for the wheels of change, when something has become brittle or sticky as you make edits, or you see it won't scale to "the next thing" you're heading towards

---

### Enemies of Understanding <!-- .element: style="margin-bottom: 0" -->
especially in legacy code  <!-- .element: style="margin: 0 0 3rem 0; font-style: italic" -->

- when does this get set or change?
- is this value safe to access?
- what _else_ is happening here?

---

---
<!-- .slide: data-background-color="#eb9486" -->

### IMPORTANT:
**Judgement-Free Refactoring**

Notes:

- I will be asserting things are good or bad a lot
- This can be subjective, but also they come without judgement
- You never know what the deal was the first time it was implemented - even if git can tell you who (and if you're lucky why)

---
<!-- .slide: data-background-color="#595A88" -->

## Functional ~~Patterns~~ Flavors

Notes: Bringing the things I like from Functional Programming into our OO landscape

---

### Functional Language Features

- immutability
- no `null`s
- 'pure' functions
- function and type composition

---

Why I Like Them

- easier to read and understand
- easier to automated test
- more **confidence** to work with

Notes: 

- our jobs can be difficult enough as it is. So we should do what we can to help each other out. Since there's little appetite for unionizing, writing legible and self-documenting code is the next best thing
- code that's functional-first is easy to differentiate and isolate the important bits for writing tests

---

<!-- .slide: data-background-color="#595A88" -->

## Tools At Our Disposal

---

### Against Mutability

- property `{ init; }`
- `IEnumerable<T>` and readonly collections
- record types

---

### Against `null`

- nullable reference types
- `required` property
- record types + primary constructors

Notes:

- Nullable reference types lets you apply the existing nullable syntax we have for value types to reference types
  - Can be an undertaking for large projects
  - Microsoft does have migration guidance
- Even if you can't do that - you can use the `required` property to force them to be set
  - Makes setting to `null` obvious and weird
- Record types with primary constructors do this under the hood

---

### Composition

- `IEnumerable<T>` and LINQ
- extension methods / members

---

### What We _Won't_ Do

- algebraic "sum/or" types (Discriminated Unions)
  - option "maybe" types (`Some<T>` or `None`)
  - result types (`T` or `TError`)
- wrapper types (`FirstName of string`)
- 'currying' with expression building

Notes:

- These are really excellent features of F# that we won't be dragging into C#
- Implementations of patterns to emulate these are on Nuget
- Can be painful to develop with -- against the grain

---

## In Action!

---
