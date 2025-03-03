namespace aweXpect.Core;

/// <summary>
///     Extension methods on <see cref="ExpectationGrammars" />.
/// </summary>
public static class ExpectationGrammarsExtensions
{
	/// <summary>
	///     Toggles the <see cref="ExpectationGrammars.Negated" /> flag.
	/// </summary>
	public static ExpectationGrammars Negate(this ExpectationGrammars grammars)
		=> grammars ^= ExpectationGrammars.Negated;
}
