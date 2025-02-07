using System;

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

	/// <summary>
	///     Specifies the selector how types should be compared.
	/// </summary>
	/// <remarks>
	///     Defaults to use the <see cref="EquivalencyDefaults.DefaultTypeComparison" />.
	/// </remarks>
	public Func<Type, EquivalencyComparisonType> TypeComparison { get; init; } = EquivalencyDefaults.DefaultTypeComparison;
}
