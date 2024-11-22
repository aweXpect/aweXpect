// ReSharper disable once CheckNamespace

namespace aweXpect;

/// <summary>
///     The result when checking the condition in an <see cref="ICollectionEvaluator{TItem}" />.
/// </summary>
public record struct CollectionEvaluatorResult(bool? IsSuccess, string Error);
