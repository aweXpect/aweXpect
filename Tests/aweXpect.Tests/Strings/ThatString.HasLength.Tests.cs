namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class HasLength
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenActualIsNull_ShouldFail()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).HasLength().EqualTo(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have length equal to 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenExpectedLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).HasLength().EqualTo(-1);

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
					=> await That(subject).HasLength().EqualTo(length);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have length equal to {length},
					              but it had length {subject.Length}
					              """);
			}

			[Theory]
			[InlineData("", 0)]
			[InlineData("abc", 3)]
			[InlineData(" a b c ", 7)]
			public async Task WhenLengthMatches_ShouldSucceed(string subject, int length)
			{
				async Task Act()
					=> await That(subject).HasLength().EqualTo(length);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class NotEqualToTests
		{
			[Fact]
			public async Task WhenActualIsNull_ShouldSucceed()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).HasLength().NotEqualTo(0);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData("", 1)]
			[InlineData("abc", 4)]
			[InlineData(" a b c ", 6)]
			public async Task WhenLengthDiffers_ShouldSucceed(string subject, int length)
			{
				async Task Act()
					=> await That(subject).HasLength().NotEqualTo(length);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData("", 0)]
			[InlineData("abc", 3)]
			[InlineData(" a b c ", 7)]
			public async Task WhenLengthMatches_ShouldFail(string subject, int length)
			{
				async Task Act()
					=> await That(subject).HasLength().NotEqualTo(length);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have length not equal to {length},
					              but it had length {length}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).HasLength().NotEqualTo(-1);

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*The unexpected length must be greater than or equal to zero*")
					.AsWildcard().And
					.WithParamName("unexpected");
			}
		}
	}
}
