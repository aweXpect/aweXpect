using System.Collections.Generic;
using aweXpect.Core.EvaluationContext;

namespace aweXpect.Helpers;

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

#if NET8_0_OR_GREATER
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
#endif
}
