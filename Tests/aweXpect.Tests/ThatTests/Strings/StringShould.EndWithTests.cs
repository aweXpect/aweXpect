using Testably.Expectations.Tests.TestHelpers;

namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class EndWithTests
	{
		[Theory]
		[InlineData(false)]
		[InlineData(true)]
		public async Task
			IgnoringCase_WhenSubjectEndsWithDifferentCase_ShouldFailUnlessCaseIsIgnored(
				bool ignoreCase)
		{
			string subject = "some arbitrary text";
			string expected = "TEXT";

			async Task Act()
				=> await That(subject).Should().EndWith(expected).IgnoringCase(ignoreCase);

			await That(Act).Should().Throw<XunitException>()
				.OnlyIf(!ignoreCase)
				.WithMessage("""
				             Expected subject to
				             end with "TEXT",
				             but it was "some arbitrary text"
				             """);
		}

		[Fact]
		public async Task
			IgnoringCase_WhenSubjectEndsWithDifferentString_ShouldIncludeIgnoringCaseInMessage()
		{
			string subject = "some arbitrary text";
			string expected = "SOME";

			async Task Act()
				=> await That(subject).Should().EndWith(expected).IgnoringCase();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             end with "SOME" ignoring case,
				             but it was "some arbitrary text"
				             """);
		}

		[Fact]
		public async Task
			Using_WhenSubjectEndsWithIncorrectMatchAccordingToComparer_ShouldIncludeComparerInMessage()
		{
			string subject = "some arbitrary text";
			string expected = "TEXT";

			async Task Act()
				=> await That(subject).Should().EndWith(expected)
					.Using(new IgnoreCaseForVocalsComparer());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             end with "TEXT" using IgnoreCaseForVocalsComparer,
				             but it was "some arbitrary text"
				             """);
		}

		[Fact]
		public async Task
			Using_WhenSubjectEndsWithMatchAccordingToComparer_ShouldSucceed()
		{
			string subject = "some arbitrary text";
			string expected = "tExt";

			async Task Act()
				=> await That(subject).Should().EndWith(expected)
					.Using(new IgnoreCaseForVocalsComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectDoesNotEndWithExpected_ShouldFail()
		{
			string subject = "some arbitrary text";
			string expected = "some";

			async Task Act()
				=> await That(subject).Should().EndWith(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             end with "some",
				             but it was "some arbitrary text"
				             """);
		}

		[Fact]
		public async Task WhenSubjectEndsWithExpected_ShouldSucceed()
		{
			string subject = "some arbitrary text";
			string expected = "text";

			async Task Act()
				=> await That(subject).Should().EndWith(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public class NotEndWithTests
	{
		[Fact]
		public async Task IgnoringCase_WhenSubjectDoesEndWithExpected_ShouldFail()
		{
			string subject = "some text";
			string expected = "TEXT";

			async Task Act()
				=> await That(subject).Should().NotEndWith(expected).IgnoringCase();

			await That(Act).Should().Throw<XunitException>()
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

			await That(Act).Should().NotThrow();
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

			await That(Act).Should().Throw<XunitException>()
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

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectDoesEndWithExpected_ShouldFail()
		{
			string subject = "some text";
			string expected = "text";

			async Task Act()
				=> await That(subject).Should().NotEndWith(expected);

			await That(Act).Should().Throw<XunitException>()
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

			await That(Act).Should().NotThrow();
		}
	}
}
