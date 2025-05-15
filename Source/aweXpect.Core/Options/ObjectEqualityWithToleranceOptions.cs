using System;
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect.Options;

/// <summary>
///     Checks equality of objects with an optional tolerance.
/// </summary>
public class ObjectEqualityWithToleranceOptions<TSubject, TTolerance>(
	Func<TSubject, TSubject, TTolerance, bool> isWithinTolerance,
	Func<TTolerance, string>? toString = null)
	: ObjectEqualityOptions<TSubject>
{
	/// <summary>
	///     Specifies a specific <see cref="IEqualityComparer{T}" /> to use for comparing <see cref="object" />s.
	/// </summary>
	public ObjectEqualityOptions<TSubject> Within(TTolerance tolerance)
	{
		MatchType = new WithinMatchType(tolerance, isWithinTolerance, toString ?? DefaultToleranceFormatter);
		return this;
	}

	private static string DefaultToleranceFormatter(TTolerance tolerance)
		=> $" within {Formatter.Format(tolerance)}";

	private sealed class WithinMatchType(
		TTolerance tolerance,
		Func<TSubject, TSubject, TTolerance, bool> isWithinTolerance,
		Func<TTolerance, string> toString)
		: IObjectMatchType
	{
		#region IEquality Members

		/// <inheritdoc cref="IObjectMatchType.AreConsideredEqual{TSubject, TExpected}(TSubject, TExpected)" />
		public bool AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected)
		{
			if (actual is null && expected is null)
			{
				return true;
			}

			return actual is TSubject typedActual && expected is TSubject typedExpected &&
			       isWithinTolerance(typedActual, typedExpected, tolerance);
		}

		/// <inheritdoc cref="IObjectMatchType.GetExpectation(string, ExpectationGrammars)" />
		public string GetExpectation(string expected, ExpectationGrammars grammars)
			=> $"is {(grammars.HasFlag(ExpectationGrammars.Negated) ? "not " : "")}equal to {expected}" + ToString();

		/// <inheritdoc cref="IObjectMatchType.GetExtendedFailure(string, ExpectationGrammars, object?, object?)" />
		public string GetExtendedFailure(string it, ExpectationGrammars grammars, object? actual, object? expected)
			=> $"{it} was {Formatter.Format(actual, FormattingOptions.Indented())}";

		/// <inheritdoc cref="object.ToString()" />
		public override string ToString()
			=> toString.Invoke(tolerance);

		#endregion
	}
}
