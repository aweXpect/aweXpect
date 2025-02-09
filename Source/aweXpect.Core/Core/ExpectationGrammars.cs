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
	None = 0,

	/// <summary>
	///     The expectation is nested.
	/// </summary>
	Nested = 1 << 1,

	/// <summary>
	///     The expectation should be in plural form.
	/// </summary>
	Plural = 1 << 2,

	/// <summary>
	///     The expectation should be in active voice.
	/// </summary>
	Active = 1 << 3
}
