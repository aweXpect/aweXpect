namespace aweXpect.Core;

/// <summary>
///     Base class for expectations, containing an <see cref="ExpectationBuilder" />.
/// </summary>
// ReSharper disable once UnusedTypeParameter
public interface IThatIs<out T> : IThatVerb<T>
{
}
