using Testably.Expectations.Core;

namespace Testably.Expectations;

/// <summary>
///     Expectations on <see cref="string" /> values.
/// </summary>
public static partial class ThatStringShould
{
	/// <summary>
	///     Start expectations for the current <see cref="string" />? <paramref name="subject" />.
	/// </summary>
	public static IThat<string?> Should(this IExpectSubject<string?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);
}
