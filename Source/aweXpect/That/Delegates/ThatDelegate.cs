using Testably.Expectations.Core;

namespace Testably.Expectations;

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
	public sealed class WithoutValue : ThatDelegate
	{
		internal WithoutValue(ExpectationBuilder expectationBuilder)
			: base(expectationBuilder)
		{
		}
	}

	/// <summary>
	///     A delegate with value of type <typeparamref name="TValue" />.
	/// </summary>
	public sealed class WithValue<TValue> : ThatDelegate
	{
		internal WithValue(ExpectationBuilder expectationBuilder)
			: base(expectationBuilder)
		{
		}
	}
}
