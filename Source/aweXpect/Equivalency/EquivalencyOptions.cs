namespace aweXpect.Equivalency;

/// <summary>
///     Options for equivalency.
/// </summary>
public record EquivalencyOptions
{
	/// <summary>
	///     The members that should be ignored when checking for equivalency.
	/// </summary>
	public string[] MembersToIgnore { get; init; } = [];

	/// <summary>
	///     Ignores the order of collections when checking for equivalency.
	/// </summary>
	public bool IgnoreCollectionOrder { get; init; }
}
