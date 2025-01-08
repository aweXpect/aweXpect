namespace aweXpect.Core;

/// <summary>
///     The type defining how two objects are compared.
/// </summary>
public interface IObjectMatchType
{
	/// <summary>
	///     Returns <see langword="true" /> if the two objects <paramref name="actual" /> and <paramref name="expected" /> are
	///     considered equal; otherwise <see langword="false" />.
	/// </summary>
	bool AreConsideredEqual(object? actual, object? expected);

	/// <summary>
	///     Get the expectations text.
	/// </summary>
	string GetExpectation(string expected);

	/// <summary>
	///     Get an extended failure text.
	/// </summary>
	string GetExtendedFailure(string it, object? actual, object? expected);
}
