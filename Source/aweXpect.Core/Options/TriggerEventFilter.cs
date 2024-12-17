using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aweXpect.Options;

/// <summary>
///     Filter for event parameters.
/// </summary>
public class TriggerEventFilter
{
	private readonly List<Func<object?[], bool>> _predicates = new();
	private readonly StringBuilder _toString = new();

	/// <summary>
	///     Filters the event parameters using the <paramref name="predicate" />.
	///     The <paramref name="predicateExpression" /> is used in the expectation string.
	/// </summary>
	public void AddPredicate<TProperty>(Func<object?[], bool> predicate, string predicateExpression)
	{
		if (_predicates.Count != 0)
		{
			_toString.Append(" and");
		}

		_toString.Append(" with ");
		Formatter.Format(_toString, typeof(TProperty));
		_toString.Append(" parameter ");
		_toString.Append(predicateExpression);
		_predicates.Add(predicate);
	}

	/// <summary>
	///     Checks if the provided <paramref name="parameters" /> match all registered predicates
	///     for the <paramref name="eventName" />.
	/// </summary>
	public bool IsMatch(string eventName, object?[] parameters)
		=> _predicates.All(predicate => predicate(parameters));

	/// <summary>
	///     A string representation including all predicate expressions.
	/// </summary>
	public override string ToString() => _toString.ToString();
}
