using System;
using System.Collections.Generic;

namespace aweXpect.Customization;

/// <summary>
///     Customizes the formatting settings.
/// </summary>
public interface ICustomizeFormatting
{
	/// <summary>
	///     The maximum number of displayed items in a collection.
	/// </summary>
	int MaximumNumberOfCollectionItems { get; }
	
	/// <summary>
	///     Specifies the maximum number of displayed items in a collection.
	/// </summary>
	/// <returns>An object, that will revert the maximum number of displayed items in a collection to the default value upon disposal.</returns>
	IDisposable SetMaximumNumberOfCollectionItems(int value);
}
