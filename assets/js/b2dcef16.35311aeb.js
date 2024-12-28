"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[7816],{8108:t=>{t.exports=JSON.parse('{"archive":{"blogPosts":[{"id":"why-another-assertion-library","metadata":{"permalink":"/aweXpect/blog/why-another-assertion-library","editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/blog/2024-11-29-first-blog-post.md","source":"@site/blog/2024-11-29-first-blog-post.md","title":"Why another assertion library","description":"I am an active contributor to fluentassertions and am well aware of the great improvements it has on the readability of unit tests.","date":"2024-11-29T00:00:00.000Z","tags":[],"readingTime":0.93,"hasTruncateMarker":true,"authors":[{"name":"Valentin Breu\xdf","title":"Maintainer of aweXpect","url":"https://github.com/vbreuss","page":{"permalink":"/aweXpect/blog/authors/vbreuss"},"socials":{"github":"https://github.com/vbreuss","linkedin":"https://www.linkedin.com/in/vbreuss/"},"imageURL":"https://github.com/vbreuss.png","key":"vbreuss"}],"frontMatter":{"slug":"why-another-assertion-library","title":"Why another assertion library","authors":["vbreuss"],"tags":[]},"unlisted":false},"content":"I am an active contributor to [fluentassertions](https://github.com/fluentassertions/fluentassertions) and am well aware of the great improvements it has on the readability of unit tests.\\nBut I am also aware of its shortcomings.\\n\\n\x3c!-- truncate --\x3e\\n\\n1. **Limited async support**  \\n   It\'s async support is very limited, e.g.\\n   - The [issue](https://github.com/fluentassertions/fluentassertions/issues/1213) to add support for `IAsyncEnumerable<T>` is open since 2019\\n   - It is not possible to access the `Content` of an `HttpResponseMessage` without blocking\\n\\n2. **No `params`**  \\n   Because the `because` parameter is part of each call, you cannot use a `params` parameter.\\n   As a workaround the `because` parameter was omitted in [`BeOneOf(params string[] validValues)`](https://github.com/fluentassertions/fluentassertions/blob/6.12.2/Src/FluentAssertions/Primitives/StringAssertions.cs#L70), but this leads to an inconsistent API.\\n\\nThat was the reason why I tried myself on a new concept which was also inspired by the approach from [TUnit](https://github.com/thomhurst/TUnit) to make every assertion asynchronous.\\n\\nThis allows to delay the evaluation of the assertion until the call is awaited, which in turn allows to move specifying a because reason to a separate call.  \\nAn additional benefit is, that the constraints themselves can decide if they need to be asynchronous, which allows mixing of sync and async constraints."}]}}')}}]);