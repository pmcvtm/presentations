---
title: 'Refactoring to Functional Patterns in C#'
theme: simple
revealOptions:
  transition: none
  progress: false
  slideNumber: false
css:
- overrides.css
- atom-one-dark-reasonable.min.css
---

# Refactoring to Functional Patterns

(in C#)

Notes: Thanks to Ashish and Daniel for inviting me, and to Laura for setting things up.

---

### Agenda

- resources
- why
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

## Why?

---

### Why This Way?

- (I am a F#anatic **_but_**)
- Work is difficult
- Ease through understanding

Notes:
- Between this and the band Phish, I spell "fan" alternatively more often than not. I love F#
- But that's not the main reason
-


---

## "Functional Patterns"

---

### More Like Functional _Paradigms_

---

### Object Orientation

- polymorphism
-

---

### Functional

-

---

### The Enemies of Understanding

- Mutability
- `null`
- Side Effects

---

## The Tools At Our Disposal

---

### Against Mutability

- property `init`

---

### Against `null`

- ``
- `required` property

---

### Against Mutability


---

## In Action!

---
- questions