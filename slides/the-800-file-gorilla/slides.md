---
title: 'The 800 File Gorilla: Lessons from Reviewing a Very Big Pull Request'
theme: simple
revealOptions:
  transition: none
  controls: false
  progress: false
css:
- overrides.css
- atom-one-dark-reasonable.min.css
---

# The 800 File Gorilla
_Lessons from Reviewing a Very Big Pull Request_

---

- pr principles
- big pr
- feedback
- tools
- questions

---

<!-- .slide: data-background-color="#dbd1b3" -->
<!-- .slide: data-transition="slide" -->

Patrick McVeety-Mill "is" <!-- .element: style="color:#5a3d2b;font:oblique 1em 'Vibur', sans-serif" -->
<br />**Loud & Abrasive** <!-- .element: style="color:#5a3d2b;font:normal 1.4em 'Bungee Shade', cursive" -->

- he/him <!-- .element: style="color:#e73602" -->
- likes 🥾 ⛺ 🎶 🏊‍♂️ 🎨 🍻 🤙 <!-- .element: style="color:#e5771e" -->
- Engineering Manager at <!-- .element: style="color:#87842c" --> **Accenture** <!-- .element: style="color:#cf47ff" -->
- `@pmcvtm` <!-- .element: style="color:#2a9d8f" -->
- `@loudandabrasive` <!-- .element: style="color:#264653" -->

---

## Pull Request Principles

---

❓ Sidebar: _What is a Pull Request?_

- "let me merge my code into yours"
- typically involves gate-checks + review

![Diagram of circles in a line representing git commits, with another line branching off of "main" into a new line above, then reconnecting with a dotted line into a circle labeled "PR"](assets/pr-graph.png)

Note: In source control, you have a mainline of code changes (or sometimes more than one) that developers "branch" off of to make changes without stepping on each others toes. When the work is done, they merge it back into mainline, but while it _can_ be merged directly, more often it goes through some review and checks first: A Pull Request.

---

📎 well-organized

- code is reasonably grouped
- scope of effort is defined and right
- related changes mostly

Note: As a starting point, it helps if your code is well organized, by project but also function or feature. It's important that the scope of the PR is defined up-front, and ideally "right-sized" between so small it's annoying and so big it's difficult to review.

---

📝 well-documented

- includes high-level description
- commits tell story of approach
- issue, ticket, or documentation for more

Note: Good PRs should include a description of the change, as well as steps to test or remarks about it. This can either be as comments or as a description that may roll-up into a commit message at the end. Speaking of commits, ideally they are incremental and can tell a story about the process, or at least makes parts of the work easy to see separately. Finally it can help to have extra documentation in the form of a linked issue or ticket, or maybe on a docs site if there's more design in play.

---

💭 well-considered

- planned before opening
- open for discussion
- pragmatic decision-making

Note: PRs are planned out either through issue listings or discussion between contributor and maintainer; and they are open to more discussion after opening. Ultimately decisions that we make as part of the PR or during review are more pragmatic than they are perfectionist.

---

👓 well-reviewed

- thorough - high and low level
- code is efficient & matches style
- tested: **functionality over all**

Note: Review of PRs, even for the most experienced and trusted authors, needs to be thorough - both at conceptual and detailed levels. We want to make sure the code is well-written, efficient, and matches our existing codebase. Most importantly, and this is something some lose sight of, that the code behaves as expected, doesn't introduce any regressions.

---

💖 have empathy

for author, reviewer, and readers

---

<!-- .slide: data-background-color="#41658A" -->
## The Big PR  <!-- .element: style="color:#F8FAF6" -->

---

🗺 Lay of the Land

- open source software provider
- most contributions are from their team
- this pull request came from a partner

Note: I was working with an open source software provider, who has a community of implementors that use it and some collaborative partners. But like many OSS projects, contributions mostly are from their team. This PR came from one of those partner orgs.

---

👭 Partner History

- had delivered large changesets
- reasonable success in the past
- semi-rotating cast of developers

Note: This partner has worked with the client before, for large changes like frameworks upgrades and feature overhauls. And they went pretty OK. The changes went through - some collaborations were smoother than others - and there were some lessons along the way, particularly about the size of PRs and how to make incremental changes over time. But they're a company too, with teams assigned to different efforts, so they move around, which meant there wasn't continuity on those lessons between efforts.

