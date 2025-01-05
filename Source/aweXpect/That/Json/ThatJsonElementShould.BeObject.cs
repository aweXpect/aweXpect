#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatJsonElementShould
{
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is an <see cref="JsonValueKind.Object" />.
	/// </summary>
	public static AndOrResult<JsonElement, IThat<JsonElement>> BeObject(this IThat<JsonElement> source)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeValueKindConstraint(it, JsonValueKind.Object)),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is an <see cref="JsonValueKind.Object" />
	///     whose value satisfies the <paramref name="expectation" />.
	/// </summary>
	public static AndOrResult<JsonElement, IThat<JsonElement>> BeObject(this IThat<JsonElement> source,
		Action<JsonObjectResult> expectation)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeValueKindConstraint(it, JsonValueKind.Object)),
			source);
}
#endif
