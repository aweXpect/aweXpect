using aweXpect.Options;

namespace aweXpect.Core.Events;

internal class EventConstraint(int index, TriggerEventFilter? filter, Quantifier quantifier)
{
	public int Index { get; } = index;
	public TriggerEventFilter? Filter { get; } = filter;
	public Quantifier Quantifier { get; } = quantifier;
}
