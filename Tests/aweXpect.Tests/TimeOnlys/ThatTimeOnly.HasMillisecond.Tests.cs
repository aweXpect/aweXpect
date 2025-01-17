#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatTimeOnly
{
	public sealed class HasMillisecond
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMillisecond(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have millisecond of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsDifferent_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMillisecond(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have millisecond of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsTheSame_ShouldSucceed()
			{
				TimeOnly subject = new(10, 11, 12, 345);
				int expected = 345;

				async Task Act()
					=> await That(subject).HasMillisecond(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif
