using System;

namespace aweXpect.Customization;

public static partial class GlobalCustomizationExtensions
{
	/// <summary>
	///     Customize the formatting settings.
	/// </summary>
	public static FormattingCustomization Formatting(this GlobalCustomization globalCustomization)
		=> new(globalCustomization);

	/// <summary>
	///     Customize the formatting settings.
	/// </summary>
	public class FormattingCustomization : IUpdateableCustomizationValue<FormattingCustomizationValue>
	{
		private readonly IGlobalCustomization _globalCustomization;

		internal FormattingCustomization(IGlobalCustomization globalCustomization)
		{
			_globalCustomization = globalCustomization;
			MaximumNumberOfCollectionItems = new CustomizationValue<int>(
				() => Get().MaximumNumberOfCollectionItems,
				// ReSharper disable once WithExpressionModifiesAllMembers
				v => Update(p => p with
				{
					MaximumNumberOfCollectionItems = v
				}));
		}

		/// <inheritdoc cref="FormattingCustomizationValue.MaximumNumberOfCollectionItems" />
		public ICustomizationValue<int> MaximumNumberOfCollectionItems { get; }

		/// <inheritdoc cref="IUpdateableCustomizationValue{FormattingCustomizationValue}.Get()" />
		public FormattingCustomizationValue Get()
			=> _globalCustomization.Get(nameof(Formatting), new FormattingCustomizationValue());

		/// <inheritdoc
		///     cref="IUpdateableCustomizationValue{FormattingCustomizationValue}.Update(Func{FormattingCustomizationValue,FormattingCustomizationValue})" />
		public CustomizationLifetime Update(Func<FormattingCustomizationValue, FormattingCustomizationValue> update)
			=> _globalCustomization.Set(nameof(Formatting), update(Get()));
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
