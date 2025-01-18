namespace aweXpect.Core;

/// <summary>
///     Base class for expectations, containing an <see cref="ExpectationBuilder" />.
/// </summary>
// ReSharper disable once UnusedTypeParameter
public interface IThatShould<out T> : IThatVerb<T>
{
}
