using aweXpect.Options;

namespace aweXpect.Helpers;

internal static class QuantifierHelpers
{
	public static string ToNegatedString(this Quantifier quantifier)
	{
		quantifier.Negate();
		string result = quantifier.ToString();
		quantifier.Negate();
		return result;
	}
}
