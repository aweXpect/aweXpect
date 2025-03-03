using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public sealed partial class StringEqualityOptionsTests
{
	public sealed class Tests
	{
		[Fact]
		public async Task AreConsideredEqual_Null_ShouldReturnFalse()
		{
			StringEqualityOptions sut = new();
			sut.IgnoringLeadingWhiteSpace();
			sut.IgnoringTrailingWhiteSpace();
			sut.IgnoringNewlineStyle();

			bool result = sut.AreConsideredEqual(null, "foo");

			await That(result).IsFalse();
		}

		[Fact]
		public async Task GetExtendedFailure_Null_ShouldReturnItWasNull()
		{
			StringEqualityOptions sut = new();
			sut.IgnoringLeadingWhiteSpace();
			sut.IgnoringTrailingWhiteSpace();
			sut.IgnoringNewlineStyle();

			string result = sut.GetExtendedFailure("it", ExpectationGrammars.None, null, "foo");

			await That(result).IsEqualTo("it was <null>");
		}

		[Fact]
		public async Task ToString_WhenCaseAndNewlineStyleIsIgnored_ShouldIncludeOptions()
		{
			StringEqualityOptions sut = new();
			sut.IgnoringCase().IgnoringNewlineStyle();

			string result = sut.ToString();

			await That(result).IsEqualTo(" ignoring case and newline style");
		}

		[Fact]
		public async Task ToString_WhenCaseAndWhiteSpaceAndNewlineStyleIsIgnored_ShouldIncludeOptions()
		{
			StringEqualityOptions sut = new();
			sut.IgnoringCase().IgnoringLeadingWhiteSpace()
				.IgnoringTrailingWhiteSpace()
				.IgnoringNewlineStyle();

			string result = sut.ToString();

			await That(result).IsEqualTo(" ignoring case, white-space and newline style");
		}

		[Fact]
		public async Task ToString_WhenWhiteSpaceAndNewlineStyleIsIgnored_ShouldIncludeOptions()
		{
			StringEqualityOptions sut = new();
			sut.IgnoringLeadingWhiteSpace()
				.IgnoringTrailingWhiteSpace()
				.IgnoringNewlineStyle();

			string result = sut.ToString();

			await That(result).IsEqualTo(" ignoring white-space and newline style");
		}
	}
}
