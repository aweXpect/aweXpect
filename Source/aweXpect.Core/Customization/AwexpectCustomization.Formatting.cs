using System;
using System.Threading;

namespace aweXpect.Customization;

public partial class AwexpectCustomization
{
	private readonly AsyncLocal<FormattingCustomizationValue> _formattingCustomizationValue = new();

	/// <summary>
	///     Customize the formatting settings.
	/// </summary>
	public FormattingCustomization Formatting() => new(this);

	/// <summary>
	///     Customize the formatting settings.
	/// </summary>
	public class FormattingCustomization : ICustomizationValueUpdater<FormattingCustomizationValue>
	{
		private readonly AwexpectCustomization _awexpectCustomization;

		internal FormattingCustomization(AwexpectCustomization awexpectCustomization)
		{
			_awexpectCustomization = awexpectCustomization;
			MaximumNumberOfCollectionItems = new CustomizationValue<int>(
				() => Get().MaximumNumberOfCollectionItems,
				// ReSharper disable once WithExpressionModifiesAllMembers
				v => Update(p => p with
				{
					MaximumNumberOfCollectionItems = v,
				}));
		}

		/// <inheritdoc cref="FormattingCustomizationValue.MaximumNumberOfCollectionItems" />
		public ICustomizationValueSetter<int> MaximumNumberOfCollectionItems { get; }

		/// <inheritdoc cref="ICustomizationValueUpdater{FormattingCustomizationValue}.Get()" />
		public FormattingCustomizationValue Get()
			=> _awexpectCustomization._formattingCustomizationValue.Value ?? new FormattingCustomizationValue();

		/// <inheritdoc
		///     cref="ICustomizationValueUpdater{FormattingCustomizationValue}.Update(Func{FormattingCustomizationValue,FormattingCustomizationValue})" />
		public CustomizationLifetime Update(Func<FormattingCustomizationValue, FormattingCustomizationValue> update)
		{
			FormattingCustomizationValue previousValue = Get();
			CustomizationLifetime lifetime = new(() =>
				_awexpectCustomization._formattingCustomizationValue.Value = previousValue);

			_awexpectCustomization._formattingCustomizationValue.Value = update(previousValue);
			return lifetime;
		}
	}

	/// <summary>
	///     Customize the formatting settings.
	/// </summary>
	public record FormattingCustomizationValue
	{
		/// <summary>
		///     The maximum number of displayed items in a collection.
		/// </summary>
		public int MaximumNumberOfCollectionItems { get; set; } = 10;
	}
}
