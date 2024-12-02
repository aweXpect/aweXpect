namespace aweXpect.Core;

/// <summary>
///     Determines if two objects are considered equal.
/// </summary>
public interface IOptionsEquality<in T>
{
	/// <summary>
	///     Returns <see langword="true" /> if the two objects <paramref name="a" /> and <paramref name="b" /> are considered
	///     equal; otherwise <see langword="false" />.
	/// </summary>
	bool AreConsideredEqual(T a, T b);
}
