using System;
using aweXpect.Signaling;

namespace aweXpect.Customization;

public static partial class AwexpectCustomizationExtensions
{
	/// <summary>
	///     Customize the recording settings.
	/// </summary>
	public static RecordingCustomization Recording(this AwexpectCustomization awexpectCustomization)
		=> new(awexpectCustomization);

	/// <summary>
	///     Customize the recording settings.
	/// </summary>
	public class RecordingCustomization : IUpdateableCustomizationValue<RecordingCustomizationValue>
	{
		private readonly IAwexpectCustomization _awexpectCustomization;

		internal RecordingCustomization(IAwexpectCustomization awexpectCustomization)
		{
			_awexpectCustomization = awexpectCustomization;
			DefaultTimeout = new CustomizationValue<TimeSpan>(
				() => Get().DefaultTimeout,
				// ReSharper disable once WithExpressionModifiesAllMembers
				v => Update(p => p with
				{
					DefaultTimeout = v
				}));
		}

		/// <inheritdoc cref="RecordingCustomizationValue.DefaultTimeout" />
		public ICustomizationValue<TimeSpan> DefaultTimeout { get; }

		/// <inheritdoc cref="IUpdateableCustomizationValue{RecordingCustomizationValue}.Get()" />
		public RecordingCustomizationValue Get()
			=> _awexpectCustomization.Get(nameof(Recording), new RecordingCustomizationValue());

		/// <inheritdoc
		///     cref="IUpdateableCustomizationValue{RecordingCustomizationValue}.Update(Func{RecordingCustomizationValue,RecordingCustomizationValue})" />
		public CustomizationLifetime Update(Func<RecordingCustomizationValue, RecordingCustomizationValue> update)
			=> _awexpectCustomization.Set(nameof(Recording), update(Get()));
	}

	/// <summary>
	///     Customize the recording settings.
	/// </summary>
	public record RecordingCustomizationValue
	{
		/// <summary>
		///     The default timeout for the <see cref="Signaler" />.
		/// </summary>
		public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromSeconds(30);
	}
}
