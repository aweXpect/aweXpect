using System.Diagnostics;

namespace aweXpect.Core;

/// <summary>
/// Wraps the <see cref="ExpectationBuilder"/>.
/// </summary>
[DebuggerDisplay("Expect.ThatSubject<{typeof(T)}>: {ExpectationBuilder}")]
public readonly struct ThatSubject<T>(ExpectationBuilder expectationBuilder)
	: IThat<T>, IThatShould<T>, IThatIs<T>, IThatHas<T>
{
	/// <inheritdoc cref="IThatVerb{T}.ExpectationBuilder" />
	public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;
}
