using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Tests.Core;

public class StringDifferenceTests
{
	[Fact]
	public async Task ShouldCacheIndexOfFirstMismatch()
	{
		const string actual = "Foo";
		const string expected = "Foo";
		ExecuteOnceComparer? comparer = new();

		StringDifference sut = new(actual, expected, comparer);

		await That(sut.IndexOfFirstMismatch).Is(-1);
		await That(sut.IndexOfFirstMismatch).Is(-1);
	}

	[Fact]
	public async Task WhenActualValueIsLongerThanExpected_ShouldDifferAtIndexActualLength()
	{
		const string actual = "A text that is longer";
		const string expected = "A text";

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(6);
		await That(sut.ToString()).Is(
			"""
			differs at index 6:
			         ↓ (actual)
			  "A text that is longer"
			  "A text"
			         ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenActualValueIsNull_ShouldDifferAtIndex0()
	{
		const string? actual = null;
		const string expected = "This is a text";

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(0);
		await That(sut.ToString()).Is(
			"""
			differs at index 0:
			  ↓ (actual)
			  <null>
			  "This is a text"
			  ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenActualValueIsShorterThanExpected_ShouldDifferAtIndexExpectedLength()
	{
		const string actual = "A text";
		const string expected = "A text that is longer";

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(6);
		await That(sut.ToString()).Is(
			"""
			differs at index 6:
			         ↓ (actual)
			  "A text"
			  "A text that is longer"
			         ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenExpectedValueIsNull_ShouldDifferAtIndex0()
	{
		const string actual = "This is a text";
		const string? expected = null;

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(0);
		await That(sut.ToString()).Is(
			"""
			differs at index 0:
			  ↓ (actual)
			  "This is a text"
			  <null>
			  ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenFirstMismatchIsBelow11Characters_ShouldIncludeCompleteTextBeforeFirstMismatch()
	{
		const string actual = "This is a long text";
		const string expected = "This is a text that differs at index 10";

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(10);
		await That(sut.ToString()).Is(
			"""
			differs at index 10:
			             ↓ (actual)
			  "This is a long text"
			  "This is a text that differs at index 10"
			             ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenNoLeadingWordBoundaryExistsBetween5And15Characters_ShouldFallbackTo50Characters()
	{
		const string actual = "This text '_contains' a long word between 5 and 15 characters before the first mismatch";
		const string expected =
			"This text '_contains' a loNg word between 5 and 15 characters before the first mismatch";
		StringDifference sut = new(actual, expected);

		string result = sut.ToString();

		await That(result).Is(
			"""
			differs at index 26:
			              ↓ (actual)
			  "…ains' a long word between 5 and 15 characters before the…"
			  "…ains' a loNg word between 5 and 15 characters before the…"
			              ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenNoTrailingWordBoundaryExistsBetween45And60Characters_ShouldFallbackTo50Characters()
	{
		const string actual = "This text contains a lot of words and is used for testing the WordBoundaryAlgorithm";
		const string expected =
			"This text is used to verify when between 45 'and60characters' no word boundary exists";
		StringDifference sut = new(actual, expected);

		string result = sut.ToString();

		await That(result).Is(
			"""
			differs at index 10:
			             ↓ (actual)
			  "This text contains a lot of words and is used for testing…"
			  "This text is used to verify when between 45 'and60…"
			             ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenStringContainsWhitespace_ShouldPositionArrowsCorrectly()
	{
		const string actual = "foo\rbar\nBAZ";
		const string expected = "foo\rbar\nbaz";

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(8);
		await That(sut.ToString()).Is(
			"""
			differs on line 2 and column 1 (index 8):
			             ↓ (actual)
			  "foo\rbar\nBAZ"
			  "foo\rbar\nbaz"
			             ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenStringsDifferInCaseOnly_ShouldDefaultToCaseSensitiveComparison()
	{
		const string actual = "this IS a text that only differs in casing";
		const string expected = "this is a text that only differs in casing";

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(5);
		await That(sut.ToString()).Is(
			"""
			differs at index 5:
			        ↓ (actual)
			  "this IS a text that only differs in casing"
			  "this is a text that only differs in casing"
			        ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenStringsDifferInCaseOnly_WhenUsingAComparer_ShouldCompareSubstringsWithComparer()
	{
		const string actual = "this IS a text that only differs in casing";
		const string expected = "this is a text that only differs in casing";
		StringComparer? comparer = StringComparer.OrdinalIgnoreCase;

		StringDifference sut = new(actual, expected, comparer);

		await That(sut.IndexOfFirstMismatch).Is(-1);
		await That(sut.ToString()).Is("differs");
	}

	[Fact]
	public async Task WhenTextDiffers_ShouldCalculateIndexOfFirstMismatch()
	{
		const string actual = "this is a long text that differs in between two words";
		const string expected = "this is a long text which differs in between two words";

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(20);
		await That(sut.ToString()).Is(
			"""
			differs at index 20:
			                   ↓ (actual)
			  "…is a long text that differs in between two words"
			  "…is a long text which differs in between two words"
			                   ↑ (expected)
			""");
	}

	[Fact]
	public async Task WhenTextHasMultipleLines_ShouldIncludeLineAndColumnNumbers()
	{
		int expectedIndex = 100 + (3 * Environment.NewLine.Length);
		string nl = Environment.NewLine.DisplayWhitespace();

		string actual = """
		                @startuml
		                Alice -> Bob : Authentication Request
		                Bob --> Alice : Authentication Response
		                Alice -> Bob : Another authentication Request
		                Alice <-- Bob : Another authentication Response
		                @enduml
		                """;

		string expected = """
		                  @startuml
		                  Alice -> Bob : Authentication Request
		                  Bob --> Alice : Authentication Response
		                  Alice -> Bob : Invalid authentication Request
		                  Alice <-- Bob : Another authentication Response
		                  @enduml
		                  """;

		StringDifference sut = new(actual, expected);

		await That(sut.IndexOfFirstMismatch).Is(expectedIndex);
		await That(sut.ToString()).Is(
			$"""
			 differs on line 4 and column 16 (index {expectedIndex}):
			              ↓ (actual)
			   "…-> Bob : Another authentication Request{nl}Alice <-- Bob :…"
			   "…-> Bob : Invalid authentication Request{nl}Alice <-- Bob :…"
			              ↑ (expected)
			 """);
	}

	[Fact]
	public async Task WhenTextIsSame_ShouldSetIndexOfFirstMismatchToNegativeOne()
	{
		const string actual = "this is a text that does not differ";

		StringDifference sut = new(actual, actual);

		await That(sut.IndexOfFirstMismatch).Is(-1);
		await That(sut.ToString()).Is("differs");
	}

	private class ExecuteOnceComparer : IEqualityComparer<string>
	{
		private bool _wasExecuted;

		public bool Equals(string? x, string? y)
		{
			if (!_wasExecuted)
			{
				_wasExecuted = true;
				return string.Equals(x, y);
			}

			throw new NotSupportedException("Comparer was executed more than once");
		}

		public int GetHashCode([DisallowNull] string obj) => obj.GetHashCode();
	}
}
