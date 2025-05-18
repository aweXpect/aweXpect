# Multiple expectations

You can combine multiple expectations in different ways:

## On the same property

Simply use `.And` or `.Or` to combine multiple expectations, e.g.

```csharp
string subject = "something different"
await Expect.That(subject).StartsWith("some").And.EndsWith("text");
```

> ```
> Expected subject to
> start with "some" and end with "text",
> but it was "something different"
> ```

## On different properties of the same subject

Use the `For`-syntax to access different properties of a common subject and combine them again with `.And` or `.Or`,
e.g.

```csharp
  public record MyClass(int Status, string Content);
  MyClass subject = new(1, "some other content");
  
  await Expect.That(subject)
    .For(x => x.Status, x => x.IsGreaterThan(1)).And
    .For(x => x.Content, x => x.Is("some content"));
```

> ```
> Expected subject to
> for .Status be greater than 1 and for .Content be equal to "some content",
> but .Status was 1 and .Content was "some other content" which differs at index 5:
>         ↓ (actual)
>   "some other content"
>   "some content"
>         ↑ (expected)
> ```

## On different subjects

Use the `Expect.ThatAll` or `Expect.ThatAny` syntax to combine arbitrary expectations, e.g.

```csharp
  string subjectA = "ABC";
  string subjectB = "XYZ";
  
  await Expect.ThatAll(
    Expect.That(subjectA).Is("ABC"),
    Expect.That(subjectB).Is("DEF"));
```

> ```
> Expected all of the following to succeed:
>  [01] Expected subjectA to be equal to "ABC"
>  [02] Expected subjectB to be equal to "DEF"
> but
>  [02] it was "XYZ" which differs at index 0:
>          ↓ (actual)
>         "XYZ"
>         "DEF"
>          ↑ (expected)
> ```
