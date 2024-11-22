using System;

namespace Testably.Expectations.Core;

/// <summary>
///     Extension methods to simplify usage of the <see cref="ExpectationBuilder" />.
/// </summary>
public static class ExpectationBuilderExtensions
{
	/// <summary>
	///     Specifies a constraint that applies to the property selected
	///     by the <paramref name="propertySelector" /> displayed as <paramref name="displayName" />.
	/// </summary>
	public static ExpectationBuilder.PropertyExpectationBuilder<TSource, TTarget>
		ForProperty<TSource, TTarget>(
			this ExpectationBuilder expectationBuilder,
			Func<TSource, TTarget> propertySelector,
			string displayName,
			bool replaceIt = true)
		=> expectationBuilder.ForProperty(
			PropertyAccessor<TSource, TTarget>.FromFunc(propertySelector, displayName),
			replaceIt: replaceIt);
}
