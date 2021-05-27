# Contribution Guidelines

## Ground rules & expectations

Before we get started, here are a few things we expect from you (and that you should expect from others):

*   Be kind and thoughtful in your conversations around this project. We all come from different backgrounds and
    projects, which means we likely have different perspectives on "how open source is done." Try to listen to others
    rather than convince them that your way is correct.
*   Please ensure that your contribution passes all tests. If there are test failures, you will need to address them
    before we can merge your contribution.
*   When adding content, please consider if it is widely valuable. Please don't add references or links to things you or
    your employer have created as others will do so if they appreciate it.
*   When reporting a vulnerability on the software, please, put in contact with repository maintainers in order to discuss it 
    in a private way.

## Getting Started

So you want to contribute? Great! Here’s a short checklist with the most important points:

- Don’t worry. You are not expected to get everything right on the first attempt, we’ll guide you through it.
- Please check before starting a new issue if it does not exist or has been handeld already.
- Make sure there is an [issue](https://github.com/syncthing/syncthing/issues) that describes the change you want to do. If the thing you want to do does not have an issue yet, please file one before starting work on it. 
- Fork the repository and make your changes in a new branch. If you already have push access to the Syncthing repository, do *not* create a new branch there. We do all changes as pull requests from personal forks.

## Code Review

Commits will generally fall into one of the three categories below, with different requirements on review.

- Trivial:

  A small change or refactor that is obviously correct. These may be pushed without review by any member of the core team. Examples: removing dead code, updating values.

- Minor:

  A new feature, bugfix or refactoring that may need extra eyes on it to weed out mistakes, but is architecturally simple or at least uncontroversial. Minor changes must go through a pull request and can be merged on approval by any other developer on the core or maintainers team. Tests must pass. Examples: fixing a small bug.

- Major:

  A complex new feature or bugfix, a large refactoring, or a change to the underlying architecture of things. A major change must be reviewed by a member of the *maintainers* team. Tests must pass. We recommend that you start by opening an issue first. That way, other people can weigh in on the discussion before you do any work.


The categorization is inherently subjective; we recommend erring on the side of caution - if you are not sure whether a change is *trivial* or merely *minor*, it’s probably minor.

First time contributions from new developers are always major.

## Coding Style

### General

- All text files use Unix line endings. The git settings already present in the repository attempts to enforce this.
- When making changes, follow the brace and parenthesis style of the surrounding code.

## Commits

- The commit message subject should be a single short sentence describing the change, starting with a capital letter but without ending punctuation, and prefixed with the package name most affected by the change.

- Commits that resolve an existing issue must include the issue number as `(fixes #123)` at the end of the commit message subject. A correctly formatted commit message looks like this:

  ```
  lib/dialer: Add env var to disable proxy fallback (fixes #3006)
  ```

- If the commit message subject doesn’t say it all, one or more paragraphs of describing text should be added to the commit message. This should explain why the change is made and what it accomplishes.

- When drafting a pull request, please feel free to add commits with corrections and merge from `master` when necessary. This provides a clear time line with changes and simplifies review. Do not, in general, rebase your commits.

- Pull requests are merged to `master` using squash merge. The “stream of consciousness” set of commits described in the previous point will be reduced to a single commit at merge time.

## Tests

Yes please, do add tests when adding features or fixing bugs. Also, when a pull request is filed a number of automatic tests are run on the code. This includes:

- That the code actually builds and the test suite passes.
- That the commits are based on a reasonably recent `master`.

If the pull request is invasive or scary looking, the full integration test suite can be run as well.

## Branches

- `master` is the main branch containing good code that will end up in the next release. You should base your work on it. It won’t ever be rebased or force-pushed to.
- `vx.y` branches exist to make patch releases on otherwise obsolete minor releases. Should only contain fixes cherry picked from `master`. Don’t base any work on them.
- Other branches are probably topic branches and may be subject to rebasing. Don’t base any work on them unless you specifically know otherwise.

## Tags

All releases are tagged semver style as `vx.y.z`. The maintainer doing the release signs the tag using their GPG key.

## Licensing

All contributions are made under the same APACHE license as the rest of the project.
