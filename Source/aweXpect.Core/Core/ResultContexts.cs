using System;
using System.Collections;
using System.Collections.Generic;

namespace aweXpect.Core;

/// <summary>
///     The list of <see cref="ResultContext" /> that is appended to the failure message.
/// </summary>
public class ResultContexts : IEnumerable<ResultContext>
{
	private readonly List<ResultContext> _results = new();

	/// <inheritdoc cref="IEnumerable{ResultContext}.GetEnumerator()" />
	public IEnumerator<ResultContext> GetEnumerator() => _results.GetEnumerator();

	/// <inheritdoc cref="IEnumerable.GetEnumerator()" />
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	/// <summary>
	///     Adds the <paramref name="context" /> to the context list.
	/// </summary>
	public ResultContexts Add(ResultContext context)
	{
		_results.Add(context);
		return this;
	}

	/// <summary>
	///     Removes all contexts from the context list.
	/// </summary>
	public ResultContexts Clear()
	{
		_results.Clear();
		return this;
	}

	/// <summary>
	///     Removes all contexts with the given <paramref name="title" />.
	/// </summary>
	public ResultContexts Remove(string title, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
	{
		_results.RemoveAll(x => x.Title.Equals(title, stringComparison));
		return this;
	}

	/// <summary>
	///     Removes all contexts that match the <paramref name="predicate" />.
	/// </summary>
	public ResultContexts Remove(Predicate<ResultContext> predicate)
	{
		_results.RemoveAll(predicate);
		return this;
	}
}
