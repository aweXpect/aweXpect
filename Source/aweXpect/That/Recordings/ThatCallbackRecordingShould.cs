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
	///     Start expectations for the current <see cref="ICallbackRecording" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<ICallbackRecording> Should(
		this IExpectSubject<ICallbackRecording> subject)
		=> subject.Should(That.WithoutAction);
	
	/// <summary>
	///     Start expectations for the current <see cref="ICallbackRecording{TParameter}" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<ICallbackRecording<TParameter>> Should<TParameter>(
		this IExpectSubject<ICallbackRecording<TParameter>> subject)
		=> subject.Should(That.WithoutAction);
}
