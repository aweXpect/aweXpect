using aweXpect.Core;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	/// <summary>
	///     A delegate with value of type <typeparamref name="T" />.
	/// </summary>
	public sealed partial class WithValue<T>(ExpectationBuilder expectationBuilder)
		: ThatDelegate(expectationBuilder), IExpectThat<WithValue<T>>
	{
	}
}
