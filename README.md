# aweXpect

[![Nuget](https://img.shields.io/nuget/v/aweXpect)](https://www.nuget.org/packages/aweXpect)
[![Build](https://github.com/aweXpect/aweXpect/actions/workflows/build.yml/badge.svg)](https://github.com/aweXpect/aweXpect/actions/workflows/build.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=aweXpect_aweXpect&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=aweXpect_aweXpect)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=aweXpect_aweXpect&metric=coverage)](https://sonarcloud.io/summary/overall?id=aweXpect_aweXpect)
[![Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FaweXpect%2FaweXpect%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/aweXpect/aweXpect/main)

Assert unit tests in natural language using awesome expectations.

## Getting started

1. Install the [`aweXpect`](https://www.nuget.org/packages/aweXpect) nuget package
   ```ps
   dotnet add package aweXpect
   ```

2. Add the following `using` statement:
   ```csharp
   using aweXpect;
   ```
   This brings the static `Expect` class and lots of extension methods into scope.


3. See the [documentation](https://awexpect.github.io/aweXpect/docs/getting-started#write-your-first-expectation) for
   usage scenarios.

## Features

### Async everything

By using async assertions per default, we have a consistent API and other perks:

- Complete async support, e.g. `IAsyncEnumerable` `HttpResponseMessage` or similar async types
- No need to distinguish between `action.Throws()` and `await asyncAction.ThrowsAsync()`
- The evaluation is only triggered after the complete fluent chain is loaded, which has some nice benefits:
	- `Because` can be registered once as a general method that can be applied at the end of the expectation instead of
	  cluttering all methods with the `because` and `becauseArgs` parameters
	- `WithCancellation` can also be registered at the end an applies a `CancellationToken` to all async methods which
	  allows cancellation of `IAsyncEnumerable` evaluations
	- Expectations can be combined directly (via `Expect.ThatAll`) instead of relying on global state (
	  e.g. [assertion scopes](https://fluentassertions.com/introduction#assertion-scopes))

### Extensible

We added lots of extensibility points to allow you to build custom extensions.  
The [aweXpect.Core](https://www.nuget.org/packages/aweXpect.Core/) package is intended to be a stable source for extensions, so that the risk of version conflicts between different extensions can be reduced.

You can extend the functionality for any types, by adding extension methods on `IThat<TType>`.
More information can be found in the [extensibility guide](https://awexpect.github.io/aweXpect/docs/category/extensibility).

### Performant

A focus on performance allows you to execute your tests as fast as possible.  
Special care is taken for the happy case (succeeding tests) to be as performant as possible. See
the [benchmarks](https://awexpect.github.io/aweXpect/benchmarks) for more details.
