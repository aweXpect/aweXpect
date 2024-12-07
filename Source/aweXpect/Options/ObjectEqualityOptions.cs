using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using aweXpect.Core;
using aweXpect.Equivalency;

namespace aweXpect.Options;

/// <summary>
///     Checks equality of objects.
/// </summary>
public class ObjectEqualityOptions : IOptionsEquality<object?>
{
	private static readonly IEquality EqualsType = new EqualsEquality();
	private IEquality _type = EqualsType;

	/// <inheritdoc />
	public bool AreConsideredEqual(object? actual, object? expected)
		=> _type.AreConsideredEqual(actual, expected);

	/// <summary>
	///     Compares the objects via <see cref="object.Equals(object, object)" />.
	/// </summary>
	public ObjectEqualityOptions Equals()
	{
		_type = EqualsType;
		return this;
	}

	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public ObjectEqualityOptions Equivalent(EquivalencyOptions equivalencyOptions)
	{
		_type = new EquivalencyEquality(equivalencyOptions);
		return this;
	}

	/// <summary>
	///     Specifies a specific <see cref="IEqualityComparer{T}" /> to use for comparing <see cref="object" />s.
	/// </summary>
	public ObjectEqualityOptions Using(IEqualityComparer<object> comparer)
	{
		_type = new ComparerEquality(comparer);
		return this;
	}

	internal Result AreConsideredEqual(object? actual, object? expected, string it)
		=> _type.AreConsideredEqual(actual, expected, it);

	internal string GetExpectation(string expectedExpression)
		=> _type.GetExpectation(expectedExpression);

	/// <summary>
	///     The result of an equality check.
	/// </summary>
	public readonly struct Result(bool areConsideredEqual, string failure = "")
	{
		/// <summary>
		///     Flag indicating if the two values were considered equal.
		/// </summary>
		public bool AreConsideredEqual { get; } = areConsideredEqual;

		/// <summary>
		///     The failure message, when the two values were not equal.
		/// </summary>
		public string Failure { get; } = failure;
	}

	private interface IEquality
	{
		bool AreConsideredEqual(object? actual, object? expected);
		Result AreConsideredEqual(object? actual, object? expected, string it);
		string GetExpectation(string expectedExpression);
	}

	private sealed class EquivalencyEquality(EquivalencyOptions equivalencyOptions)
		: IEquality
	{
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

		#region IEquality Members

		/// <inheritdoc />
		public bool AreConsideredEqual(object? actual, object? expected)
		{
			if (HandleSpecialCases(actual, expected, out bool? specialCaseResult))
			{
				return specialCaseResult.Value;
			}

			List<ComparisonFailure> failures = Compare.CheckEquivalent(actual, expected,
				new CompareOptions { MembersToIgnore = [.. equivalencyOptions.MembersToIgnore] }).ToList();

			if (failures.FirstOrDefault() is { } firstFailure)
			{
				if (firstFailure.Type == MemberType.Value)
				{
					return false;
				}

				return false;
			}

			return true;
		}

		/// <inheritdoc />
		public Result AreConsideredEqual(object? actual, object? expected, string it)
		{
			if (HandleSpecialCases(actual, expected, out bool? specialCaseResult))
			{
				return new Result(specialCaseResult.Value,
					$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
			}

			List<ComparisonFailure> failures = Compare.CheckEquivalent(actual, expected,
				new CompareOptions { MembersToIgnore = [.. equivalencyOptions.MembersToIgnore] }).ToList();

			if (failures.FirstOrDefault() is { } firstFailure)
			{
				if (firstFailure.Type == MemberType.Value)
				{
					return new Result(false,
						$"{it} was {Formatter.Format(firstFailure.Actual, FormattingOptions.SingleLine)}");
				}

				return new Result(false, $"""
				                          {firstFailure.Type} {string.Join(".", firstFailure.NestedMemberNames)} did not match:
				                            Expected: {Formatter.Format(firstFailure.Expected)}
				                            Received: {Formatter.Format(firstFailure.Actual)}
				                          """);
			}

			return new Result(true);
		}

		/// <inheritdoc />
		public string GetExpectation(string expectedExpression)
			=> $"be equivalent to {expectedExpression}";

		#endregion
	}

	private sealed class EqualsEquality : IEquality
	{
		#region IEquality Members

		/// <inheritdoc />
		public bool AreConsideredEqual(object? actual, object? expected) => Equals(actual, expected);

		/// <inheritdoc />
		public Result AreConsideredEqual(object? actual, object? expected, string it)
			=> new(Equals(actual, expected),
				$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");

		/// <inheritdoc />
		public string GetExpectation(string expectedExpression)
			=> $"be equal to {expectedExpression}";

		#endregion
	}

	private sealed class ComparerEquality(IEqualityComparer<object> comparer) : IEquality
	{
		#region IEquality Members

		/// <inheritdoc />
		public bool AreConsideredEqual(object? actual, object? expected) => comparer.Equals(actual, expected);

		/// <inheritdoc />
		public Result AreConsideredEqual(object? actual, object? expected, string it)
			=> new(comparer.Equals(actual, expected),
				$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");

		/// <inheritdoc />
		public string GetExpectation(string expectedExpression)
			=> $"be equal to {expectedExpression} using {Formatter.Format(comparer.GetType())}";

		#endregion
	}
}
