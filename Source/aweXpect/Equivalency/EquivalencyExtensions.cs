using System;
using aweXpect.Core;
using aweXpect.Equivalency;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable once CheckNamespace
namespace aweXpect;

/// <summary>
///     Extension methods for equivalency.
/// </summary>
public static class EquivalencyExtensions
{
	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public static TSelf Equivalent<TType, TThat, TSelf>(this ObjectEqualityResult<TType, TThat, TSelf> result,
		Func<EquivalencyOptions, EquivalencyOptions>? equivalencyOptions = null)
		where TSelf : ObjectEqualityResult<TType, TThat, TSelf>
	{
		((IOptionsProvider<ObjectEqualityOptions>)result).Options.SetMatchType(
			new EquivalencyComparer(EquivalencyOptions.FromCallback(equivalencyOptions)));
		return (TSelf)result;
	}

	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public static ObjectEqualityOptions Equivalent(this ObjectEqualityOptions options,
		EquivalencyOptions equivalencyOptions)
	{
		options.SetMatchType(new EquivalencyComparer(equivalencyOptions));
		return options;
	}
}
