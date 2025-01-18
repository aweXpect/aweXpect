namespace aweXpect.Tests;

public sealed partial class ThatDateTimeOffset
{
	public sealed class DoesNotHaveOffset
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenOffsetOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				TimeSpan unexpected = 1.Hours();

				async Task Act()
					=> await That(subject).DoesNotHaveOffset(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenOffsetOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				TimeSpan unexpected = 2.Hours();

				async Task Act()
					=> await That(subject).DoesNotHaveOffset(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have offset of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
