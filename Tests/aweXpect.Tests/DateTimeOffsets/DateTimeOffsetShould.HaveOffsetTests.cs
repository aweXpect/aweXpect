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
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, 2.Hours());
				TimeSpan expected = 1.Hours();

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
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, 2.Hours());
				TimeSpan expected = 2.Hours();

				async Task Act()
					=> await That(subject).Should().HaveOffset(expected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
