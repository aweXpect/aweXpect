namespace aweXpect.Tests;

public sealed partial class ThatDateTimeOffset
{
	public sealed class HasMinute
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
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
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
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
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int expected = 14;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
