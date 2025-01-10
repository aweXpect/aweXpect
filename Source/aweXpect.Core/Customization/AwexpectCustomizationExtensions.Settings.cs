using System;
using aweXpect.Signaling;

namespace aweXpect.Customization;

public static partial class AwexpectCustomizationExtensions
{
	/// <summary>
	///     Customize the settings.
	/// </summary>
	public static SettingsCustomization Settings(this AwexpectCustomization awexpectCustomization)
		=> new(awexpectCustomization);

	/// <summary>
	///     Customize the settings.
	/// </summary>
	public class SettingsCustomization : ICustomizationValueUpdater<SettingsCustomizationValue>
	{
		private readonly IAwexpectCustomization _awexpectCustomization;

		internal SettingsCustomization(IAwexpectCustomization awexpectCustomization)
		{
			_awexpectCustomization = awexpectCustomization;
			DefaultSignalerTimeout = new CustomizationValue<TimeSpan>(
				() => Get().DefaultSignalerTimeout,
				v => Update(p => p with
				{
					DefaultSignalerTimeout = v
				}));
			DefaultTimeComparisonTimeout = new CustomizationValue<TimeSpan>(
				() => Get().DefaultTimeComparisonTimeout,
				v => Update(p => p with
				{
					DefaultTimeComparisonTimeout = v
				}));
		}

		/// <inheritdoc cref="SettingsCustomizationValue.DefaultSignalerTimeout" />
		public ICustomizationValueSetter<TimeSpan> DefaultSignalerTimeout { get; }

		/// <inheritdoc cref="SettingsCustomizationValue.DefaultTimeComparisonTimeout" />
		public ICustomizationValueSetter<TimeSpan> DefaultTimeComparisonTimeout { get; }

		/// <inheritdoc cref="ICustomizationValueUpdater{SettingsCustomizationValue}.Get()" />
		public SettingsCustomizationValue Get()
			=> _awexpectCustomization.Get(nameof(Settings), new SettingsCustomizationValue());

		/// <inheritdoc
		///     cref="ICustomizationValueUpdater{SettingsCustomizationValue}.Update(Func{SettingsCustomizationValue,SettingsCustomizationValue})" />
		public CustomizationLifetime Update(Func<SettingsCustomizationValue, SettingsCustomizationValue> update)
			=> _awexpectCustomization.Set(nameof(Settings), update(Get()));
	}

	/// <summary>
	///     Customize the settings.
	/// </summary>
	public record SettingsCustomizationValue
	{
		/// <summary>
		///     The default timeout for the <see cref="Signaler" />.
		/// </summary>
		public TimeSpan DefaultSignalerTimeout { get; set; } = TimeSpan.FromSeconds(30);
		
		/// <summary>
		///     The default timeout for time comparisons.
		/// </summary>
		public TimeSpan DefaultTimeComparisonTimeout { get; set; } = TimeSpan.Zero;
	}
}
