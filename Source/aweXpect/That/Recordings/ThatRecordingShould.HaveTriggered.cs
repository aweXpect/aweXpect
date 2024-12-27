using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Events;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatRecordingShould
{
	/// <summary>
	///     Verifies that the subject has triggered the expected <paramref name="eventName" />.
	/// </summary>
	public static EventTriggerResult<TSubject> HaveTriggered<TSubject>(this IThat<IRecording<TSubject>> source,
		string eventName)
		where TSubject : notnull
	{
		Quantifier quantifier = new();
		TriggerEventFilter filter = new();
		return new EventTriggerResult<TSubject>(
			source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint<TSubject>(it, eventName, filter, quantifier)),
			source,
			filter,
			quantifier);
	}

	private readonly struct HaveTriggeredConstraint<TSubject>(
		string it,
		string eventName,
		TriggerEventFilter filter,
		Quantifier quantifier) : IValueConstraint<IRecording<TSubject>>
		where TSubject : notnull
	{
		public ConstraintResult IsMetBy(IRecording<TSubject> actual)
		{
			int eventCount = actual.GetEventCount(eventName, filter.IsMatch);
			string quantifierString = quantifier.ToString();
			string expectation = $"have recorded the {eventName} event on {actual}{filter} {quantifier}";
			if (quantifierString == "never")
			{
				expectation = $"have never recorded the {eventName} event on {actual}{filter}";
			}

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
				sb.Append(actual.ToString(eventName, ""));
				return new ConstraintResult.Failure<IRecording<TSubject>>(actual, expectation, sb.ToString());
			}

			return new ConstraintResult.Success<IRecording<TSubject>>(actual, expectation);
		}
	}
}
