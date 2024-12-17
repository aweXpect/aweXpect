using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     Extension methods for triggers.
/// </summary>
public static class TriggersExtensions
{
	/// <summary>
	///     Verifies that the subject triggers an event with the given <paramref name="eventName" />.
	/// </summary>
	public static TriggerParameterResult<T> Triggers<T>(
		this IExpectSubject<T> subject, string eventName)
	{
		IThat<T> should = subject.Should(_ => { });
		return new TriggerParameterResult<T>(should, eventName);
	}
}
