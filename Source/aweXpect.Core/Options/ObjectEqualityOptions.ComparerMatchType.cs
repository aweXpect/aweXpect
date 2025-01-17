using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect.Options;

public partial class ObjectEqualityOptions
{
	/// <summary>
	///     Specifies a specific <see cref="IEqualityComparer{T}" /> to use for comparing <see cref="object" />s.
	/// </summary>
	public ObjectEqualityOptions Using(IEqualityComparer<object> comparer)
	{
		_matchType = new ComparerMatchType(comparer);
		return this;
	}

	private sealed class ComparerMatchType(IEqualityComparer<object> comparer) : IObjectMatchType
	{
		#region IEquality Members

		/// <inheritdoc />
		public bool AreConsideredEqual(object? actual, object? expected) => comparer.Equals(actual, expected);

		/// <inheritdoc />
		public string GetExpectation(string expected)
			=> $"be equal to {expected}" + ToString();

		/// <inheritdoc />
		public string GetExtendedFailure(string it, object? actual, object? expected)
			=> $"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}";

		/// <inheritdoc />
		public override string ToString()
			=> $" using {Formatter.Format(comparer.GetType())}";

		#endregion
	}
}
