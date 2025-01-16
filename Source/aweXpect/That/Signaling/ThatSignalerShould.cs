using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Signaling;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Signaler" />.
/// </summary>
public static partial class ThatSignalerShould
{
	/// <summary>
	///     Start expectations for the current <see cref="Signaler" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<Signaler> Should(
		this IExpectSubject<Signaler> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="Signaler{TParameter}" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<Signaler<TParameter>> Should<TParameter>(
		this IExpectSubject<Signaler<TParameter>> subject)
		=> subject.Should(That.WithoutAction);
}
