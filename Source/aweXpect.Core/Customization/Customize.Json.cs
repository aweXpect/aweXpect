#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using System.Threading;

namespace aweXpect.Customization;

public partial class Customize : ICustomizeJson
{
	private static readonly JsonDocumentOptions GlobalDefaultJsonDocumentOptions = new()
	{
		AllowTrailingCommas = true
	};
	private readonly AsyncLocal<JsonDocumentOptions?> _defaultJsonDocumentOptions = new();

	/// <summary>
	///     Customizes the JSON settings.
	/// </summary>
	public static ICustomizeJson Json => Instance;

	/// <inheritdoc />
	JsonDocumentOptions ICustomizeJson.DefaultJsonDocumentOptions
		=> _defaultJsonDocumentOptions.Value ?? GlobalDefaultJsonDocumentOptions;

	/// <inheritdoc />
	IDisposable ICustomizeJson.SetDefaultJsonDocumentOptions(JsonDocumentOptions options)
	{
		_defaultJsonDocumentOptions.Value = options;
		return new ActionDisposable(() => _defaultJsonDocumentOptions.Value = null);
	}
}
#endif
