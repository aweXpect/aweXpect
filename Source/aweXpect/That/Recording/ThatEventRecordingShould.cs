using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Recording;

namespace aweXpect;

/// <summary>
///     Expectations on event <see cref="IEventRecording{TSubject}" />.
/// </summary>
public static partial class ThatEventRecordingShould
{
	/// <summary>
	///     Start expectations for the current <see cref="IEventRecording{TSubject}" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<IEventRecording<TSubject>> Should<TSubject>(
		this IExpectSubject<IEventRecording<TSubject>> subject)
		where TSubject : notnull
		=> subject.Should(That.WithoutAction);
}
