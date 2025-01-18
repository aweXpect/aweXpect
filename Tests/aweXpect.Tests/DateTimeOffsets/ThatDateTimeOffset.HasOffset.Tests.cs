namespace aweXpect.Tests;

public sealed partial class ThatDateTimeOffset
{
	public sealed class HasOffset
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenOffsetOfSubjectIsDifferent_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				TimeSpan expected = 1.Hours();

				async Task Act()
					=> await That(subject).HasOffset(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have offset of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenOffsetOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				TimeSpan expected = 2.Hours();

				async Task Act()
					=> await That(subject).HasOffset(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
