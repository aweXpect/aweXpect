using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Options;

namespace aweXpect.Core.Events;

internal class EventConstraints
{
	private readonly Dictionary<string, List<EventConstraint>> _constraints = new();

	private int _constraintCount;

	/// <summary>
	///     Add a new event constraint.
	/// </summary>
	public void Add(string eventName, TriggerEventFilter? filter, Quantifier quantifier)
	{
		int index = ++_constraintCount;
		if (!_constraints.TryGetValue(eventName, out List<EventConstraint>? constraint))
		{
			constraint = new List<EventConstraint>();
			_constraints.Add(eventName, constraint);
		}

		constraint!.Add(new EventConstraint(index, filter, quantifier));
	}

	/// <summary>
	///     Start recording all registered events.
	/// </summary>
	/// <remarks>
	///     To stop the recording, dispose of the returned <see cref="EventRecording{T}" />.
	/// </remarks>
	public EventRecording<T> StartRecordingEvents<T>(T actual)
		=> new(actual, _constraints.Keys);

	/// <summary>
	///     Returns the expectation string.
	/// </summary>
	public override string ToString()
	{
		StringBuilder? sb = new();
		bool hasMultipleConstraints = _constraintCount > 1;
		bool hasMultipleGroups = _constraints.Count > 1;
		foreach (KeyValuePair<string, List<EventConstraint>> group in _constraints)
		{
			if (hasMultipleGroups)
			{
				sb.Append("  ");
			}

			bool hasMultipleGroupConstraints = group.Value.Count > 1;
			if (hasMultipleGroupConstraints)
			{
				sb.Append("trigger event ").Append(group.Key);
				AppendExpectationForGroupWithMultipleValues(sb, group.Value, hasMultipleGroups);
			}
			else
			{
				EventConstraint item = group.Value.First();
				AppendExpectationForGroupWithSingleValue(sb, group.Key, item, hasMultipleConstraints);
			}

			sb.Append(" and");
			sb.AppendLine();
		}

		sb.Length -= 4;
		sb.Length -= Environment.NewLine.Length;
		return sb.ToString();
	}

	private static void AppendExpectationForGroupWithMultipleValues(
		StringBuilder sb,
		List<EventConstraint> groupConstraints,
		bool hasMultipleGroups)
	{
		foreach (EventConstraint? item in groupConstraints)
		{
			sb.AppendLine();
			if (hasMultipleGroups)
			{
				sb.Append("  ");
			}

			sb.Append("  [").Append(item.Index).Append("]");
			if (item.Filter != null)
			{
				sb.Append(item.Filter);
			}

			sb.Append(' ').Append(item.Quantifier);
			sb.Append(" and");
		}

		sb.Length -= 4;
	}

	private static void AppendExpectationForGroupWithSingleValue(
		StringBuilder sb,
		string eventName,
		EventConstraint item,
		bool hasMultipleConstraints)
	{
		if (hasMultipleConstraints)
		{
			sb.Append("[" + item.Index + "] ");
		}

		sb.Append("trigger event ").Append(eventName);
		if (item.Filter != null)
		{
			sb.Append(item.Filter);
		}

		sb.Append(' ').Append(item.Quantifier);
	}

	/// <summary>
	///     Checks if any expectation is not satisfied by the recorded events in the <paramref name="recording" />.
	/// </summary>
	/// <remarks>
	///     Appends all errors to the provided <paramref name="stringBuilder" />.
	/// </remarks>
	public bool HasErrors<T>(EventRecording<T> recording, StringBuilder stringBuilder)
	{
		bool hasErrors = false;
		bool hasMultipleConstraints = _constraintCount > 1;
		if (hasMultipleConstraints)
		{
			stringBuilder.AppendLine();
		}

		foreach (KeyValuePair<string, List<EventConstraint>> constraint in _constraints)
		{
			string eventName = constraint.Key;
			List<EventConstraint> constraints = constraint.Value;

			bool hasGroupError =
				HasGroupError(recording, stringBuilder, eventName, constraints, hasMultipleConstraints);

			if (hasGroupError)
			{
				hasErrors = true;
				stringBuilder.Append(" in ");
				stringBuilder.Append(recording.ToString(eventName, hasMultipleConstraints ? "      " : ""));
				stringBuilder.AppendLine(" and");
			}
		}

		stringBuilder.Length -= 4;
		stringBuilder.Length -= Environment.NewLine.Length;
		return hasErrors;
	}

	private static bool HasGroupError<T>(
		EventRecording<T> recording,
		StringBuilder stringBuilder,
		string eventName,
		List<EventConstraint> constraints,
		bool hasMultipleConstraints)
	{
		bool hasGroupError = false;
		foreach (EventConstraint? item in constraints)
		{
			int eventCount = recording.GetEventCount(eventName, item.Filter);

			if (item.Quantifier.Check(eventCount, true) != true)
			{
				hasGroupError = true;
				if (constraints.Count > 1)
				{
					stringBuilder.Append("  [").Append(item.Index).Append(']');
					stringBuilder.Append(eventCount switch
					{
						0 => " never recorded",
						1 => " only recorded once",
						_ => $" only recorded {eventCount} times"
					});
					stringBuilder.AppendLine(" and");
				}
				else
				{
					if (hasMultipleConstraints)
					{
						stringBuilder.Append("  [").Append(item.Index).Append(']');
					}

					stringBuilder.Append(eventCount switch
					{
						0 => " never recorded",
						1 => " only recorded once",
						_ => $" only recorded {eventCount} times"
					});
				}
			}
		}

		if (constraints.Count > 1)
		{
			stringBuilder.Length -= 4;
			stringBuilder.Length -= Environment.NewLine.Length;
		}

		return hasGroupError;
	}
}
