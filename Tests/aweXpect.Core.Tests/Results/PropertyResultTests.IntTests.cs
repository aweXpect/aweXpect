using aweXpect.Results;
using aweXpect.Signaling;

namespace aweXpect.Core.Tests.Results;

public sealed partial class PropertyResultTests
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
}
