namespace aweXpect.Tests;

public sealed partial class ThatDateTimeOffset
{
	public sealed class DoesNotHaveSecond
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSecondOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? unexpected = 14;

				async Task Act()
					=> await That(subject).DoesNotHaveSecond(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSecondOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int unexpected = 15;

				async Task Act()
					=> await That(subject).DoesNotHaveSecond(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have second of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveSecond(unexpected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
