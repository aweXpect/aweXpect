---
sidebar_position: 10
---

# Stream

Describes the possible expectations for `Stream` and `BufferedStream`.

## Properties

You can verify, the properties of the `Stream`:
```csharp
Stream subject = new MemoryStream();

await Expect.That(subject).IsReadable();
await Expect.That(subject).IsSeekable();
await Expect.That(subject).IsWritable();
await Expect.That(File.Open("read-only.txt", FileMode.OpenOrCreate, FileAccess.Read)).IsReadOnly()
  .Because("the file was opened with Read access");
await Expect.That(File.Open("write-only.txt", FileMode.OpenOrCreate, FileAccess.Write)).IsWriteOnly()
  .Because("the file was opened with Write access");
```

## Length

You can verify, the length of the `Stream`:

```csharp
Stream subject = new MemoryStream("foo"u8.ToArray());

await Expect.That(subject).HasLength(3);
await Expect.That(subject).DoesNotHaveLength(4);
```

## Position

You can verify, the position of the `Stream`:

```csharp
Stream subject = new MemoryStream("foo"u8.ToArray());
subject.Seek(2, SeekOrigin.Current);

await Expect.That(subject).HasPosition(2);
await Expect.That(subject).DoesNotHavePosition(0);
```

## Buffer size

You can verify, the buffer size of the `BufferedStream`:

```csharp
BufferedStream subject = new(new MemoryStream("foo"u8.ToArray()), 2);

await Expect.That(subject).HasBufferSize(2);
await Expect.That(subject).DoesNotHaveBufferSize(3);
```
