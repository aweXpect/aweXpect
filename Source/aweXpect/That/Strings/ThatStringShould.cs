using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="string" /> values.
/// </summary>
public static partial class ThatStringShould
{
	/// <summary>
	///     Start expectations for the current <see cref="string" />? <paramref name="subject" />.
	/// </summary>
	public static IThat<string?> Should(this IExpectSubject<string?> subject)
		=> subject.Should(That.WithoutAction);
}
