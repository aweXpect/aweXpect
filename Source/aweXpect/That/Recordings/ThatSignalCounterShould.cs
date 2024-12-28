using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Recording;

namespace aweXpect;

/// <summary>
///     Expectations on signal counters.
/// </summary>
public static partial class ThatSignalCounterShould
{
	/// <summary>
	///     Start expectations for the current <see cref="SignalCounter" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<SignalCounter> Should(
		this IExpectSubject<SignalCounter> subject)
		=> subject.Should(That.WithoutAction);
	
	/// <summary>
	///     Start expectations for the current <see cref="SignalCounter{TParameter}" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<SignalCounter<TParameter>> Should<TParameter>(
		this IExpectSubject<SignalCounter<TParameter>> subject)
		=> subject.Should(That.WithoutAction);
}