---

👋 Starting point

- I joined as new lead with effort underway
- _somewhat_ collaborative rhythms:
  - weekly syncs + questions chat
  - lighter touch than client dev team
- initial plan for smaller chunks had snowballed

Note: Communication was less frequent than as with dev team; our team wasn't _owning_ that effort, but would support the outcome from it. Things were going **OK** but it had become clear the initial hope of several medium-sized PRs was a dream long gone. The changes in question had snowballed to the tune of a changeset of 900 files.

---

### Process

Note: Now that we're in this situation, how do we go about it?

---

#### 1. Acceptance

Note: In a reverse on the stages of grief, the first step is acceptance.

----

<!-- .slide: data-background-image="https://media3.giphy.com/media/l0Iy2MPfW2jmXeLgA/giphy.gif?cid=790b761103740f3d151646b6c893b6ad6984aea3ecde9edd&rid=giphy.gif&ct=g" -->

Note: Look yourself in the mirror and say "This is happening"

---

#### 2. Assessing the Scene

Note: Before we dig in to actually reviewing the PR, we should see what's up at a high level, and then plan our excavation.

---

Create a list of all changes between PR and main

<!-- data-line-numbers="" -->
```shell [1|3-13]
$ git diff --name-status main >> ./pr-files.txt

M  .gitignore
A  .vscode/settings.json
M  MyProject.Common.Tests/MyProject.Common.Tests.csproj
M  MyProject.Common.Tests/PowerShellPreprocessorServiceTests.cs
M  MyProject.Common.Tests/ScriptExtensionsTests.cs
D  MyProject.Common.Tests/app.config
D  MyProject.Common.Tests/packages.config
D  MyProject.Common/App.config
M  MyProject.Common/AzureFileService.cs
R092  MyProject.Web/script.js  MyProject.Web/wwwroot/script.js
:
```

Note: Chances are your code repository's website will crash if you start looking at files in the browser. Thankfully, this is easy with git. We can ask the repo what's different and spit it out as a text file.

---

📊 Dump that text into a spreadsheet and then:

- sort by change <!-- .element: class="fragment" --> (**A**dd,**M**odify,**D**elete,**R**ename)
- sort by 'project' <!-- .element: class="fragment" -->
- pull out readable filenames <!-- .element: class="fragment" -->

Note: Uses Excel's `LEFT=` and `SEARCH` to pull out project names, then `RIGHT` for the file.

----

![Spreadsheet listing file diff with columns [git] "Change", "Full Name", the calculated "Project" name and short "Filename"](assets/diff-spreadsheet.jpg)

Note: There's _a lot_ we are going to do with this! Most importantly we should save this twice, once for posterity and then once again to edit as needed.

---

✅ Translate into a Checklist

```markdown [|1|3-6]
## MyProject.Common

- [ ] Helpers/FileHelper.cs
- [ ] Helpers/IEncryptionKeySettings.cs
- [ ] Helpers/OptionsEncryptionKeyResolver.cs
- [ ] Spreadsheets/GridFormatter.cs
...
```

Note: One of the first things we can do is break out a checklist for ourselves to take notes on.
I broke this out to a markdown file.. we'll see why in a bit, and I pulled out each list of files per project, and did some Find/Replace magic to add the checkbox in front of each. You could also do this as another row in Excel if you're more comfortable with that.

---

👃 Look for Smells

- `R100`
  - directory renames
  - file exoduses
- **A**dd + **D**elete with similar names
- big chunks of **A**dds
- **C**opy changes

Note: We first look for quick things to either throw out, or flag to look at more closely.

Renames show as `R` + the percent difference.  Are these 100%s necessary? Sometimes, but not often. We can assume that we can breeze past reading them when they are needed, since they haven't changed. We also note files that we should _not_ gloss over.

---

<!-- .slide: data-background-color="#b02810" -->

🛑 Stop!

Leave life-easing feedback before proceeding

> &nbsp; &nbsp; **pmcvtm** commented at 10:11 am
>
> Hello 👋 I have started review on this PR and noticed that:
>
> - ⬜ `ProjectX` was renamed to `ProjectY`
> - ⬜ all source files were moved into a new folder
>
> Would you mind renaming and moving those **back** to cut down the diff? We can restore them in a follow-up.
>
<!-- .element: class="pr-comment" -->

