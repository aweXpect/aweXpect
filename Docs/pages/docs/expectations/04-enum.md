# Enum

Describes the possible expectations for `enum` values.

## Equality

You can verify that the `enum` is equal to another one or not:

```csharp
enum Colors { Red = 1, Green = 2, Blue = 3}

await Expect.That(Colors.Red).IsEqualTo(Colors.Red)
  .Because("it is 'Red'");
await Expect.That(Colors.Red).IsNotEqualTo(Colors.Blue)
  .Because("it is 'Red'");
```

## Value

You can verify that the `enum` has a given value or not:

```csharp
enum Colors { Red = 1, Green = 2, Blue = 3}

await Expect.That(Colors.Red).HasValue(1)
  .Because("'Red' is 1");
await Expect.That(Colors.Red).DoesNotHaveValue(2)
  .Because("'Red' is 1");
```

## Defined

You can verify that the `enum` has a defined value or not:

```csharp
enum Colors { Red = 1, Green = 2, Blue = 3}

await Expect.That((Colors)3).IsDefined()
  .Because("3 corresponds to 'Blue'");
await Expect.That((Colors)4).IsNotDefined()
  .Because("4 is no valid color");
```

## Flags

You can verify that the `enum` has a specific flag or not:

```csharp
RegexOptions subject = RegexOptions.Multiline | RegexOptions.IgnoreCase;

await Expect.That(subject).HasFlag(RegexOptions.IgnoreCase)
  .Because("it has the 'IgnoreCase' flag");
await Expect.That(subject).DoesNotHaveFlag(RegexOptions.ExplicitCapture)
  .Because("it does not have the 'ExplicitCapture' flag");
```
