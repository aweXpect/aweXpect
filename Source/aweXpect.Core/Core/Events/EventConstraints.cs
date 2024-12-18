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

	public void Add(string name, TriggerEventFilter? filter, Quantifier quantifier)
	{
		int index = ++_constraintCount;
		if (!_constraints.TryGetValue(name, out List<EventConstraint>? constraint))
		{
			constraint = new List<EventConstraint>();
			_constraints.Add(name, constraint);
		}

		constraint!.Add(new EventConstraint(index, filter, quantifier));
	}

	public EventRecording<T> StartRecordingEvents<T>(T actual)
		=> new(actual, _constraints.Keys);

	public override string ToString()
	{
		StringBuilder? sb = new();
		bool hasMultipleGroups = _constraints.Count > 1;
		bool hasMultipleConstraints = _constraintCount > 1;
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
				foreach (EventConstraint? item in group.Value)
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
			else
			{
				EventConstraint? item = group.Value.First();
				if (hasMultipleConstraints)
				{
					sb.Append("[" + item.Index + "] ");
				}

				sb.Append("trigger event ").Append(group.Key);
				if (item.Filter != null)
				{
					sb.Append(item.Filter);
				}

				sb.Append(' ').Append(item.Quantifier);
			}

			sb.Append(" and");
			sb.AppendLine();
		}

		sb.Length -= 4;
		sb.Length -= Environment.NewLine.Length;
		return sb.ToString();
	}


	public bool HasErrors<T>(EventRecording<T> recording, StringBuilder sb)
	{
		bool hasErrors = false;

		bool hasMultipleConstraints = _constraintCount > 1;
		if (hasMultipleConstraints)
		{
			sb.AppendLine();
		}

		foreach (KeyValuePair<string, List<EventConstraint>> constraint in _constraints)
		{
			string? name = constraint.Key;
			bool hasGroupError = false;
			foreach (EventConstraint? item in constraint.Value)
			{
				int eventCount = recording.GetEventCount(name, item.Filter);

				if (item.Quantifier.Check(eventCount, true) != true)
				{
					hasErrors = true;
					hasGroupError = true;
					if (constraint.Value.Count > 1)
					{
						sb.Append("  [").Append(item.Index).Append(']');
						sb.Append(eventCount switch
						{
							0 => " never recorded",
							1 => " only recorded once",
							_ => $" only recorded {eventCount} times"
						});
						sb.AppendLine(" and");
					}
					else
					{
						if (hasMultipleConstraints)
						{
							sb.Append("  [").Append(item.Index).Append(']');
						}

						sb.Append(eventCount switch
						{
							0 => " never recorded",
							1 => " only recorded once",
							_ => $" only recorded {eventCount} times"
						});
					}
				}
			}

			if (constraint.Value.Count > 1)
			{
				sb.Length -= 4;
				sb.Length -= Environment.NewLine.Length;
			}

			if (hasGroupError)
			{
				sb.Append(" in ");
				sb.Append(recording.ToString(name, hasMultipleConstraints ? "      " : ""));
				sb.AppendLine(" and");
			}
		}

		return hasErrors;
	}
}
