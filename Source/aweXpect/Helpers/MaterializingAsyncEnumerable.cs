﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

namespace aweXpect.Helpers;

internal sealed class MaterializingAsyncEnumerable<T> : IAsyncEnumerable<T>, IMaterializedEnumerable<T>
{
	private readonly IAsyncEnumerator<T> _enumerator;
	private readonly List<T> _materializedItems = new();

	private MaterializingAsyncEnumerable(IAsyncEnumerable<T> enumerable)
	{
		_enumerator = enumerable.GetAsyncEnumerator();
	}

	#region IAsyncEnumerable<T> Members

	public async IAsyncEnumerator<T> GetAsyncEnumerator(
		CancellationToken cancellationToken = default)
	{
		foreach (T materializedItem in _materializedItems)
		{
			yield return materializedItem;
		}

		while (await _enumerator.MoveNextAsync())
		{
			if (cancellationToken.IsCancellationRequested)
			{
				break;
			}

			T item = _enumerator.Current;
			_materializedItems.Add(item);
			yield return item;
		}

		Count = _materializedItems.Count;
	}

	#endregion

	/// <inheritdoc cref="ICountable.Count" />
	public int? Count { get; private set; }

	/// <inheritdoc cref="IMaterializedEnumerable{T}.MaterializedItems" />
	IReadOnlyList<T> IMaterializedEnumerable<T>.MaterializedItems => _materializedItems;

	public static IAsyncEnumerable<T> Wrap(IAsyncEnumerable<T> enumerable)
	{
		if (enumerable is MaterializingAsyncEnumerable<T>)
		{
			return enumerable;
		}

		return new MaterializingAsyncEnumerable<T>(enumerable);
	}
}

#endif
