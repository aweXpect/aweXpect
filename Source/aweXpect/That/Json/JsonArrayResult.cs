#if NET8_0_OR_GREATER
using System;
using System.Text.Json;

namespace aweXpect;

/// <summary>
///     The result for an expectation on a JSON <see cref="JsonValueKind.Array" />.
/// </summary>
public class JsonArrayResult(JsonElement source)
{
	/// <summary>
	///     Combine multiple JSON expectations on this <see cref="JsonValueKind.Array" />.
	/// </summary>
	public JsonArrayResult And => this;

	/// <summary>
	///     Add an expectation on the number of elements in the array.
	/// </summary>
	public JsonArrayLengthResult With(int amount)
	{
		_ = source;
		return new JsonArrayLengthResult(this);
	}

	/// <summary>
	///     Add an expectation on the element at the given zero-based <paramref name="index" />.
	/// </summary>
	public JsonPropertyResult<JsonArrayResult> At(int index) => new(this);

	/// <summary>
	///     Add an expectation on the elements of the array. They are matched against the <paramref name="expected" /> values.
	/// </summary>
	public JsonArrayElementsResult WithElements(params object[] expected) => new(source);

	/// <summary>
	///     Add an expectation that the elements of the array are arrays which satisfy the <paramref name="expectations" />.
	/// </summary>
	public JsonArrayElementsResult WithArrays(params Action<JsonArrayResult>[] expectations) => new(source);

	/// <summary>
	///     Add an expectation that the elements of the array are objects which satisfy the <paramref name="expectations" />.
	/// </summary>
	public JsonArrayElementsResult WithObjects(params Action<JsonObjectResult>[] expectations) => new(source);

	/// <summary>
	///     Result for the number of elements in a JSON <see cref="JsonValueKind.Array" />.
	/// </summary>
	public class JsonArrayLengthResult(JsonArrayResult result)
	{
		/// <summary>
		///     The number of elements in a JSON <see cref="JsonValueKind.Array" />.
		/// </summary>
		public JsonArrayResult Elements() => result;
	}

	/// <summary>
	///     Result for the enumeration of elements in a JSON array.
	/// </summary>
	public class JsonArrayElementsResult(JsonElement source) : JsonArrayResult(source)
	{
		/// <summary>
		///     The provided elements can be in any order.
		/// </summary>
		public JsonArrayResult InAnyOrder() => this;
	}
}
#endif
