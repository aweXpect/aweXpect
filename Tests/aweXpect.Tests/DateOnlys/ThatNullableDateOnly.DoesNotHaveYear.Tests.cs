#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatNullableDateOnly
{
	public sealed class DoesNotHaveYear
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				DateOnly? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveYear(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateOnly? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).DoesNotHaveYear(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateOnly? subject = new(2010, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveYear(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenYearOfSubjectIsDifferent_ShouldSucceed()
			{
				DateOnly? subject = new(2010, 11, 12);
				int? unexpected = 2011;

				async Task Act()
					=> await That(subject).DoesNotHaveYear(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenYearOfSubjectIsTheSame_ShouldFail()
			{
				DateOnly? subject = new(2010, 11, 12);
				int unexpected = 2010;

				async Task Act()
					=> await That(subject).DoesNotHaveYear(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have year of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
#endif
