#if NET8_0_OR_GREATER
using System;
using System.Text.Json;

namespace aweXpect.Customization;

/// <summary>
///     Customizes the JSON settings.
/// </summary>
public interface ICustomizeJson
{
	/// <summary>
	///     The default <see cref="JsonDocumentOptions"/>.
	/// </summary>
	JsonDocumentOptions DefaultJsonDocumentOptions { get; }

	/// <summary>
	///     Specifies the default <see cref="JsonDocumentOptions"/>.
	/// </summary>
	/// <returns>
	///     An object, that will revert the default options upon disposal.
	/// </returns>
	IDisposable SetDefaultJsonDocumentOptions(JsonDocumentOptions options);
}
#endif
