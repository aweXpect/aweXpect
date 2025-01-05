#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatJsonElementShould
{
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is <see cref="JsonValueKind.False"/>.
	/// </summary>
	public static AndOrResult<JsonElement, IThat<JsonElement>> BeFalse(this IThat<JsonElement> source) => new(
		source.ExpectationBuilder.AddConstraint(it
			=> new BeValueKindConstraint(it, JsonValueKind.False)),
		source);
}
#endif
