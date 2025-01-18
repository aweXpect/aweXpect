using System.Diagnostics;

namespace aweXpect.Core;

/// <summary>
///     Wraps the <see cref="ExpectationBuilder" />.
/// </summary>
[DebuggerDisplay("ThatSubject<{typeof(T)}>: {ExpectationBuilder}")]
public readonly struct ThatSubject<T>(ExpectationBuilder expectationBuilder)
	: IThat<T>, IThatDoes<T>, IThatHas<T>, IThatIs<T>
{
	/// <inheritdoc cref="IThatVerb{T}.ExpectationBuilder" />
	public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;
}
