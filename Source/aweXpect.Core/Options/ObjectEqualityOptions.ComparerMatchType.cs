﻿using System.Collections.Generic;
using System.Threading.Tasks;
using aweXpect.Core;

namespace aweXpect.Options;

public partial class ObjectEqualityOptions<TSubject>
{
	/// <summary>
	///     Specifies a specific <see cref="IEqualityComparer{T}" /> to use for comparing <see cref="object" />s.
	/// </summary>
	public ObjectEqualityOptions<TSubject> Using(IEqualityComparer<object> comparer)
	{
		MatchType = new ComparerMatchType(comparer);
		return this;
	}

	private sealed class ComparerMatchType(IEqualityComparer<object> comparer) : IObjectMatchType
	{
		#region IEquality Members

		/// <inheritdoc cref="IObjectMatchType.AreConsideredEqual{TSubject, TExpected}(TSubject, TExpected)" />
#if NET8_0_OR_GREATER
		public ValueTask<bool> AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected)
			=> ValueTask.FromResult(comparer.Equals(actual, expected));
#else
		public Task<bool> AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected)
			=> Task.FromResult(comparer.Equals(actual, expected));
#endif

		/// <inheritdoc cref="IObjectMatchType.GetExpectation(string, ExpectationGrammars)" />
		public string GetExpectation(string expected, ExpectationGrammars grammars)
			=> $"is {(grammars.HasFlag(ExpectationGrammars.Negated) ? "not " : "")}equal to {expected}" + ToString();

		/// <inheritdoc cref="IObjectMatchType.GetExtendedFailure(string, ExpectationGrammars, object?, object?)" />
		public string GetExtendedFailure(string it, ExpectationGrammars grammars, object? actual, object? expected)
			=> $"{it} was {Formatter.Format(actual, FormattingOptions.Indented())}";

		/// <inheritdoc cref="object.ToString()" />
		public override string ToString()
			=> $" using {Formatter.Format(comparer.GetType())}";

		#endregion
	}
}
