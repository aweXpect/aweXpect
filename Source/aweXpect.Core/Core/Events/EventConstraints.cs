using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Options;

namespace aweXpect.Core.Events;

internal class EventConstraints
{
	private int _index;
	public int TotalCount => _index;
	internal Dictionary<string, List<EventConstraint>> Constraints { get; } = new();

	public void Add(string name, TriggerEventFilter? filter, Quantifier quantifier)
	{
		int index = ++_index;
		if (!Constraints.TryGetValue(name, out List<EventConstraint>? constraint))
		{
			constraint = new List<EventConstraint>();
			Constraints.Add(name, constraint);
		}

		constraint!.Add(new EventConstraint(index, filter, quantifier));
	}

	public EventRecording<T> StartRecordingEvents<T>(T actual) => new(this, actual);

	public override string ToString()
	{
		StringBuilder? sb = new();
		bool hasMultipleGroups = Constraints.Count > 1;
		bool hasMultipleConstraints = _index > 1;
		foreach (KeyValuePair<string, List<EventConstraint>> group in Constraints)
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

	internal class EventConstraint(int index, TriggerEventFilter? filter, Quantifier quantifier)
	{
		public int Index { get; } = index;
		public TriggerEventFilter? Filter { get; } = filter;
		public Quantifier Quantifier { get; } = quantifier;
	}
}
