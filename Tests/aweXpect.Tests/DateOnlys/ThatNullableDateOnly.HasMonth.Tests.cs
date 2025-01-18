#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatNullableDateOnly
{
	public sealed class HasMonth
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly? subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMonth(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsDifferent_ShouldFail()
			{
				DateOnly? subject = new(2010, 11, 12);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMonth(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSame_ShouldSucceed()
			{
				DateOnly? subject = new(2010, 11, 12);
				int expected = 11;

				async Task Act()
					=> await That(subject).HasMonth(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				DateOnly? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMonth(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have month of <null>,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				DateOnly? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).HasMonth(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have month of 1,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
