using System;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect.Equivalency;

/// <summary>
///     Extension methods for equivalency.
/// </summary>
public static class EquivalencyExtensions
{
	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public static TSelf Equivalent<TType, TThat, TElement, TSelf>(
		this ObjectEqualityResult<TType, TThat, TElement, TSelf> result,
		Func<EquivalencyOptions, EquivalencyOptions>? options = null)
		where TSelf : ObjectEqualityResult<TType, TThat, TElement, TSelf>
	{
		((IOptionsProvider<ObjectEqualityOptions<TElement>>)result).Options.SetMatchType(
			new EquivalencyComparer(EquivalencyOptionsExtensions.FromCallback(options)));
		return (TSelf)result;
	}

	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	internal static ObjectEqualityOptions<TSubject> Equivalent<TSubject>(this ObjectEqualityOptions<TSubject> options,
		EquivalencyOptions equivalencyOptions)
	{
		options.SetMatchType(new EquivalencyComparer(equivalencyOptions));
		return options;
	}


	internal static void AddEquivalencyContext(this ExpectationBuilder expectationBuilder,
		EquivalencyOptions equivalencyOptions)
		=> expectationBuilder.UpdateContexts(contexts => contexts.Add(
			new ResultContext("Equivalency options",
				_ => Task.FromResult<string?>(equivalencyOptions.ToString()),
				int.MinValue)));
}
