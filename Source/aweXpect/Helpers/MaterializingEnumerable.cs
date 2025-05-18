using System.Collections;
using System.Collections.Generic;

namespace aweXpect.Helpers;

internal sealed class MaterializingEnumerable<T> : IEnumerable<T>, ICountable
{
	private readonly IEnumerator<T> _enumerator;
	private readonly List<T> _materializedItems = new();

	private MaterializingEnumerable(IEnumerable<T> enumerable)
	{
		_enumerator = enumerable.GetEnumerator();
	}

	public static IEnumerable<T> Wrap(IEnumerable<T> enumerable)
	{
		if (enumerable is ICollection<T> or MaterializingEnumerable<T>)
		{
			return enumerable;
		}

		return new MaterializingEnumerable<T>(enumerable);
	}

	#region IEnumerable<T> Members

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	/// <inheritdoc />
	public IEnumerator<T> GetEnumerator()
	{
		foreach (T materializedItem in _materializedItems)
		{
			yield return materializedItem;
		}

		while (_enumerator.MoveNext())
		{
			T item = _enumerator.Current;
			_materializedItems.Add(item);
			yield return item;
		}

		Count = _materializedItems.Count;
	}

	#endregion

	public int? Count { get; private set; }
}
