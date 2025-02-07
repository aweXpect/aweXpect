namespace aweXpect.Equivalency;

/// <summary>
///     Equivalency options for a specific type.
/// </summary>
public record EquivalencyTypeOptions
{
	/// <summary>
	///     The members that should be ignored when checking for equivalency.
	/// </summary>
	public string[] MembersToIgnore { get; init; } = [];

	/// <summary>
	///     Specifies which fields to include in the object comparison.
	/// </summary>
	public IncludeMembers Fields { get; init; } = IncludeMembers.Public;

	/// <summary>
	///     Specifies which properties to include in the object comparison.
	/// </summary>
	public IncludeMembers Properties { get; init; } = IncludeMembers.Public;

	/// <summary>
	///     Ignores the order of collections when checking for equivalency.
	/// </summary>
	public bool IgnoreCollectionOrder { get; init; }
}
