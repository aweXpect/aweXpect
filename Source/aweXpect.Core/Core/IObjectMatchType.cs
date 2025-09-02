﻿using System.Threading.Tasks;

namespace aweXpect.Core;

/// <summary>
///     The type defining how two objects are compared.
/// </summary>
public interface IObjectMatchType
{
	/// <summary>
	///     Returns <see langword="true" /> if the two objects <paramref name="actual" /> and <paramref name="expected" /> are
	///     considered equal; otherwise <see langword="false" />.
	/// </summary>
#if NET8_0_OR_GREATER
	ValueTask<bool> AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected);
#else
	Task<bool> AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected);
#endif

	/// <summary>
	///     Get the expectations text.
	/// </summary>
	string GetExpectation(string expected, ExpectationGrammars grammars);

	/// <summary>
	///     Get an extended failure text.
	/// </summary>
	string GetExtendedFailure(string it, ExpectationGrammars grammars, object? actual, object? expected);
}
