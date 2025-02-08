using System;

namespace aweXpect.Core;

/// <summary>
///     The grammar to use in the expectation text.
/// </summary>
[Flags]
public enum ExpectationGrammar
{
	/// <summary>
	///     The default expectation text.
	/// </summary>
	Default,

	/// <summary>
	///     The expectation is nested.
	/// </summary>
	Nested,

	/// <summary>
	///     The expectation should be in plural form.
	/// </summary>
	Plural
}
