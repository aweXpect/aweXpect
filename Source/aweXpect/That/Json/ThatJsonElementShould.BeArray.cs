#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatJsonElementShould
{
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is an <see cref="JsonValueKind.Array" />.
	/// </summary>
	public static AndOrResult<JsonElement, IThat<JsonElement>> BeArray(this IThat<JsonElement> source)
		=> new(
		source.ExpectationBuilder.AddConstraint(it
			=> new BeValueKindConstraint(it, JsonValueKind.Object)),
		source);
	
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is an <see cref="JsonValueKind.Array" />
	///     whose value satisfies the <paramref name="expectation" />.
	/// </summary>
	public static AndOrResult<JsonElement, IThat<JsonElement>> BeArray(this IThat<JsonElement> source,
		Action<JsonArrayResult> expectation)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
			=> new BeValueKindConstraint(it, JsonValueKind.Object)),
		source);
}
#endif
