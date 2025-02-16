namespace aweXpect.Equivalency;

/// <summary>
///     Specifies how a type should be compared.
/// </summary>
public enum EquivalencyComparisonType
{
	/// <summary>
	///     Uses <see cref="object.Equals(object)" /> to check if two instances are considered equivalent.
	/// </summary>
	ByValue,

	/// <summary>
	///     Checks all members if they are considered equivalent.
	/// </summary>
	ByMembers,
}
