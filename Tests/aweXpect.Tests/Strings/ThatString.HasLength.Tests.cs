namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class HasLength
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsNull_ShouldFail()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).HasLength(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have length 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenExpectedLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).HasLength(-1);

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*The expected length must be greater than or equal to zero*")
					.AsWildcard().And
					.WithParamName("expected");
			}

			[Theory]
			[InlineData("", 1)]
			[InlineData("abc", 4)]
			[InlineData(" a b c ", 6)]
			public async Task WhenLengthDiffers_ShouldFail(string subject, int length)
			{
				async Task Act()
					=> await That(subject).HasLength(length);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have length {length},
					              but it did have a length of {subject.Length}:
					                {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData("", 0)]
			[InlineData("abc", 3)]
			[InlineData(" a b c ", 7)]
			public async Task WhenLengthMatches_ShouldSucceed(string subject, int length)
			{
				async Task Act()
					=> await That(subject).HasLength(length);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
