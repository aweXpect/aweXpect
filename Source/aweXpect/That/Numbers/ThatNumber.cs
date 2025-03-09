using System.Diagnostics.CodeAnalysis;

namespace aweXpect;

/// <summary>
///     Expectations on numeric values.
/// </summary>
public static partial class ThatNumber
{
	private static bool IsFinite<T>([NotNullWhen(true)] T? value) => value switch
	{
		null => false,
		double d => !double.IsNaN(d),
		float f => !float.IsNaN(f),
		_ => true,
	};
}
