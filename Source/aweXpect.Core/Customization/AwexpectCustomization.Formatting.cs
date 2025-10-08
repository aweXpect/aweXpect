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
				v => Update(p => p with
				{
					MaximumNumberOfCollectionItems = v,
				}));
			MinimumNumberOfCharactersAfterStringDifference = new CustomizationValue<int>(
				() => Get().MinimumNumberOfCharactersAfterStringDifference,
				v => Update(p => p with
				{
					MinimumNumberOfCharactersAfterStringDifference = v,
				}));
			MaximumStringLength = new CustomizationValue<int>(
				() => Get().MaximumStringLength,
				v => Update(p => p with
				{
					MaximumStringLength = v,
				}));
		}

		/// <inheritdoc cref="FormattingCustomizationValue.MaximumNumberOfCollectionItems" />
		public ICustomizationValueSetter<int> MaximumNumberOfCollectionItems { get; }

		/// <inheritdoc cref="FormattingCustomizationValue.MaximumStringLength" />
		public ICustomizationValueSetter<int> MaximumStringLength { get; }

		/// <inheritdoc cref="FormattingCustomizationValue.MinimumNumberOfCharactersAfterStringDifference" />
		public ICustomizationValueSetter<int> MinimumNumberOfCharactersAfterStringDifference { get; }

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
		
		/// <summary>
		///     The maximum length of a <see langword="string" /> before it gets truncated.
		/// </summary>
		public int MaximumStringLength { get; init; } = 100;

		/// <summary>
		///     The minimum number of characters included after the first mismatch in the string difference.
		/// </summary>
		public int MinimumNumberOfCharactersAfterStringDifference { get; init; } = 45;
	}
}
