#if NET8_0_OR_GREATER
using System.Text.Json;

namespace aweXpect;

/// <summary>
///     The result for an expectation on a JSON <see cref="JsonValueKind.Object" />.
/// </summary>
public class JsonObjectResult(JsonElement source)
{
	/// <summary>
	///     Combine multiple JSON expectations on this <see cref="JsonValueKind.Object" />.
	/// </summary>
	public JsonObjectResult And => this;

	/// <summary>
	///     Add an expectation on the number of properties in the object.
	/// </summary>
	public JsonObjectLengthResult With(int amount)
	{
		_ = source;
		return new JsonObjectLengthResult(this);
	}

	/// <summary>
	///     Add an expectation on the property with the given <paramref name="propertyName" />.
	/// </summary>
	public JsonPropertyResult<JsonObjectResult> With(string propertyName) => new(this);

	/// <summary>
	///     Result for the number of properties in a JSON <see cref="JsonValueKind.Object" />.
	/// </summary>
	public class JsonObjectLengthResult(JsonObjectResult result)
	{
		/// <summary>
		///     The number of properties in a JSON <see cref="JsonValueKind.Object" />.
		/// </summary>
		public JsonObjectResult Properties() => result;
	}
}
#endif
