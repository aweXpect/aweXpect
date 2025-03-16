using System.Diagnostics;

namespace aweXpect.Core;

/// <summary>
///     Wraps the <see cref="ExpectationBuilder" />.
/// </summary>
[DebuggerDisplay("ThatSubject<{typeof(T)}>: {ExpectationBuilder}")]
public readonly struct ThatSubject<T>(ExpectationBuilder expectationBuilder)
	: IExpectThat<T>
{
	/// <inheritdoc cref="IExpectThat{T}.ExpectationBuilder" />
	public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;
}
