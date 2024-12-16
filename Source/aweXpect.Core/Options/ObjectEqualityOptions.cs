using System;
using aweXpect.Core;

namespace aweXpect.Options;

/// <summary>
///     Checks equality of objects.
/// </summary>
public partial class ObjectEqualityOptions : IOptionsEquality<object?>
{
	private static readonly IEquality EqualsType = new EqualsEquality();
	private IEquality _type = EqualsType;

	/// <inheritdoc />
	public bool AreConsideredEqual(object? actual, object? expected)
		=> _type.AreConsideredEqual(actual, expected);

	/// <summary>
	///     Returns a <see cref="Result" /> that has the result of the comparison and additionally an extended failure string.
	/// </summary>
	public Result AreConsideredEqual(object? actual, object? expected, string it)
		=> _type.AreConsideredEqual(actual, expected, it);

	/// <summary>
	///     Returns the expectation string, e.g. <c>be equal to {expectedExpression}</c>.
	/// </summary>
	public string GetExpectation(string expectedExpression)
		=> _type.GetExpectation(expectedExpression);

	/// <inheritdoc />
	public override string? ToString() => _type.ToString();

	/// <summary>
	///     The result of an equality check.
	/// </summary>
	public readonly struct Result(bool areConsideredEqual, Func<string> failure)
	{
		/// <summary>
		///     Flag indicating if the two values were considered equal.
		/// </summary>
		public bool AreConsideredEqual { get; } = areConsideredEqual;

		/// <summary>
		///     The failure message, when the two values were not equal.
		/// </summary>
		public string Failure { get; } = failure();
	}

	private interface IEquality
	{
		bool AreConsideredEqual(object? actual, object? expected);
		Result AreConsideredEqual(object? actual, object? expected, string it);
		string GetExpectation(string expectedExpression);
	}
}
