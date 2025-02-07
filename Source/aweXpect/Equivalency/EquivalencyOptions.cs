using System;
using System.Collections.Generic;

namespace aweXpect.Equivalency;

/// <summary>
///     Options for equivalency.
/// </summary>
public record EquivalencyOptions : EquivalencyTypeOptions
{
	/// <summary>
	///     Specifies the selector how types should be compared.
	/// </summary>
	/// <remarks>
	///     Defaults to use the <see cref="EquivalencyDefaults.DefaultTypeComparison" />.
	/// </remarks>
	public Func<Type, EquivalencyComparisonType> TypeComparison { get; init; } =
		EquivalencyDefaults.DefaultTypeComparison;

	/// <summary>
	///     Custom type-specific equivalency options.
	/// </summary>
	public Dictionary<Type, EquivalencyTypeOptions> CustomOptions { get; init; } = new();
}

/// <summary>
///     Options for equivalency for expected type <typeparamref name="TExpected" />.
/// </summary>
public record EquivalencyOptions<TExpected> : EquivalencyOptions
{
	/// <summary>
	///     Initializes the values with the <paramref name="inner" /> equivalency options.
	/// </summary>
	public EquivalencyOptions(EquivalencyOptions inner)
	{
		MembersToIgnore = inner.MembersToIgnore;
		IgnoreCollectionOrder = inner.IgnoreCollectionOrder;
		TypeComparison = inner.TypeComparison;
	}
}
