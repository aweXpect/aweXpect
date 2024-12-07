using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class StringEnumerableShould
{
	public sealed class BeTests
	{
		[Fact]
		public async Task AsWildcard_ShouldNotThrowWhenMatchingWildcard()
		{
			IEnumerable<string> subject = ["foo", "bar", "baz"];
			string[] expected = ["*oo", "*a?", "?a?"];

			async Task Act()
				=> await That(subject).Should().Be(expected).AsWildcard();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task IgnoringLeadingWhiteSpace_ShouldNotThrowWhenOnlyDifferenceIsInLeadingWhiteSpace()
		{
			IEnumerable<string> subject = [" a", "b", "\tc"];
			string[] expected = ["a", " b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringLeadingWhiteSpace();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task IgnoringTrailingWhiteSpace_ShouldNotThrowWhenOnlyDifferenceIsInTrailingWhiteSpace()
		{
			IEnumerable<string> subject = ["a ", "b", "c\t"];
			string[] expected = ["a", "b ", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringTrailingWhiteSpace();

			await That(Act).Should().NotThrow();
		}
	}
}
