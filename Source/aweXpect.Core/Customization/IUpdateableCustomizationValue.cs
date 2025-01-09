using System;

namespace aweXpect.Customization;

/// <summary>
///     A customization value of type <typeparamref name="TValue" /> that can be updated.
/// </summary>
/// <remarks>
///     This is primarily intended for record types.
/// </remarks>
public interface IUpdateableCustomizationValue<TValue>
{
	/// <summary>
	///     Get the stored <typeparamref name="TValue" />.
	/// </summary>
	TValue Get();

	/// <summary>
	///     Update the stored <typeparamref name="TValue" />.
	/// </summary>
	CustomizationLifetime Update(Func<TValue, TValue> update);
}