﻿using aweXpect.Core;

namespace aweXpect.Options;

internal static class ObjectEqualityOptions
{
	internal static readonly IObjectMatchType EqualsMatch = new EqualsMatchType();

	private sealed class EqualsMatchType : IObjectMatchType
	{
		/// <inheritdoc cref="object.ToString()" />
		public override string? ToString() => "";

		#region IEquality Members

		/// <inheritdoc cref="IObjectMatchType.AreConsideredEqual{TSubject, TExpected}(TSubject, TExpected)" />
		public bool AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected)
			=> Equals(actual, expected);

		/// <inheritdoc cref="IObjectMatchType.GetExpectation(string, ExpectationGrammars)" />
		public string GetExpectation(string expected, ExpectationGrammars grammars)
			=> $"is {(grammars.IsNegated() ? "not " : "")}equal to {expected}";

		/// <inheritdoc cref="IObjectMatchType.GetExtendedFailure(string, ExpectationGrammars, object?, object?)" />
		public string GetExtendedFailure(string it, ExpectationGrammars grammars, object? actual, object? expected)
			=> $"{it} was {Formatter.Format(actual, FormattingOptions.Indented())}";

		#endregion
	}
}

/// <summary>
///     Checks equality of objects.
/// </summary>
public partial class ObjectEqualityOptions<TSubject> : IOptionsEquality<TSubject>
{
	private IObjectMatchType _matchType = ObjectEqualityOptions.EqualsMatch;

	/// <inheritdoc />
	public bool AreConsideredEqual<TExpected>(TSubject? actual, TExpected? expected)
		=> _matchType.AreConsideredEqual(actual, expected);

	/// <summary>
	///     Specifies a new <see cref="IStringMatchType" /> to use for matching two strings.
	/// </summary>
	public void SetMatchType(IObjectMatchType matchType) => _matchType = matchType;

	/// <summary>
	///     Get an extended failure text.
	/// </summary>
	public string GetExtendedFailure(string it, ExpectationGrammars grammars, object? actual, object? expected)
		=> _matchType.GetExtendedFailure(it, grammars, actual, expected);

	/// <summary>
	///     Returns the expectation string, e.g. <c>be equal to {expectedExpression}</c>.
	/// </summary>
	public string GetExpectation(string expectedExpression, ExpectationGrammars grammars)
		=> _matchType.GetExpectation(expectedExpression, grammars);

	/// <inheritdoc />
	public override string? ToString() => _matchType.ToString();
}
