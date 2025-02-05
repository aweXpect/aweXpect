using System;
using aweXpect.Core;
using aweXpect.Equivalency;
using aweXpect.Options;
using aweXpect.Results;
using EquivalencyOptions = aweXpect.Equivalency.EquivalencyOptions;

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
	public static TSelf Equivalent<TType, TThat, TElement, TSelf>(this ObjectEqualityResult<TType, TThat, TElement, TSelf> result,
		Func<EquivalencyOptions, EquivalencyOptions>? equivalencyOptions = null)
		where TSelf : ObjectEqualityResult<TType, TThat, TElement, TSelf>
	{
		((IOptionsProvider<ObjectEqualityOptions<TElement>>)result).Options.SetMatchType(
			new EquivalencyComparer(EquivalencyOptions.FromCallback(equivalencyOptions)));
		return (TSelf)result;
	}

	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public static ObjectEqualityOptions<TSubject> Equivalent<TSubject>(this ObjectEqualityOptions<TSubject> options,
		EquivalencyOptions equivalencyOptions)
	{
		options.SetMatchType(new EquivalencyComparer(equivalencyOptions));
		return options;
	}
}
