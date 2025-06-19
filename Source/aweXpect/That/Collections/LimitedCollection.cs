using System.Collections;
using System.Collections.Generic;
using aweXpect.Customization;

namespace aweXpect;

internal class LimitedCollection<T> : ICollection<T>
{
	private readonly List<T> _buffer;
	private readonly int _limit;

	public LimitedCollection(int? limit = null)
	{
		_limit = limit ?? Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get() + 1;
		_buffer = new List<T>(_limit);
	}

	/// <inheritdoc cref="IEnumerable{T}.GetEnumerator()" />
	public IEnumerator<T> GetEnumerator()
		=> _buffer.GetEnumerator();

	/// <inheritdoc cref="IEnumerable.GetEnumerator()" />
	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	/// <inheritdoc cref="ICollection{T}.Add(T)" />
	public void Add(T item)
	{
		if (!IsReadOnly)
		{
			_buffer.Add(item);
		}
	}

	/// <inheritdoc cref="ICollection{T}.Clear()" />
	public void Clear()
		=> _buffer.Clear();

	/// <inheritdoc cref="ICollection{T}.Contains(T)" />
	public bool Contains(T item)
		=> _buffer.Contains(item);

	/// <inheritdoc cref="ICollection{T}.CopyTo(T[], int)" />
	public void CopyTo(T[] array, int arrayIndex)
		=> _buffer.CopyTo(array, arrayIndex);

	/// <inheritdoc cref="ICollection{T}.Remove(T)" />
	public bool Remove(T item)
		=> _buffer.Remove(item);

	/// <inheritdoc cref="ICollection{T}.Count" />
	public int Count
		=> _buffer.Count;

	/// <inheritdoc cref="ICollection{T}.IsReadOnly" />
	public bool IsReadOnly
		=> _buffer.Count >= _limit;
}
