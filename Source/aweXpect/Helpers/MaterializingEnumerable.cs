using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace aweXpect.Helpers;

internal sealed class MaterializingEnumerable<T> : IEnumerable<T>, ICountable
{
	private readonly IEnumerator<T> _enumerator;
	private readonly List<T> _materializedItems = new();

	private MaterializingEnumerable(IEnumerable<T> enumerable)
	{
		_enumerator = enumerable.GetEnumerator();
	}

	public int? Count { get; private set; }

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
}

internal sealed class MaterializingEnumerable : IEnumerable, ICountable
{
	private readonly IEnumerator _enumerator;
	private readonly List<object?> _materializedItems = new();
	private bool _isMaterializedCompletely = false;

	private MaterializingEnumerable(IEnumerable enumerable)
	{
		_enumerator = enumerable.GetEnumerator();
	}

	public int? Count { get; private set; }

	[return: NotNullIfNotNull(nameof(enumerable))]
	public static IEnumerable? Wrap(IEnumerable? enumerable)
	{
		if (enumerable is ICollection or MaterializingEnumerable or null)
		{
			return enumerable;
		}

		return new MaterializingEnumerable(enumerable);
	}

	#region IEnumerable Members

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	private IEnumerator GetEnumerator()
	{
		foreach (object? materializedItem in _materializedItems)
		{
			yield return materializedItem;
		}

		while (!_isMaterializedCompletely && _enumerator.MoveNext())
		{
			object? item = _enumerator.Current;
			_materializedItems.Add(item);
			yield return item;
		}

		_isMaterializedCompletely = true;
		(_enumerator as IDisposable)?.Dispose();
		Count = _materializedItems.Count;
	}

	#endregion
}
