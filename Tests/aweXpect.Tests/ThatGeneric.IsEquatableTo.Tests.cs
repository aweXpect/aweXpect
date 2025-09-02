namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	public sealed class IsEquatableTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenComparingToMatchingLong_ShouldSucceed()
			{
				Wrapper subject = new(1);

				async Task Act()
					=> await That(subject).IsEquatableTo(1L);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenComparingToMatchingWrapper_ShouldSucceed()
			{
				Wrapper subject = new(1);
				Wrapper expected = new(1);

				async Task Act()
					=> await That(subject).IsEquatableTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenComparingToNotMatchingLong_ShouldFail()
			{
				Wrapper subject = new(1);

				async Task Act()
					=> await That(subject).IsEquatableTo(2L);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equatable to 2,
					             but it was ThatGeneric.IsEquatableTo.Wrapper {
					               Value = 1
					             }
					             """);
			}

			[Fact]
			public async Task WhenComparingToNotMatchingWrapper_ShouldFail()
			{
				Wrapper subject = new(1);
				Wrapper expected = new(3);

				async Task Act()
					=> await That(subject).IsEquatableTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equatable to ThatGeneric.IsEquatableTo.Wrapper {
					               Value = 3
					             },
					             but it was ThatGeneric.IsEquatableTo.Wrapper {
					               Value = 1
					             }
					             """);
			}
		}

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenComparingToMatchingLong_ShouldFail()
			{
				Wrapper subject = new(1);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsEquatableTo(1L));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equatable to 1,
					             but it was in ThatGeneric.IsEquatableTo.Wrapper {
					               Value = 1
					             }
					             """);
			}

			[Fact]
			public async Task WhenComparingToMatchingWrapper_ShouldFail()
			{
				Wrapper subject = new(1);
				Wrapper expected = new(1);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsEquatableTo(expected));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equatable to ThatGeneric.IsEquatableTo.Wrapper {
					               Value = 1
					             },
					             but it was in ThatGeneric.IsEquatableTo.Wrapper {
					               Value = 1
					             }
					             """);
			}

			[Fact]
			public async Task WhenComparingToNotMatchingLong_ShouldSucceed()
			{
				Wrapper subject = new(1);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsEquatableTo(2L));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenComparingToNotMatchingWrapper_ShouldSucceed()
			{
				Wrapper subject = new(1);
				Wrapper expected = new(3);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsEquatableTo(expected));

				await That(Act).DoesNotThrow();
			}
		}

		private readonly struct Wrapper(long value)
			: IEquatable<Wrapper>,
				IEquatable<long>
		{
			public long Value { get; } = value;

			public bool Equals(Wrapper other)
				=> Value == other.Value;

			public bool Equals(long other)
				=> Value == other;
		}
	}
}
