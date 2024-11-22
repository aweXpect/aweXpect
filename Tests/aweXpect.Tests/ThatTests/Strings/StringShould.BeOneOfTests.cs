namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class BeOneOfTests
	{
		[Theory]
		[InlineData("foo", "bar", "baz")]
		public async Task WhenValueIsDifferentToAllExpected_ShouldFail(
			string? subject, params string?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
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

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenValueIsNull_ShouldFail(
			params string?[] expected)
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}
	}

	public class NotBeOneOfTests
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
