# Presentations

Collection of slide decks of presentations created by Patrick McVeety-Mill!

Slides utilize [reveal.js](https://revealjs.com/) by way of [reveal-md](https://github.com/webpro/reveal-md) for smooth and repeatable translation from markdown to reveal.js.

See the slides live at [slides.loudandabrasive.com](https://slides.loudandabrasive.com) or by reading them as rich text in the [/slides](https://github.com/pmcvtm/presentations/tree/main/slides) directory of the repo (navigate to the desired presentation).

## Usage

### Requirements

- [Node JS](https://nodejs.org/en/) v18 or higher

### Installation

After downloading the source code, `cd` into the root of the repository and run:

```shell
$ npm install
```

### Creating and presenting slides

Slide decks are named conventionally by folder under the `/slides` directory. In order to be found, slide content **must** be saved as markdown in a file at `/slides/presentation-name/readme.md`.

Slides follow formatting outlined as in [reveal-md](https://github.com/webpro/reveal-md#markdown). See documentation there and from [reveal.js](https://revealjs.com/markdown/) for details.

Launch a slide deck for local presentation or while working with:

```shell
$ npm start presentation-name
```

When work is complete, publish a presentation to a static website in the `/publish` directory with:

```shell
$ npm run publish presentation-name
```

All slides on the main branch are output automatically as html to github pages using [GitHub Actions](https://github.com/pmcvtm/presentations/actions/workflows/publish-to-pages.yml).

## License

### Code / Workflows

The small amount of "software" in this project to support publishing presentations is licensed under the [BSD 3-Clause License](/license.txt).

More relevant is the content of the slides, outlined below.

### Slide Contents

<a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-sa/4.0/88x31.png" /></a><br /><span xmlns:dct="http://purl.org/dc/terms/" property="dct:title">All presentations and slide contents </span> by <a xmlns:cc="http://creativecommons.org/ns#" href="https://loudandabrasive.com" property="cc:attributionName" rel="cc:attributionURL">Patrick McVeety-Mill</a> are licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/">Creative Commons Attribution-ShareAlike 4.0 International License</a>.<br />Based on a work at <a xmlns:dct="http://purl.org/dc/terms/" href="https://github.com/pmcvtm/presentations" rel="dct:source">https://github.com/pmcvtm/presentations</a>.

See full license in [this repo](/slides/license.md) or from [creative commons](https://creativecommons.org/licenses/by-sa/4.0/legalcode), as well as their [human-readable shorthand](https://creativecommons.org/licenses/by-sa/4.0/).
