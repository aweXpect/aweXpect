#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatJsonElementShould
{
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is a <see cref="JsonValueKind.String" />.
	/// </summary>
	public static AndOrResult<JsonElement, IThat<JsonElement>> BeString(this IThat<JsonElement> source) => new(
		source.ExpectationBuilder.AddConstraint(it
			=> new BeValueKindConstraint(it, JsonValueKind.String)),
		source);

	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is a <see cref="JsonValueKind.String" />
	///     whose value equals the <paramref name="expected" />.
	/// </summary>
	public static StringEqualityResult<JsonElement, IThat<JsonElement>> BeString(
		this IThat<JsonElement> source,
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<JsonElement, IThat<JsonElement>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeValueKindConstraint(it, JsonValueKind.String)),
			source,
			options);
	}
}
#endif
