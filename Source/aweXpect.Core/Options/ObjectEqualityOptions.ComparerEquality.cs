using System;
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
		_type = new ComparerEquality(comparer);
		return this;
	}

	private sealed class ComparerEquality : IEquality
	{
		private IComparerOptions? _options;
		private readonly IEqualityComparer<object> _comparer;

		public ComparerEquality(IEqualityComparer<object> comparer)
		{
			_comparer = comparer;
			// ReSharper disable once SuspiciousTypeConversion.Global
			if (comparer is IComparerOptions options)
			{
				_options = options;
			}
		}

		#region IEquality Members

		/// <inheritdoc />
		public bool AreConsideredEqual(object? actual, object? expected) => _comparer.Equals(actual, expected);

		/// <inheritdoc />
		public Result AreConsideredEqual(object? actual, object? expected, string it)
			=> new(_comparer.Equals(actual, expected),
				() => _options?.GetExtendedFailure(it, actual, expected)
				      ?? $"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");

		/// <inheritdoc />
		public string GetExpectation(string expectedExpression)
			=> _options?.GetExpectation(expectedExpression)
			   ?? $"be equal to {expectedExpression} using {Formatter.Format(_comparer.GetType())}";

		#endregion
	}
}
