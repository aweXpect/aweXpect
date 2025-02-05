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
	}

	private class Dummy : IThat<string>, IThatIs<string>
	{
		public ExpectationBuilder ExpectationBuilder { get; } = new ManualExpectationBuilder<string>();
	}
}
