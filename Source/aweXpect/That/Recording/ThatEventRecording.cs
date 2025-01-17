using System.Text;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Recording;

namespace aweXpect;

/// <summary>
///     Expectations on event <see cref="IEventRecording{TSubject}" />.
/// </summary>
public static partial class ThatEventRecording
{
	private readonly struct HaveTriggeredConstraint<TSubject>(
		string it,
		string eventName,
		TriggerEventFilter filter,
		Quantifier quantifier) : IValueConstraint<IEventRecording<TSubject>>
		where TSubject : notnull
	{
		public ConstraintResult IsMetBy(IEventRecording<TSubject> actual)
		{
			string expectation = $"have recorded the {eventName} event on {actual}{filter} {quantifier}";
			string quantifierString = quantifier.ToString();
			if (quantifierString == "never")
			{
				expectation = $"have never recorded the {eventName} event on {actual}{filter}";
			}

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				expectation = quantifierString == "never"
					? $"have never recorded the {eventName} event{filter}"
					: $"have recorded the {eventName} event{filter} {quantifier}";
				return new ConstraintResult.Failure<IEventRecording<TSubject>>(actual!, expectation,
					$"{it} was <null>");
			}

			IEventRecordingResult recordingResult = actual.Stop();
			int eventCount = recordingResult.GetEventCount(eventName, filter.IsMatch);

			if (quantifier.Check(eventCount, true) != true)
			{
				StringBuilder sb = new();
				sb.Append(it).Append(" was ");
				if (eventCount == 0)
				{
					sb.Append("never recorded ");
				}
				else if (eventCount == 1)
				{
					sb.Append("recorded once ");
				}
				else
				{
					sb.Append("recorded ").Append(eventCount).Append(" times ");
				}

				sb.Append("in ");
				sb.Append(recordingResult.ToString(eventName));
				return new ConstraintResult.Failure<IEventRecording<TSubject>>(actual, expectation, sb.ToString());
			}

			return new ConstraintResult.Success<IEventRecording<TSubject>>(actual, expectation);
		}
	}
}
