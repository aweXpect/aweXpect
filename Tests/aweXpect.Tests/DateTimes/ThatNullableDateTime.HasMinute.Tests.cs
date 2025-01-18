namespace aweXpect.Tests;

public sealed partial class ThatNullableDateTime
{
	public sealed class HasMinute
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsDifferent_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int expected = 14;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				DateTime? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have minute of <null>,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				DateTime? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have minute of 1,
					             but it was <null>
					             """);
			}
		}
	}
}
