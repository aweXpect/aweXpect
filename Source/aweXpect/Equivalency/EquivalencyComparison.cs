using System.Collections.Generic;
using System.Text;

namespace aweXpect.Equivalency;

internal static partial class EquivalencyComparison
{
	public static bool Compare<TActual, TExpected>(
		TActual actual,
		TExpected expected,
		EquivalencyOptions options,
		StringBuilder failureBuilder)
		=> Compare(
			actual,
			expected,
			options,
			options.GetTypeOptions(actual?.GetType(), options),
			failureBuilder,
			"",
			MemberType.Value,
			new EquivalencyContext());

	private sealed class EquivalencyContext
	{
		/// <summary>
		///     Tracks already compared objects to catch recursions.
		/// </summary>
		public HashSet<object> ComparedObjects { get; } = new();
	}
}
