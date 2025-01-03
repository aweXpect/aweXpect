using System.Collections.Generic;

namespace aweXpect.Core;

/// <summary>
///     The type defining how two strings are compared.
/// </summary>
public interface IStringMatchType
{
	/// <summary>
	///     Get an extended failure text.
	/// </summary>
	string GetExtendedFailure(string it, string? actual, string? pattern, bool ignoreCase,
		IEqualityComparer<string> comparer);

	/// <summary>
	///     Returns <see langword="true" /> if the two objects <paramref name="actual" /> and <paramref name="expected" /> are
	///     considered equal; otherwise <see langword="false" />.
	/// </summary>
	bool Matches(string? actual, string? expected, bool ignoreCase, IEqualityComparer<string> comparer);

	/// <summary>
	///     Get the expectations text.
	/// </summary>
	string GetExpectation(string? expected, bool useActiveGrammaticVoice);
}
