using System;

namespace aweXpect.Core;

/// <summary>
///     The matcher for collections.
/// </summary>
public interface ICollectionMatcher<in T, out T2> : IDisposable
	where T : T2
{
	/// <summary>
	///     Verifies for each <paramref name="value" /> in the subject, if it results in a failure.
	/// </summary>
	/// <returns><see langword="null" /> when it results in no failure, the failure text otherwise.</returns>
	string? Verify(string it, T value, IOptionsEquality<T2> options);

	/// <summary>
	///     Verifies if it results in a failure when the enumeration is complete.
	/// </summary>
	/// <returns><see langword="null" /> when it results in no failure, the failure text otherwise.</returns>
	string? VerifyComplete(string it, IOptionsEquality<T2> options);
}
