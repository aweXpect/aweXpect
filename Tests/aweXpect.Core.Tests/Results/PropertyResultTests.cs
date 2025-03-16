using aweXpect.Results;
using aweXpect.Signaling;

namespace aweXpect.Core.Tests.Results;

public sealed class PropertyResultTests
{
	public sealed class IntTests
	{
		[Fact]
		public async Task EqualTo_ShouldTriggerValidation()
		{
			Signaler<int?> signal = new();
			PropertyResult.Int<string> sut = new(new Dummy(), _ => 0, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.EqualTo(42);

			await That(signal).Signaled().With(e => e == 42);
		}

		[Fact]
		public async Task EqualTo_ShouldVerifyThatActualIsEqualToExpected()
		{
			PropertyResult.Int<MyClass?> sut = MyClass.HasIntValue(42);

			MyClass? result = await sut.EqualTo(42);

			await That(result?.IntValue).IsEqualTo(42);
		}

		[Fact]
		public async Task GreaterThan_ShouldTriggerValidation()
		{
			Signaler<int?> signal = new();
			PropertyResult.Int<string> sut = new(new Dummy(), _ => 0, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.GreaterThan(42);

			await That(signal).Signaled().With(e => e == 42);
		}

		[Fact]
		public async Task GreaterThan_ShouldVerifyThatActualIsGreaterThanExpected()
		{
			PropertyResult.Int<MyClass?> sut = MyClass.HasIntValue(42);

			MyClass? result = await sut.GreaterThan(41);

			await That(result?.IntValue).IsEqualTo(42);
		}

		[Fact]
		public async Task GreaterThanOrEqualTo_ShouldTriggerValidation()
		{
			Signaler<int?> signal = new();
			PropertyResult.Int<string> sut = new(new Dummy(), _ => 0, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.GreaterThanOrEqualTo(42);

			await That(signal).Signaled().With(e => e == 42);
		}

		[Fact]
		public async Task GreaterThanOrEqualTo_ShouldVerifyThatActualIsGreaterThanOrEqualToExpected()
		{
			PropertyResult.Int<MyClass?> sut = MyClass.HasIntValue(42);

			MyClass? result = await sut.GreaterThanOrEqualTo(42);

			await That(result?.IntValue).IsEqualTo(42);
		}

		[Fact]
		public async Task LessThan_ShouldTriggerValidation()
		{
			Signaler<int?> signal = new();
			PropertyResult.Int<string> sut = new(new Dummy(), _ => 0, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.LessThan(42);

			await That(signal).Signaled().With(e => e == 42);
		}

		[Fact]
		public async Task LessThan_ShouldVerifyThatActualIsLessThanExpected()
		{
			PropertyResult.Int<MyClass?> sut = MyClass.HasIntValue(42);

			MyClass? result = await sut.LessThan(43);

			await That(result?.IntValue).IsEqualTo(42);
		}

		[Fact]
		public async Task LessThanOrEqualTo_ShouldTriggerValidation()
		{
			Signaler<int?> signal = new();
			PropertyResult.Int<string> sut = new(new Dummy(), _ => 0, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.LessThanOrEqualTo(42);

			await That(signal).Signaled().With(e => e == 42);
		}

		[Fact]
		public async Task LessThanOrEqualTo_ShouldVerifyThatActualIsLessThanOrEqualToExpected()
		{
			PropertyResult.Int<MyClass?> sut = MyClass.HasIntValue(42);

			MyClass? result = await sut.LessThanOrEqualTo(42);

			await That(result?.IntValue).IsEqualTo(42);
		}

		[Fact]
		public async Task NotEqualTo_ShouldTriggerValidation()
		{
			Signaler<int?> signal = new();
			PropertyResult.Int<string> sut = new(new Dummy(), _ => 0, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.NotEqualTo(42);

			await That(signal).Signaled().With(e => e == 42);
		}

		[Fact]
		public async Task NotEqualTo_ShouldVerifyThatActualIsNotEqualToExpected()
		{
			PropertyResult.Int<MyClass?> sut = MyClass.HasIntValue(42);

			MyClass? result = await sut.NotEqualTo(43);

			await That(result?.IntValue).IsEqualTo(42);
		}
	}

	public sealed class LongTests
	{
		[Fact]
		public async Task EqualTo_ShouldTriggerValidation()
		{
			Signaler<long?> signal = new();
			PropertyResult.Long<string> sut = new(new Dummy(), _ => 0L, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.EqualTo(42L);

			await That(signal).Signaled().With(e => e == 42L);
		}

		[Fact]
		public async Task EqualTo_ShouldVerifyThatActualIsEqualToExpected()
		{
			PropertyResult.Long<MyClass?> sut = MyClass.HasLongValue(42L);

			MyClass? result = await sut.EqualTo(42L);

			await That(result?.LongValue).IsEqualTo(42L);
		}

		[Fact]
		public async Task GreaterThan_ShouldTriggerValidation()
		{
			Signaler<long?> signal = new();
			PropertyResult.Long<string> sut = new(new Dummy(), _ => 0L, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.GreaterThan(42L);

			await That(signal).Signaled().With(e => e == 42L);
		}

		[Fact]
		public async Task GreaterThan_ShouldVerifyThatActualIsGreaterThanExpected()
		{
			PropertyResult.Long<MyClass?> sut = MyClass.HasLongValue(42L);

			MyClass? result = await sut.GreaterThan(41L);

			await That(result?.LongValue).IsEqualTo(42L);
		}

		[Fact]
		public async Task GreaterThanOrEqualTo_ShouldTriggerValidation()
		{
			Signaler<long?> signal = new();
			PropertyResult.Long<string> sut = new(new Dummy(), _ => 0L, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.GreaterThanOrEqualTo(42L);

			await That(signal).Signaled().With(e => e == 42L);
		}

		[Fact]
		public async Task GreaterThanOrEqualTo_ShouldVerifyThatActualIsGreaterThanOrEqualToExpected()
		{
			PropertyResult.Long<MyClass?> sut = MyClass.HasLongValue(42L);

			MyClass? result = await sut.GreaterThanOrEqualTo(42L);

			await That(result?.LongValue).IsEqualTo(42L);
		}

		[Fact]
		public async Task LessThan_ShouldTriggerValidation()
		{
			Signaler<long?> signal = new();
			PropertyResult.Long<string> sut = new(new Dummy(), _ => 0L, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.LessThan(42L);

			await That(signal).Signaled().With(e => e == 42L);
		}

		[Fact]
		public async Task LessThan_ShouldVerifyThatActualIsLessThanExpected()
		{
			PropertyResult.Long<MyClass?> sut = MyClass.HasLongValue(42L);

			MyClass? result = await sut.LessThan(43L);

			await That(result?.LongValue).IsEqualTo(42L);
		}

		[Fact]
		public async Task LessThanOrEqualTo_ShouldTriggerValidation()
		{
			Signaler<long?> signal = new();
			PropertyResult.Long<string> sut = new(new Dummy(), _ => 0L, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.LessThanOrEqualTo(42L);

			await That(signal).Signaled().With(e => e == 42L);
		}

		[Fact]
		public async Task LessThanOrEqualTo_ShouldVerifyThatActualIsLessThanOrEqualToExpected()
		{
			PropertyResult.Long<MyClass?> sut = MyClass.HasLongValue(42L);

			MyClass? result = await sut.LessThanOrEqualTo(42L);

			await That(result?.LongValue).IsEqualTo(42L);
		}

		[Fact]
		public async Task NotEqualTo_ShouldTriggerValidation()
		{
			Signaler<long?> signal = new();
			PropertyResult.Long<string> sut = new(new Dummy(), _ => 0L, "foo", (e, _) =>
			{
				signal.Signal(e);
			});

			_ = sut.NotEqualTo(42L);

			await That(signal).Signaled().With(e => e == 42L);
		}

		[Fact]
		public async Task NotEqualTo_ShouldVerifyThatActualIsNotEqualToExpected()
		{
			PropertyResult.Long<MyClass?> sut = MyClass.HasLongValue(42L);

			MyClass? result = await sut.NotEqualTo(41L);

			await That(result?.LongValue).IsEqualTo(42L);
		}
	}

	private sealed class Dummy : IExpectThat<string>
	{
		public ExpectationBuilder ExpectationBuilder { get; } = new ManualExpectationBuilder<string>(null);
	}

	private sealed class MyClass
	{
		public int IntValue { get; private init; }
		public long LongValue { get; private init; }

		public static PropertyResult.Int<MyClass?> HasIntValue(int intValue)
		{
			MyClass subject = new()
			{
				IntValue = intValue,
			};
#pragma warning disable aweXpect0001
			IThat<MyClass> source = That(subject);
#pragma warning restore aweXpect0001
			return new PropertyResult.Int<MyClass?>(source, a => a?.IntValue, "int value");
		}

		public static PropertyResult.Long<MyClass?> HasLongValue(long longValue)
		{
			MyClass subject = new()
			{
				LongValue = longValue,
			};
#pragma warning disable aweXpect0001
			IThat<MyClass> source = That(subject);
#pragma warning restore aweXpect0001
			return new PropertyResult.Long<MyClass?>(source, a => a?.LongValue, "long value");
		}
	}
}
