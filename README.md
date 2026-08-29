[![](https://img.shields.io/github/actions/workflow/status/soenneker/Soenneker.SevenZip.Runner/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/Soenneker.SevenZip.Runner/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/Soenneker.SevenZip.Runner/daily-automatic-update.yml?style=for-the-badge&label=Daily%20Update)](https://github.com/soenneker/Soenneker.SevenZip.Runner/actions/workflows/daily-automatic-update.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/Soenneker.SevenZip.Runner/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/Soenneker.SevenZip.Runner/actions/workflows/codeql.yml)

# Soenneker.SevenZip.Runner

Represents the constants.

> This is an automation runner, not a package intended for application consumption.

## What you get

- `Constants` — Represents the constants.
- `ConsoleHostedService` — Represents the console hosted service.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ConsoleHostedService.StartAsync(cancellationToken)` | Starts the Console Hosted Service and begins its background work. | A task that completes after the Console Hosted Service has started. |
| `ConsoleHostedService.StopAsync(cancellationToken)` | Stops the Console Hosted Service and waits for its background work to finish. | A task that completes after the Console Hosted Service has stopped. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
