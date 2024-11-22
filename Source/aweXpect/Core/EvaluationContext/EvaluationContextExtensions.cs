using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Testably.Expectations.Core.EvaluationContext;

/// <summary>
///     Extension methods for the <see cref="IEvaluationContext" />.
/// </summary>
internal static class EvaluationContextExtensions
{
	private const string MaterializedEnumerableKey = nameof(MaterializedEnumerableKey);

	/// <summary>
	///     Avoids enumerating an <see cref="IEnumerable{TItem}" /> multiple times,
	///     by caching already materialized items in the <paramref name="evaluationContext" />.
	/// </summary>
	public static IEnumerable<TItem> UseMaterializedEnumerable<TItem, TCollection>(
		this IEvaluationContext evaluationContext, TCollection collection)
		where TCollection : IEnumerable<TItem>
	{
		if (evaluationContext.TryReceive(MaterializedEnumerableKey,
			out IEnumerable<TItem>? existingValue))
		{
			return existingValue;
		}

		IEnumerable<TItem> materializedEnumerable = MaterializingEnumerable<TItem>.Wrap(collection);
		// ReSharper disable once PossibleMultipleEnumeration
		evaluationContext.Store(MaterializedEnumerableKey, materializedEnumerable);
		// ReSharper disable once PossibleMultipleEnumeration
		return materializedEnumerable;
	}

	private sealed class MaterializingEnumerable<T> : IEnumerable<T>
	{
		private readonly IEnumerator<T> _enumerator;
		private readonly List<T> _materializedItems = new();

		private MaterializingEnumerable(IEnumerable<T> enumerable)
		{
			_enumerator = enumerable.GetEnumerator();
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
		}

		#endregion

		public static IEnumerable<T> Wrap(IEnumerable<T> enumerable)
		{
			if (enumerable is ICollection<T> or MaterializingEnumerable<T>)
			{
				return enumerable;
			}

			return new MaterializingEnumerable<T>(enumerable);
		}
	}
#if NET6_0_OR_GREATER
	private const string MaterializedAsyncEnumerableKey = nameof(MaterializedAsyncEnumerableKey);

	/// <summary>
	///     Avoids enumerating an <see cref="IEnumerable{TItem}" /> multiple times,
	///     by caching already materialized items in the <paramref name="evaluationContext" />.
	/// </summary>
	public static IAsyncEnumerable<TItem> UseMaterializedAsyncEnumerable<TItem, TCollection>(
		this IEvaluationContext evaluationContext, TCollection collection)
		where TCollection : IAsyncEnumerable<TItem>
	{
		if (evaluationContext.TryReceive(MaterializedAsyncEnumerableKey,
			out IAsyncEnumerable<TItem>? existingValue))
		{
			return existingValue;
		}

		IAsyncEnumerable<TItem> materializedEnumerable =
			MaterializingAsyncEnumerable<TItem>.Wrap(collection);
		// ReSharper disable once PossibleMultipleEnumeration
		evaluationContext.Store(MaterializedAsyncEnumerableKey, materializedEnumerable);
		// ReSharper disable once PossibleMultipleEnumeration
		return materializedEnumerable;
	}

	private sealed class MaterializingAsyncEnumerable<T> : IAsyncEnumerable<T>
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
		}

		#endregion

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
}
