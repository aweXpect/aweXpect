// ReSharper disable UnusedMember.Local

namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	public sealed class CompliesWith
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(1, true)]
			[InlineData(2, false)]
			public async Task WhenValueIsDifferent_ShouldFail(int expectedValue, bool expectSuccess)
			{
				Other subject = new()
				{
					Value = 1,
				};

				async Task Act()
					=> await That(subject).CompliesWith(x => x.IsEquivalentTo(new
					{
						Value = expectedValue,
					}));

				await That(Act).Throws<XunitException>()
					.OnlyIf(!expectSuccess)
					.WithMessage("""
					             Expected that subject
					             is equivalent to new
					             					{
					             						Value = expectedValue,
					             					},
					             but it was Other {
					               Value = 1
					             }
					             """);
			}
		}

		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenGlobalTimeoutIsApplied_ShouldFail()
			{
				int count = 0;
				MyChangingClass subject = new(42);

				async Task Act()
					=> await That(subject).CompliesWith(x => x.IsEquivalentTo(new
					{
						HasWaitedEnough = true,
					})).Within(30.Seconds()).WithTimeout(50.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to new
					             					{
					             						HasWaitedEnough = true,
					             					} within 0:30,
					             but it was MyChangingClass {
					               HasWaitedEnough = False
					             }
					             """);
			}

			[Theory]
			[InlineData(1, false)]
			[InlineData(0, true)]
			[InlineData(-1, true)]
			public async Task WhenIntervalIsNotPositive_ShouldThrowArgumentOutOfRangeException(int intervalSeconds,
				bool shouldThrow)
			{
				Other subject = new();

				async Task Act()
					=> await That(subject).CompliesWith(x => x.IsNotNull()).Within(1.Seconds())
						.CheckEvery(intervalSeconds.Seconds());

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.OnlyIf(shouldThrow)
					.WithParamName("interval").And
					.WithMessage("The interval must be positive*").AsWildcard();
			}

			[Fact]
			public async Task WhenPredicateResultTurnsTrueLaterOn_ShouldSucceed()
			{
				int count = 0;
				MyChangingClass subject = new(2);

				async Task Act()
					=> await That(subject).CompliesWith(x => x.IsEquivalentTo(new
					{
						HasWaitedEnough = true,
					})).Within(1.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(1, false)]
			[InlineData(0, false)]
			[InlineData(-1, true)]
			public async Task WhenTimeoutIsNegative_ShouldThrowArgumentOutOfRangeException(int timeoutSeconds,
				bool shouldThrow)
			{
				Other subject = new();

				async Task Act()
					=> await That(subject).CompliesWith(x => x.IsNotNull()).Within(timeoutSeconds.Seconds());

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.OnlyIf(shouldThrow)
					.WithParamName("timeout").And
					.WithMessage("The timeout must not be negative*").AsWildcard();
			}

			[Fact]
			public async Task WhenTimeoutIsTooShort_ShouldFail()
			{
				int count = 0;
				MyChangingClass subject = new(42);

				async Task Act()
					=> await That(subject).CompliesWith(x => x.IsEquivalentTo(new
					{
						HasWaitedEnough = true,
					})).Within(50.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to new
					             					{
					             						HasWaitedEnough = true,
					             					} within 0:00.050,
					             but it was MyChangingClass {
					               HasWaitedEnough = False
					             }
					             """);
			}

			private class MyChangingClass(int numberOfChanges)
			{
				private int _iterations;
				public bool HasWaitedEnough => _iterations++ >= numberOfChanges;
			}
		}
	}
}
