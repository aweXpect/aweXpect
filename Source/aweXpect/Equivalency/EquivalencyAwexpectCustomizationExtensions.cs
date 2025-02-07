using System;
using aweXpect.Customization;

namespace aweXpect.Equivalency;

/// <summary>
///     Extension methods on <see cref="AwexpectCustomization" /> for equivalency.
/// </summary>
public static class EquivalencyAwexpectCustomizationExtensions
{
	/// <summary>
	///     Customize the equivalency settings.
	/// </summary>
	public static EquivalencyCustomization Equivalency(this AwexpectCustomization awexpectCustomization)
		=> new(awexpectCustomization);

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

	/// <summary>
	///     Customize the equivalency settings.
	/// </summary>
	public class EquivalencyCustomization : ICustomizationValueUpdater<EquivalencyCustomizationValue>
	{
		private readonly IAwexpectCustomization _awexpectCustomization;

		internal EquivalencyCustomization(IAwexpectCustomization awexpectCustomization)
		{
			_awexpectCustomization = awexpectCustomization;
			DefaultEquivalencyOptions = new CustomizationValue<EquivalencyOptions>(
				() => Get().DefaultEquivalencyOptions,
				v => Update(p => p with
				{
					DefaultEquivalencyOptions = v
				}));
		}

		/// <inheritdoc cref="EquivalencyCustomizationValue.DefaultEquivalencyOptions" />
		public ICustomizationValueSetter<EquivalencyOptions> DefaultEquivalencyOptions { get; }

		/// <inheritdoc cref="ICustomizationValueUpdater{EquivalencyCustomizationValue}.Get()" />
		public EquivalencyCustomizationValue Get()
			=> _awexpectCustomization.Get(nameof(Equivalency), new EquivalencyCustomizationValue());

		/// <inheritdoc
		///     cref="ICustomizationValueUpdater{EquivalencyCustomizationValue}.Update(Func{EquivalencyCustomizationValue,EquivalencyCustomizationValue})" />
		public CustomizationLifetime Update(Func<EquivalencyCustomizationValue, EquivalencyCustomizationValue> update)
			=> _awexpectCustomization.Set(nameof(Equivalency), update(Get()));
	}

	/// <summary>
	///     Customize the equivalency settings.
	/// </summary>
	public record EquivalencyCustomizationValue
	{
		/// <summary>
		///     The default <see cref="EquivalencyOptions" />.
		/// </summary>
		public EquivalencyOptions DefaultEquivalencyOptions { get; init; } = new();
	}
}
