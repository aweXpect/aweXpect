#if NET8_0_OR_GREATER
using System;
using System.Text.Json;

namespace aweXpect.Json;

/// <summary>
///     Options for strings as JSON.
/// </summary>
public class JsonOptions
{
	private static readonly JsonDocumentOptions DefaultOptions = new()
	{
		AllowTrailingCommas = true
	};

	private JsonDocumentOptions? _options;

	/// <summary>
	///     The current <see cref="JsonDocumentOptions" /> to use when interpreting a <see langword="string" /> as JSON.
	/// </summary>
	public JsonDocumentOptions DocumentOptions => _options ?? DefaultOptions;

	/// <summary>
	///     Flag indicating, if the subject should be checked for additional properties.
	/// </summary>
	public bool CheckForAdditionalProperties { get; private set; }

	/// <summary>
	///     Specify the <see cref="JsonDocumentOptions" /> to use when interpreting a <see langword="string" /> as JSON.
	/// </summary>
	public JsonOptions UsingJsonOptions(
		Func<JsonDocumentOptions, JsonDocumentOptions> jsonDocumentOptions)
	{
		_options ??= DefaultOptions;
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
