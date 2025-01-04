using System.Runtime.CompilerServices;

namespace aweXpect.Recording;

/// <summary>
///     Extension methods for creating <see cref="IEventRecording{TSubject}" />.
/// </summary>
public static class RecordExtensions
{
	/// <summary>
	///     Create a recording on the <paramref name="subject" />.
	/// </summary>
	public static RecordingFactory<TSubject> Record<TSubject>(this TSubject subject,
		[CallerArgumentExpression("subject")] string doNotPopulateThisValue = "")
		where TSubject : notnull
		=> new(subject, doNotPopulateThisValue);
}
