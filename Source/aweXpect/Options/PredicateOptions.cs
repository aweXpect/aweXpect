using System;

namespace aweXpect.Options;

internal class PredicateOptions<TItem>
{
	private Func<TItem, bool>? _predicate;
	private string? _predicateDescription;

	public bool Matches(TItem item)
		=> _predicate is null || _predicate(item);

	internal void SetPredicate(Func<TItem, bool> predicate, string predicateDescription)
	{
		_predicate = predicate;
		_predicateDescription = predicateDescription;
	}

	public string GetDescription()
	{
		if (_predicateDescription is null)
		{
			return "";
		}

		return _predicateDescription;
	}
}
