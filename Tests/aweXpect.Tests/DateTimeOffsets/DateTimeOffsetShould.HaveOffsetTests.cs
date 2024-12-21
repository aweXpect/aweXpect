namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class HaveOffset
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenOffsetOfSubjectIsDifferent_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				TimeSpan expected = TimeSpan.FromHours(1);

				async Task Act()
					=> await That(subject).Should().HaveOffset(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have offset of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenOffsetOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				TimeSpan expected = TimeSpan.FromHours(2);

				async Task Act()
					=> await That(subject).Should().HaveOffset(expected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
