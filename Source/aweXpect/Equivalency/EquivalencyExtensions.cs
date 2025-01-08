using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using aweXpect.Core;
using aweXpect.Equivalency;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable once CheckNamespace
namespace aweXpect;

/// <summary>
///     Extension methods for equivalency.
/// </summary>
public static class EquivalencyExtensions
{
	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public static TSelf Equivalent<TType, TThat, TSelf>(this ObjectEqualityResult<TType, TThat, TSelf> result,
		Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null)
		where TSelf : ObjectEqualityResult<TType, TThat, TSelf>
	{
		EquivalencyOptions equivalencyOptions =
			optionsCallback?.Invoke(new EquivalencyOptions()) ?? new EquivalencyOptions();
		((IOptionsProvider<ObjectEqualityOptions>)result).Options.SetMatchType(
			new EquivalencyComparer(equivalencyOptions));
		return (TSelf)result;
	}

	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public static ObjectEqualityOptions Equivalent(this ObjectEqualityOptions options,
		EquivalencyOptions equivalencyOptions)
	{
		options.SetMatchType(new EquivalencyComparer(equivalencyOptions));
		return options;
	}

	private sealed class EquivalencyComparer(EquivalencyOptions equivalencyOptions)
		: IObjectMatchType
	{
		private ComparisonFailure? _firstFailure;

		/// <inheritdoc cref="IObjectMatchType.AreConsideredEqual(object?, object?)" />
		public bool AreConsideredEqual(object? actual, object? expected)
		{
			if (HandleSpecialCases(actual, expected, out bool? specialCaseResult))
			{
				return specialCaseResult.Value;
			}

			List<ComparisonFailure> failures = Compare.CheckEquivalent(actual, expected,
				new CompareOptions
				{
					MembersToIgnore = [.. equivalencyOptions.MembersToIgnore]
				}).ToList();

			if (failures.FirstOrDefault() is { } firstFailure)
			{
				_firstFailure = firstFailure;
				if (firstFailure.Type == MemberType.Value)
				{
					return false;
				}

				return false;
			}

			return true;
		}

		/// <inheritdoc cref="IObjectMatchType.GetExpectation(string)" />
		public string GetExpectation(string expected) => $"be equivalent to {expected}";

		/// <inheritdoc cref="IObjectMatchType.GetExtendedFailure(string, object?, object?)" />
		public string GetExtendedFailure(string it, object? actual, object? expected)
		{
			if (_firstFailure == null)
			{
				return $"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}";
			}

			if (_firstFailure.Type == MemberType.Value)
			{
				return $"{it} was {Formatter.Format(_firstFailure.Actual, FormattingOptions.SingleLine)}";
			}

			StringBuilder sb = new();
			sb.Append(_firstFailure.Type).Append(' ')
				.Append(string.Join(".", _firstFailure.NestedMemberNames))
				.AppendLine(" did not match:");

			sb.Append("  Expected: ");
			Formatter.Format(sb, _firstFailure.Expected);
			sb.AppendLine();

			sb.Append("  Received: ");
			Formatter.Format(sb, _firstFailure.Actual);
			return sb.ToString();
		}

		private static bool HandleSpecialCases(object? a, object? b,
			[NotNullWhen(true)] out bool? isConsideredEqual)
		{
			if (a is IEqualityComparer basicEqualityComparer)
			{
				isConsideredEqual = basicEqualityComparer.Equals(a, b);
				return true;
			}

			if (b is IEqualityComparer expectedBasicEqualityComparer)
			{
				isConsideredEqual = expectedBasicEqualityComparer.Equals(a, b);
				return true;
			}

			if (a is IEnumerable enumerable && b is IEnumerable enumerable2)
			{
				isConsideredEqual =
					enumerable.Cast<object>().SequenceEqual(enumerable2.Cast<object>());
				return true;
			}

			isConsideredEqual = null;
			return false;
		}

		public override string ToString() => " equivalent";
	}
}
