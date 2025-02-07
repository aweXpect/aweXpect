using System;
using System.Collections.Generic;

namespace aweXpect.Equivalency;

/// <summary>
///     Options for equivalency.
/// </summary>
public record EquivalencyOptions : EquivalencyTypeOptions
{
	/// <summary>
	///     Specifies the selector how types should be compared, if not overwritten in the <see cref="CustomOptions" />.
	/// </summary>
	/// <remarks>
	///     Defaults to use the <see cref="EquivalencyDefaults.DefaultComparisonType" />.
	/// </remarks>
	public Func<Type, EquivalencyComparisonType> DefaultComparisonTypeSelector { get; init; } =
		EquivalencyDefaults.DefaultComparisonType;

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
		DefaultComparisonTypeSelector = inner.DefaultComparisonTypeSelector;
	}
}
