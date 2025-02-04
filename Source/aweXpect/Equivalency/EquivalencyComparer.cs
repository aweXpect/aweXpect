using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Equivalency;

internal sealed class EquivalencyComparer(EquivalencyOptions equivalencyOptions)
	: IObjectMatchType
{
	private readonly StringBuilder _failureBuilder = new();

	/// <inheritdoc cref="IObjectMatchType.AreConsideredEqual{TSubject, TExpected}(TSubject, TExpected)" />
	public bool AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected)
	{
		if (HandleSpecialCases(actual, expected, _failureBuilder, out bool? specialCaseResult))
		{
			return specialCaseResult.Value;
		}

		return Compare.CheckEquivalent(actual, expected,
			new CompareOptions
			{
				#if DEBUG
				IgnoreCollectionOrder = equivalencyOptions.IgnoreCollectionOrder,
				#endif
				MembersToIgnore = [.. equivalencyOptions.MembersToIgnore]
			},
			_failureBuilder);
	}

	/// <inheritdoc cref="IObjectMatchType.GetExpectation(string)" />
	public string GetExpectation(string expected) => $"be equivalent to {expected}";

	/// <inheritdoc cref="IObjectMatchType.GetExtendedFailure(string, object?, object?)" />
	public string GetExtendedFailure(string it, object? actual, object? expected)
	{
		if (actual is null != expected is null)
		{
			_failureBuilder.Clear();
			_failureBuilder.Append(it);
			_failureBuilder.Append(" was ");
			Formatter.Format(_failureBuilder, actual, FormattingOptions.SingleLine);
			_failureBuilder.Append(" instead of ");
			Formatter.Format(_failureBuilder, expected, FormattingOptions.SingleLine);
			return _failureBuilder.ToString();
		}

		if (_failureBuilder.Length > 0)
		{
			_failureBuilder.Insert(0, " was not:");
			_failureBuilder.Insert(0, it);
			return _failureBuilder.ToString();
		}

		return $"{it} was considered equivalent";
	}

	private static bool HandleSpecialCases<TActual, TExpected>(TActual actual, TExpected expected,
		StringBuilder failureBuilder,
		[NotNullWhen(true)] out bool? isConsideredEqual)
	{
		if (actual is IEqualityComparer actualEqualityComparer)
		{
			isConsideredEqual = actualEqualityComparer.Equals(actual, expected);
			if (isConsideredEqual == false)
			{
				Formatter.Format(failureBuilder, actual, FormattingOptions.SingleLine);
				failureBuilder.Append(" did not equal ");
				Formatter.Format(failureBuilder, expected, FormattingOptions.SingleLine);
			}

			return true;
		}

		if (expected is IEqualityComparer expectedEqualityComparer)
		{
			isConsideredEqual = expectedEqualityComparer.Equals(actual, expected);
			if (isConsideredEqual == false)
			{
				Formatter.Format(failureBuilder, actual, FormattingOptions.SingleLine);
				failureBuilder.Append(" did not equal ");
				Formatter.Format(failureBuilder, expected, FormattingOptions.SingleLine);
			}

			return true;
		}

		isConsideredEqual = null;
		return false;
	}

	public override string ToString() => " equivalent";
}
