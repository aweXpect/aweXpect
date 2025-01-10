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
			DefaultTimeComparisonTolerance = new CustomizationValue<TimeSpan>(
				() => Get().DefaultTimeComparisonTolerance,
				v => Update(p => p with
				{
					DefaultTimeComparisonTolerance = v
				}));
		}

		/// <inheritdoc cref="SettingsCustomizationValue.DefaultSignalerTimeout" />
		public ICustomizationValueSetter<TimeSpan> DefaultSignalerTimeout { get; }

		/// <inheritdoc cref="SettingsCustomizationValue.DefaultTimeComparisonTolerance" />
		public ICustomizationValueSetter<TimeSpan> DefaultTimeComparisonTolerance { get; }

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

#if NET8_0_OR_GREATER
		/// <summary>
		///     The default tolerance for time comparisons.
		/// </summary>
		/// <remarks>
		///     In Windows the `DateTime` resolution is about 10 to 15
		///     milliseconds (<see href="https://stackoverflow.com/q/3140826/4003370" />), so
		///     comparing them as exact values might result in brittle tests.<br />
		///     Therefore, it is possible to specify a default tolerance that is used for all <see cref="DateTime" />,
		///     <see cref="DateTimeOffset" />, <see cref="DateOnly" />, <see cref="TimeOnly" /> and <see cref="TimeSpan" />
		///     comparisons (unless an explicit tolerance is given).
		/// </remarks>
#else
		/// <summary>
		///     The default tolerance for time comparisons.
		/// </summary>
		/// <remarks>
		///     In Windows the `DateTime` resolution is about 10 to 15
		///     milliseconds (<see href="https://stackoverflow.com/q/3140826/4003370" />), so
		///     comparing them as exact values might result in brittle tests.<br />
		///     Therefore, it is possible to specify a default tolerance that is used for all <see cref="DateTime" />,
		///     <see cref="DateTimeOffset" /> and <see cref="TimeSpan" /> comparisons
		///     (unless an explicit tolerance is given).
		/// </remarks>
#endif
		public TimeSpan DefaultTimeComparisonTolerance { get; set; } = TimeSpan.Zero;
	}
}
