using System;

namespace aweXpect.Customization;

/// <summary>
///     Extension methods for <see cref="AwexpectCustomization" />.
/// </summary>
public static partial class AwexpectCustomizationExtensions
{
	private sealed class CustomizationValue<TValue>(
		Func<TValue> getter,
		Func<TValue, CustomizationLifetime> setter)
		: ICustomizationValueSetter<TValue>
	{
		/// <inheritdoc cref="ICustomizationValueSetter{TValue}.Get()" />
		public TValue Get() => getter();

		/// <inheritdoc cref="ICustomizationValueSetter{TValue}.Set(TValue)" />
		public CustomizationLifetime Set(TValue value) => setter(value);
	}
}
