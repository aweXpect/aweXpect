using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public class NotEndWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task IgnoringCase_WhenSubjectDoesEndWithExpected_ShouldFail()
			{
				string subject = "some text";
				string expected = "TEXT";

				async Task Act()
					=> await That(subject).Should().NotEndWith(expected).IgnoringCase();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with "TEXT" ignoring case,
					             but it was "some text"
					             """);
			}

			[Fact]
			public async Task IgnoringCase_WhenSubjectDoesNotEndWithWithExpected_ShouldSucceed()
			{
				string subject = "some text";
				string expected = "SOME";

				async Task Act()
					=> await That(subject).Should().NotEndWith(expected).IgnoringCase();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task
				Using_WhenSubjectDoesEndWithExpected_ShouldFail()
			{
				string subject = "some arbitrary text";
				string expected = "tExt";

				async Task Act()
					=> await That(subject).Should().NotEndWith(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with "tExt" using IgnoreCaseForVocalsComparer,
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task
				Using_WhenSubjectDoesNotEndWithWithExpected_ShouldSucceed()
			{
				string subject = "some arbitrary text";
				string expected = "TEXT";

				async Task Act()
					=> await That(subject).Should().NotEndWith(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectDoesEndWithExpected_ShouldFail()
			{
				string subject = "some text";
				string expected = "text";

				async Task Act()
					=> await That(subject).Should().NotEndWith(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with "text",
					             but it was "some text"
					             """);
			}

			[Fact]
			public async Task WhenSubjectDoesNotEndWithWithExpected_ShouldSucceed()
			{
				string subject = "some text";
				string expected = "some";

				async Task Act()
					=> await That(subject).Should().NotEndWith(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
