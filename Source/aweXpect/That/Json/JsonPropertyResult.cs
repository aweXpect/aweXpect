#if NET8_0_OR_GREATER
using System;
using System.Text.Json;

namespace aweXpect;

/// <summary>
///     The result for an expectation on a JSON property.
/// </summary>
public class JsonPropertyResult<TReturn>(TReturn returnValue)
{
	/// <summary>
	///     Add an expectation that the property value matches the given <paramref name="expected" /> value.
	/// </summary>
	public TReturn Matching(object expected) => returnValue;

	/// <summary>
	///     Add an expectation that the property value is an array which satisfies the <paramref name="expectation" />.
	/// </summary>
	/// <remarks>
	///     If the <paramref name="expectation" /> is <see langword="null" />,
	///     only the <see cref="JsonElement.ValueKind" /> is verified to be <see cref="JsonValueKind.Array" />
	/// </remarks>
	public TReturn AnArray(Action<JsonArrayResult>? expectation = null) => returnValue;

	/// <summary>
	///     Add an expectation that the property value is an object which satisfies the <paramref name="expectation" />.
	/// </summary>
	/// <remarks>
	///     If the <paramref name="expectation" /> is <see langword="null" />,
	///     only the <see cref="JsonElement.ValueKind" /> is verified to be <see cref="JsonValueKind.Object" />
	/// </remarks>
	public TReturn AnObject(Action<JsonObjectResult>? expectation = null) => returnValue;
}
#endif
