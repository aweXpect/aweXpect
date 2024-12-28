using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Recording;

namespace aweXpect;

/// <summary>
///     Expectations on callback recordings.
/// </summary>
public static partial class ThatCallbackRecordingShould
{
	/// <summary>
	///     Start expectations for the current <see cref="ISignalCounter" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<ISignalCounter> Should(
		this IExpectSubject<ISignalCounter> subject)
		=> subject.Should(That.WithoutAction);
	
	/// <summary>
	///     Start expectations for the current <see cref="ISignalCounter{TParameter}" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<ISignalCounter<TParameter>> Should<TParameter>(
		this IExpectSubject<ISignalCounter<TParameter>> subject)
		=> subject.Should(That.WithoutAction);
}
