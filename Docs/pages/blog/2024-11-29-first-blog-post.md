---
slug: why-another-assertion-library
title: Why another assertion library
authors: [vbreuss]
tags: []
---

I am an active contributor to [fluentassertions](https://github.com/fluentassertions/fluentassertions) and am well aware of the great improvements it has on the readability of unit tests.
But I am also aware of its shortcomings.

<!-- truncate -->

1. **Limited async support**  
   It's async support is very limited, e.g.
   - The [issue](https://github.com/fluentassertions/fluentassertions/issues/1213) to add support for `IAsyncEnumerable<T>` is open since 2019
   - It is not possible to access the `Content` of an `HttpResponseMessage` without blocking

2. **No `params`**  
   Because the `because` parameter is part of each call, you cannot use a `params` parameter.
   As a workaround the `because` parameter was omitted in [`BeOneOf(params string[] validValues)`](https://github.com/fluentassertions/fluentassertions/blob/6.12.2/Src/FluentAssertions/Primitives/StringAssertions.cs#L70), but this leads to an inconsistent API.

That was the reason why I tried myself on a new concept which was also inspired by the approach from [TUnit](https://github.com/thomhurst/TUnit) to make every assertion asynchronous.

This allows to delay the evaluation of the assertion until the call is awaited, which in turn allows to move specifying a because reason to a separate call.  
An additional benefit is, that the constraints themselves can decide if they need to be asynchronous, which allows mixing of sync and async constraints.
