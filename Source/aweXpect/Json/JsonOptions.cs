#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using aweXpect.Customization;

namespace aweXpect.Json;

/// <summary>
///     Options for strings as JSON.
/// </summary>
public class JsonOptions
{
	private JsonDocumentOptions? _options;

	/// <summary>
	///     The current <see cref="JsonDocumentOptions" /> to use when interpreting a <see langword="string" /> as JSON.
	/// </summary>
	public JsonDocumentOptions DocumentOptions => _options ?? Customize.Json.DefaultJsonDocumentOptions;

	/// <summary>
	///     Flag indicating, if the subject should be checked for additional properties.
	/// </summary>
	public bool CheckForAdditionalProperties { get; private set; }

	/// <summary>
	///     Specify the <see cref="JsonDocumentOptions" /> to use when interpreting a <see langword="string" /> as JSON.
	/// </summary>
	public JsonOptions WithJsonOptions(
		Func<JsonDocumentOptions, JsonDocumentOptions> jsonDocumentOptions)
	{
		_options ??= Customize.Json.DefaultJsonDocumentOptions;
		_options = jsonDocumentOptions(_options.Value);
		return this;
	}

	/// <summary>
	///     Check for additional properties in the subject.
	/// </summary>
	public JsonOptions CheckingForAdditionalProperties(bool checkForAdditionalProperties = true)
	{
		CheckForAdditionalProperties = checkForAdditionalProperties;
		return this;
	}
}
#endif
