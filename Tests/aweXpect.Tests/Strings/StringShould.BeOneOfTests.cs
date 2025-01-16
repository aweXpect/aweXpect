namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public class BeOneOf
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData("foo", "bar", "baz")]
			public async Task WhenValueIsDifferentToAllExpected_ShouldFail(
				string? subject, params string?[] expected)
			{
				async Task Act()
					=> await That(subject).Should().BeOneOf(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be one of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData("foo", "bar", "foo", "baz")]
			public async Task WhenValueIsEqualToAnyExpected_ShouldSucceed(
				string? subject, params string?[] expected)
			{
				async Task Act()
					=> await That(subject).Should().BeOneOf(expected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenValueIsNull_ShouldFail(
				params string?[] expected)
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).Should().BeOneOf(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be one of {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}
		}
	}
}