Note: Getting ahead of this will make the rest of the review easier. For my situation, the PR went from almost 900 files to 600. Still big, but smaller.

We're also being very deliberate about how we lay this message out, and will dig into that a bit later. But first we're almost done charting a course for our more detailed review.

---

#### 3. Dig In 🍴

- copy spreadsheet to new list
- project by project, file by file
- address additions / deletions, then edits

Note: Begin the tedium, going programmatically through parts of the changeset, by project then files. When looking at the code we can quickly assess additions and deletions, or at least note them before re-examining them in another context, before looking at each actual modified piece of code.

---

🔎 Examine the Modified Code

```shell
$ git difftool -d main MyProject.Common
```
![DiffTool showing side-by-side list of file details in "MyProject.Common" folder which are highlighted in different colors to show the kind of difference between them](assets/difftool-directories.jpg) <!-- .element: class="fragment" -->

Note: We can use git to open a nice tool for identifying the differences in the changeset. There are lots of good ones out there, I use BeyondCompare. Hopefully yours has an "expand all" option, and hopefully too they show up in the same order as our checklist. And this is really where the meat of the work starts.

---

🐛 Check for Quality and Behavior

- read through edits looking for **critical** differences
- quickly note format or style issues
- manually test related features before moving on

Note: We can now look through each file diff for the changes _that matter_. Look for functional changes, try to get an understanding, and quickly note and move past any style or non-functional changes.

Most important too is that while we go through code, we are manually testing functionality as we go. It doesn't matter if the code _looks_ right, or nicer even, if the behavior isn't as expected.

---

🧰 Use All Your Tools

- don't get stuck viewing diffs
- leverage features to paint a picture
- use what you know

Note: We may be switching here between the Diff Tool, "final" code to get highlighting and navigate easier, git tool for commit history... Maybe you're looking at these diffs in a fancy IDE to reduce switching. I wasn't using Rider at the time but I think it would be pretty good at this.

----

Start with the file diff

![Diff tool showing changes in a file side-by-side with different rows highlighted](assets/tools-file-diff.jpg)

Note: This is the bread and butter of reviewing files - try to use one that diffs whitespace differently than other changes.

----

See historical context in git

![Git GUI tools showing list of commits in a tree format with date, message, and author](assets/tools-git-history.jpg)

Note: We can look in our favorite Git GUI to see how this file changed over time or what the thought process behind a change was

----

Navigate and analyze code

![Git GUI tools showing list of commits in a tree format with date, message, and author](assets/tools-code-ide.jpg)

Note: Use your IDE to look at code more deeply, especially when it's new. Some applications may allow you to do all these things from the same app, so if that fits for you, do it.

---

📝 Track and Note in Your Checklist

```markdown [|3|4-5|6-8|9-10|]
## MyProject.Common

- [x] Helpers/FileHelper.cs
- [x] Helpers/IEncryptionKeySettings.cs
  - [ ] L11-12 private member formatting
- [x] Helpers/OptionsEncryptionKeyResolver.cs
  - [ ] Unit tests not translated
  - _How is this registered?_
- [x] Spreadsheets/GridFormatter.cs
  - [ ] Broken for .csv
```

Note: So here's where it was nice to have my checklist as a text document: I can now add sub-bullets for each item that needs addressing, or when there's a note to look at something else. You could still use Excel for this, but since this is Markdown, it will be easy to plug into the repository's website for formatting.

---

#### 3. Give Feedback

---

Tell the author how they did:

> &nbsp; &nbsp; **pmcvtm** commented at 6:28 pm
>
> Hi again 👋 sorry it's been a month. Here's my review:
>
> ⛔ **REJECTED NEEDS FIXES** ⛔
>
> There are **lots** of changes we need to do before this merges.
>
> I will follow up but closing for now, thanks. 😉
<!-- .element: class="pr-comment" -->

Note: As expected, there were lots of things to fix. But this is **not** the way we want to communicate that out.

---

## Feedback

Note: Let's talk about what **would** be a good way to deliver this feedback. Some of this is for big PRs especially, but others are more general.

---

🔁 Be incremental

- don't wait until the end
- file-by-file, **project-by-project**
- catch patterns and give a heads up

