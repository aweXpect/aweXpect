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
	public JsonDocumentOptions DocumentOptions
		=> _options ?? Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get();

	/// <summary>
	///     Flag indicating, if the additional properties in the subject should be ignored.
	/// </summary>
	public bool IgnoreAdditionalProperties { get; private set; }

	/// <summary>
	///     Specify the <see cref="JsonDocumentOptions" /> to use when interpreting a <see langword="string" /> as JSON.
	/// </summary>
	public JsonOptions WithJsonOptions(
		Func<JsonDocumentOptions, JsonDocumentOptions> jsonDocumentOptions)
	{
		_options ??= Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get();
		_options = jsonDocumentOptions(_options.Value);
		return this;
	}

	/// <summary>
	///     Ignores additional properties in the subject
	///     when <paramref name="ignoreAdditionalProperties" /> is <see langword="true" />
	/// </summary>
	public JsonOptions IgnoringAdditionalProperties(bool ignoreAdditionalProperties = true)
	{
		IgnoreAdditionalProperties = ignoreAdditionalProperties;
		return this;
	}
}
#endif
