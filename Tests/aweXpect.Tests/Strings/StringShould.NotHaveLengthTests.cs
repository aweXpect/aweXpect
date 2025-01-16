namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public sealed class NotHaveLength
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsNull_ShouldSucceed()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).Should().NotHaveLength(0);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData("", 1)]
			[InlineData("abc", 4)]
			[InlineData(" a b c ", 6)]
			public async Task WhenLengthDiffers_ShouldSucceed(string subject, int length)
			{
				async Task Act()
					=> await That(subject).Should().NotHaveLength(length);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData("", 0)]
			[InlineData("abc", 3)]
			[InlineData(" a b c ", 7)]
			public async Task WhenLengthMatches_ShouldFail(string subject, int length)
			{
				async Task Act()
					=> await That(subject).Should().NotHaveLength(length);

				await That(Act).Does().Throw<XunitException>()
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

				await That(Act).Does().Throw<ArgumentOutOfRangeException>()
					.WithParamName("unexpected").And
					.WithMessage("*The unexpected length must be greater than or equal to zero*")
					.AsWildcard();
			}
		}
	}
}
