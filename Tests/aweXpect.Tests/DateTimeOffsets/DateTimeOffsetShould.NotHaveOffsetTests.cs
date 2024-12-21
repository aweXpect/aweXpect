namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class NotHaveOffset
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenOffsetOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				TimeSpan unexpected = TimeSpan.FromHours(1);

				async Task Act()
					=> await That(subject).Should().NotHaveOffset(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenOffsetOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				TimeSpan unexpected = TimeSpan.FromHours(2);

				async Task Act()
					=> await That(subject).Should().NotHaveOffset(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have offset of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
