namespace aweXpect.Recording;

/// <summary>
///     Factory for creating a <see cref="IEventRecording{TSubject}" />.
/// </summary>
public class RecordingFactory<TSubject>(TSubject subject, string subjectExpression)
	where TSubject : notnull
{
	/// <summary>
	///     Record the events with the given <paramref name="eventNames" />.
	/// </summary>
	/// <remarks>
	///     If no <paramref name="eventNames" /> are provided, all events on the subject are recorded.
	/// </remarks>
	public IEventRecording<TSubject> Events(params string[] eventNames)
		=> new EventRecording<TSubject>(subject, subjectExpression, eventNames);
}
