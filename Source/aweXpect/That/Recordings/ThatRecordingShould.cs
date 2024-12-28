using aweXpect.Core;
using aweXpect.Events;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on event <see cref="IRecording{TSubject}" />.
/// </summary>
public static partial class ThatRecordingShould
{
	/// <summary>
	///     Start expectations for the current <see cref="IRecording{TSubject}" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<IRecording<TSubject>> Should<TSubject>(this IExpectSubject<IRecording<TSubject>> subject)
		where TSubject : notnull
		=> subject.Should(That.WithoutAction);
}
