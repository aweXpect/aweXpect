using aweXpect.Core;

namespace aweXpect.Options;

/// <summary>
///     Checks equality of objects.
/// </summary>
public partial class ObjectEqualityOptions : IOptionsEquality<object?>
{
	private static readonly IObjectMatchType EqualsMatch = new EqualsMatchType();
	private IObjectMatchType _matchType = EqualsMatch;

	/// <inheritdoc />
	public bool AreConsideredEqual(object? actual, object? expected)
		=> _matchType.AreConsideredEqual(actual, expected);

	/// <summary>
	///     Specifies a new <see cref="IStringMatchType" /> to use for matching two strings.
	/// </summary>
	public void SetMatchType(IObjectMatchType matchType) => _matchType = matchType;

	/// <summary>
	///     Get an extended failure text.
	/// </summary>
	public string GetExtendedFailure(string it, object? actual, object? expected)
		=> _matchType.GetExtendedFailure(it, actual, expected);

	/// <summary>
	///     Returns the expectation string, e.g. <c>be equal to {expectedExpression}</c>.
	/// </summary>
	public string GetExpectation(string expectedExpression)
		=> _matchType.GetExpectation(expectedExpression);

	/// <inheritdoc />
	public override string? ToString() => _matchType.ToString();
}
