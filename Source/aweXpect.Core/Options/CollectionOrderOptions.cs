using System;
using System.Collections.Generic;

namespace aweXpect.Options;

/// <summary>
///     The options for specifying a <see cref="IComparer{TItem}" /> to use to compare the order of items.
/// </summary>
public record CollectionOrderOptions<TItem>
{
	private IComparer<TItem>? _comparer;

	/// <summary>
	///     Returns the specified comparer or a default comparer.
	/// </summary>
	public IComparer<TItem> GetComparer() => _comparer ?? GetDefaultComparer();

	/// <summary>
	///     Set the comparer to use to compare the order of items.
	/// </summary>
	public void SetComparer(IComparer<TItem> comparer) => _comparer = comparer;

	private static IComparer<TItem> GetDefaultComparer()
		=> typeof(TItem) == typeof(string) ? (IComparer<TItem>)StringComparer.Ordinal : Comparer<TItem>.Default;

	/// <inheritdoc />
	public override string ToString()
	{
		if (_comparer == null)
		{
			return "";
		}

		return $" using {_comparer.GetType().Name}";
	}
}
