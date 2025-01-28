#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatTimeOnly
{
	public sealed class HasMillisecond
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMillisecond().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have millisecond equal to <null>,
					              but it had millisecond 345
					              """);
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsDifferent_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMillisecond().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have millisecond equal to {Formatter.Format(expected)},
					              but it had millisecond 345
					              """);
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsTheSame_ShouldSucceed()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int expected = 345;

				async Task Act()
					=> await That(subject).HasMillisecond().EqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}
		
		public sealed class NotEqualToTests
		{
			[Fact]
			public async Task WhenMillisecondOfSubjectIsDifferent_ShouldSucceed()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int? unexpected = 12;

				async Task Act()
					=> await That(subject).HasMillisecond().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsTheSame_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int unexpected = 345;

				async Task Act()
					=> await That(subject).HasMillisecond().NotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have millisecond not equal to {Formatter.Format(unexpected)},
					              but it had millisecond 345
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).HasMillisecond().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
