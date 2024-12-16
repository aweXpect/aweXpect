using System;
using System.Threading;

namespace aweXpect.Customization;

public partial class Customize : ICustomizeFormatting
{
	private static readonly int MaximumNumberOfCollectionItemsDefaultValue = 10;

	private readonly AsyncLocal<int?> _maximumNumberOfCollectionItems = new();

	/// <summary>
	///     Customizes the formatting settings.
	/// </summary>
	public static ICustomizeFormatting Formatting => Instance;

	/// <inheritdoc />
	int ICustomizeFormatting.MaximumNumberOfCollectionItems
		=> _maximumNumberOfCollectionItems.Value ?? MaximumNumberOfCollectionItemsDefaultValue;

	/// <inheritdoc />
	IDisposable ICustomizeFormatting.SetMaximumNumberOfCollectionItems(int value)
	{
		_maximumNumberOfCollectionItems.Value = value;
		return new ActionDisposable(() => _maximumNumberOfCollectionItems.Value = null);
	}
}
