using System;

namespace aweXpect.Customization;

/// <summary>
///     Extension methods for <see cref="GlobalCustomization" />.
/// </summary>
public static partial class GlobalCustomizationExtensions
{
	private class CustomizationValue<TValue>(
		Func<TValue> getter,
		Func<TValue, CustomizationLifetime> setter)
		: ICustomizationValue<TValue>
	{
		/// <inheritdoc cref="ICustomizationValue{TValue}.Get()" />
		public TValue Get() => getter();

		/// <inheritdoc cref="ICustomizationValue{TValue}.Set(TValue)" />
		public CustomizationLifetime Set(TValue value) => setter(value);
	}
}
