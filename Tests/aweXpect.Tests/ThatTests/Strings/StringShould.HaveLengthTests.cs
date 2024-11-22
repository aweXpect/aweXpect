namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class HaveLengthTests
	{
		[Fact]
		public async Task WhenActualIsNull_ShouldFail()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().HaveLength(0);

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().HaveLength(-1);

			await That(Act).Should().Throw<ArgumentOutOfRangeException>()
				.WithParamName("expected").And
				.WithMessage("*The expected length must be greater than or equal to zero*")
				.AsWildcard();
		}

		[Theory]
		[InlineData("", 1)]
		[InlineData("abc", 4)]
		[InlineData(" a b c ", 6)]
		public async Task WhenLengthDiffers_ShouldFail(string subject, int length)
		{
			async Task Act()
				=> await That(subject).Should().HaveLength(length);

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().HaveLength(length);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveLengthTests
	{
		[Fact]
		public async Task WhenActualIsNull_ShouldSucceed()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().NotHaveLength(0);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData("", 1)]
		[InlineData("abc", 4)]
		[InlineData(" a b c ", 6)]
		public async Task WhenLengthDiffers_ShouldSucceed(string subject, int length)
		{
			async Task Act()
				=> await That(subject).Should().NotHaveLength(length);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData("", 0)]
		[InlineData("abc", 3)]
		[InlineData(" a b c ", 7)]
		public async Task WhenLengthMatches_ShouldFail(string subject, int length)
		{
			async Task Act()
				=> await That(subject).Should().NotHaveLength(length);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have length {length},
				              but it did:
				                {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenUnexpectedLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().NotHaveLength(-1);

			await That(Act).Should().Throw<ArgumentOutOfRangeException>()
				.WithParamName("unexpected").And
				.WithMessage("*The unexpected length must be greater than or equal to zero*")
				.AsWildcard();
		}
	}
}
