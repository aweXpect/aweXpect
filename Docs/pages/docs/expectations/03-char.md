# Char

Describes the possible expectations for `char` values.

## Equality

You can verify that the `char` is equal to another one.

```csharp
char subject = 'a';

await Expect.That(subject).IsEqualTo('a');
await Expect.That(subject).IsNotEqualTo('b');
```

## One of

You can verify that the `char` is one of many alternatives.

```csharp
char subject = 'a';

await Expect.That(subject).IsOneOf('a', 'b', 'c');
await Expect.That(subject).IsNotOneOf('x', 'y', 'z');
```

## Is

### An ASCII letter

You can verify that the `char` is an ASCII letter.

```csharp
await Expect.That('a').IsAnAsciiLetter();
```

This verifies that the subject is an ASCII letter
(see [`char.IsAsciiLetter(char)`](https://learn.microsoft.com/de-de/dotnet/api/system.char.isasciiletter)).

### A letter

You can verify that the `char` is a letter.

```csharp
await Expect.That('a').IsALetter();
await Expect.That('乐').IsALetter();
```

This verifies that the subject is categorized as a Unicode letter
(see [`char.IsLetter(char)`](https://learn.microsoft.com/de-de/dotnet/api/system.char.isletter)).

### A number

You can verify that the `char` is a number.

```csharp
await Expect.That('3').IsANumber();
```

This verifies that the subject is categorized as a Unicode number
(see [`char.IsNumber(char)`](https://learn.microsoft.com/de-de/dotnet/api/system.char.isnumber)).

### White-Space

You can verify that the `char` is white-space.

```csharp
await Expect.That('\t').IsWhiteSpace();
```

This verifies that the subject is categorized as white-space
(see [`char.IsWhiteSpace(char)`](https://learn.microsoft.com/de-de/dotnet/api/system.char.iswhitespace)).
