using System;

namespace aweXpect.Customization;

public partial class AwexpectCustomization
{
	private FormattingCustomization? _formatting;

	/// <summary>
	///     Customize the formatting settings.
	/// </summary>
	public FormattingCustomization Formatting()
	{
		_formatting ??= new FormattingCustomization(this);
		return _formatting;
	}

	/// <summary>
	///     Customize the formatting settings.
	/// </summary>
	public class FormattingCustomization : ICustomizationValueUpdater<FormattingCustomizationValue>
	{
		private static readonly FormattingCustomizationValue EmptyValue = new();
		private readonly IAwexpectCustomization _awexpectCustomization;

		internal FormattingCustomization(IAwexpectCustomization awexpectCustomization)
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
			=> _awexpectCustomization.Get(nameof(Formatting), EmptyValue);

		/// <inheritdoc
		///     cref="ICustomizationValueUpdater{FormattingCustomizationValue}.Update(Func{FormattingCustomizationValue,FormattingCustomizationValue})" />
		public CustomizationLifetime Update(Func<FormattingCustomizationValue, FormattingCustomizationValue> update)
			=> _awexpectCustomization.Set(nameof(Formatting), update(Get()));
	}

	/// <summary>
	///     Customize the formatting settings.
	/// </summary>
	public record FormattingCustomizationValue
	{
		/// <summary>
		///     The maximum number of displayed items in a collection.
		/// </summary>
		public int MaximumNumberOfCollectionItems { get; init; } = 10;
	}
}
