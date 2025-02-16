using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect.Options;

public partial class ObjectEqualityOptions<TSubject>
{
	/// <summary>
	///     Specifies a specific <see cref="IEqualityComparer{T}" /> to use for comparing <see cref="object" />s.
	/// </summary>
	public ObjectEqualityOptions<TSubject> Using(IEqualityComparer<object> comparer)
	{
		_matchType = new ComparerMatchType(comparer);
		return this;
	}

	private sealed class ComparerMatchType(IEqualityComparer<object> comparer) : IObjectMatchType
	{
		#region IEquality Members

		/// <inheritdoc cref="IObjectMatchType.AreConsideredEqual{TSubject, TExpected}(TSubject, TExpected)" />
		public bool AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected)
			=> comparer.Equals(actual, expected);

		/// <inheritdoc cref="IObjectMatchType.GetExpectation(string, bool)" />
		public string GetExpectation(string expected, bool negate = false)
			=> $"is {(negate ? "not " : "")}equal to {expected}" + ToString();

		/// <inheritdoc cref="IObjectMatchType.GetExtendedFailure(string, object?, object?)" />
		public string GetExtendedFailure(string it, object? actual, object? expected)
			=> $"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}";

		/// <inheritdoc cref="object.ToString()" />
		public override string ToString()
			=> $" using {Formatter.Format(comparer.GetType())}";

		#endregion
	}
}
