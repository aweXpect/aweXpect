using System;

namespace aweXpect.Core;

/// <summary>
///     The grammar to use in the expectation text.
/// </summary>
[Flags]
public enum ExpectationGrammars
{
	/// <summary>
	///     The default expectation text.
	/// </summary>
	None,

	/// <summary>
	///     The expectation is nested.
	/// </summary>
	Nested,

	/// <summary>
	///     The expectation should be in plural form.
	/// </summary>
	Plural
}
