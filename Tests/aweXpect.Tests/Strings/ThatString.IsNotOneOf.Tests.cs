namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class IsNotOneOf
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).IsNotOneOf("foo", "bar");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not one of ["foo", "bar"],
					             but it was <null>
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenUnexpectedIsNull_ShouldSucceed(
				string subject)
			{
				string? unexpected = null;

				async Task Act()
					=> await That(subject).IsNotOneOf(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData("foo", "bar", "baz")]
			public async Task WhenValueIsDifferentToAllUnexpected_ShouldSucceed(string subject,
				params string?[] unexpected)
			{
				async Task Act()
					=> await That(subject).IsNotOneOf(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData("foo", "bar", "foo", "baz")]
			public async Task WhenValueIsEqualToAnyUnexpected_ShouldFail(string subject,
				params string?[] unexpected)
			{
				async Task Act()
					=> await That(subject).IsNotOneOf(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not one of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