Note: Like a lot of "agile" processes, giving _pieces_ of feedback as you go is nicer for the dev (probably, or they can decide to wait if that's their style) and allows you to set a stage for them for what they might expect. If you see something repeating, let them know that it will need to be fixed in other places, too.

---

✅ Be specific and actionable

_small PRs have in-line resolvable comments_

- list files and line numbers
- leave check-able boxes
- communicate priority and importance

Note: Since we can't use fancy in-line comments right alongside the code, we need to be clear about what we're referring to.

---

🔢 Prioritizing with [MoSCoW](https://en.wikipedia.org/wiki/MoSCoW_method) (MuSh CouWt?)

- **MUST** do before merging
- **SHOULD** do or create debt
- **COULD** do to be nice
- **WON'T** do even if we'd like

also **QUESTION** and **COMMENT**

---

> &nbsp; &nbsp; **pmcvtm** commented at 2:26 pm
>
> ### MyProject.Common
>
> - `IEncryptionKeySettings.cs`
>   - ⬜ SHOULD: L11-12 private member formatting <br/>
> - `OptionsEncryptionKeyResolver.cs`
>   - ⬜ MUST: Unit tests not translated over. Even though this is used a little differently in the new framework, we want to preserve the previous test coverage. Let me know if you want to go over anything.
<!-- .element: class="pr-comment" -->

---

🔲 Track against _your_ notes too

```markdown [|5|7,10]
## MyProject.Common

- [x] Helpers/FileHelper.cs
- [x] Helpers/IEncryptionKeySettings.cs
  - [x] L11-12 private member formatting
- [x] Helpers/OptionsEncryptionKeyResolver.cs
  - [?] Unit tests not translated
  - ~~How is this registered?~~
- [x] Spreadsheets/GridFormatter.cs
  - [?] Broken for .csv
```

---

😩 Say "when" (softly)

- give tricky issues back to internal team
- be honest and kind with contributor
- epic/release branches are your friend

Note: There may be some issues you find which require too much technical expertise or domain knowledge to fix. That's OK; don't delay the PR further to get it done, but don't lie to them about things working, either

---

> &nbsp; &nbsp; **pmcvtm** commented at 2:26 pm
>
> ### MyProject.Common
>
> - `IEncryptionKeySettings.cs`
>   - ⬜ SHOULD: L11-12 private member formatting <br/>
> - `OptionsEncryptionKeyResolver.cs`
>   - ⬜ MUST: Unit tests not translated over. Even though this is used a little differently in the new framework, we want to preserve the previous test coverage. Let me know if you want to go over anything.
> - `ProcessOverride.cs`
>    - ❎ I noticed that the Process Override feature is not working with Postgres.
>    - We will file a bug and follow up with it. 
<!-- .element: class="pr-comment" -->

---

🤝 Be diplomatic

- OSS is **publicly viewable**
- don't be demeaning
- don't scare contributors off
- own up to _your_ errors

Note: It's important to be diplomatic with contributor feedback, especially when trust hasn't been built like it may have been with your internal dev team. Also, PRs are publicly visible, and don't include anything about relationships, past discussions, or other context that may inform your communication. Don't scare folks off... Personally I am not a fan of closing PRs without extra communication, even though it is easy. Also it's important to _own up_ when you goof! We're all only human.

---

💝 Have empathy

- be kind
- keep in mind the effort
- remember it's a person on the other end (hopefully you've talked with them, too)

---

## Tools

---

⏸ Formatting and Rules

```csharp [|2|8]
private readonly IService _service;
private readonly IDatabase database;
private readonly ISettings _settings;

public MyDatabase(IService service, IDatabase database, ISettings settings)
{
    _service = service;
    this.database = database;
    _settings = settings;
}
```

Note: What do we notice in this codeblock? "One of these things is not like the other." But often it just has to do with editor defaults.

---

🔡 Guide, automate, or enforce style

- **`.editorconfig`** is cross-language and cross-ide
- prefer auto-formatting to error messages
- go stronger-arm if you want _(but know it's annoying)_

Note: Documented styleguides are nice to read, but only go so far.

---

💬 Communication Channels

- pull request interface is great for tracking
- guide usage with description templates
- face-to-face, async chat are good for discussion but not always possible

---

🔨 Break It Down

- smaller PRs are easier to review
- early feedback is less likely to repeat
- big PRs can only exist on big codebases 😉

---

↩ Alternatives to PRs

- small teams + pairing
- trust, tests, and recovery

---

# Question

