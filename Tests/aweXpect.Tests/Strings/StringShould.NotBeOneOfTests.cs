namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public class NotBeOneOf
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenUnexpectedIsNull_ShouldSucceed(
				int subject)
			{
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotBeOneOf(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData("foo", "bar", "baz")]
			public async Task WhenValueIsDifferentToAllUnexpected_ShouldSucceed(string subject,
				params string?[] unexpected)
			{
				async Task Act()
					=> await That(subject).Should().NotBeOneOf(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData("foo", "bar", "foo", "baz")]
			public async Task WhenValueIsEqualToAnyUnexpected_ShouldFail(string subject,
				params string?[] unexpected)
			{
				async Task Act()
					=> await That(subject).Should().NotBeOneOf(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be one of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
