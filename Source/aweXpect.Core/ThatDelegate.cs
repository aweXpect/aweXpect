using aweXpect.Core;

namespace aweXpect;

/// <summary>
///     Expectations on delegate values.
/// </summary>
public abstract class ThatDelegate(ExpectationBuilder expectationBuilder)
{
	/// <summary>
	///     The expectation builder.
	/// </summary>
	public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;

	/// <summary>
	///     A delegate without value.
	/// </summary>
	public sealed class WithoutValue(ExpectationBuilder expectationBuilder)
		: ThatDelegate(expectationBuilder);

	/// <summary>
	///     A delegate with value of type <typeparamref name="TValue" />.
	/// </summary>
	public sealed class WithValue<TValue>(ExpectationBuilder expectationBuilder)
		: ThatDelegate(expectationBuilder);
}
