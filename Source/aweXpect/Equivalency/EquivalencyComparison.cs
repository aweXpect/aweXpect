using System.Collections.Generic;
using System.Text;

namespace aweXpect.Equivalency;

internal static partial class EquivalencyComparison
{
	public static bool Compare<TActual, TExpected>(
		TActual actual,
		TExpected expected,
		CompareOptions options,
		StringBuilder failureBuilder)
		=> Compare(
			actual,
			expected,
			options,
			failureBuilder,
			"",
			MemberType.Value,
			new EquivalencyContext());

	public record CompareOptions
	{
		public bool IgnoreCollectionOrder { get; set; }
		public string[] MembersToIgnore { get; set; } = [];
	}

	private sealed class EquivalencyContext
	{
		/// <summary>
		///     Tracks already compared objects to catch recursions.
		/// </summary>
		public HashSet<object> ComparedObjects { get; } = new();
	}
}
