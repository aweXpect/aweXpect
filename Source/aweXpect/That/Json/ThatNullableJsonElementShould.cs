#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="JsonElement" />? values.
/// </summary>
public static partial class ThatNullableJsonElementShould
{
	/// <summary>
	///     Start expectations for the current <see cref="JsonElement" />? <paramref name="subject" />.
	/// </summary>
	public static IThat<JsonElement?> Should(
		this IExpectSubject<JsonElement?> subject)
		=> subject.Should(That.WithoutAction);
}
#endif
