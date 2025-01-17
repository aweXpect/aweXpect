using System;
using aweXpect.Results;

namespace aweXpect;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the actual <see cref="Exception" /> has an <paramref name="expected" /> HResult.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>>
		WithHResult(int expected)
		=> new(ExpectationBuilder.AddConstraint(it
				=> new ThatException.HasHResultValueConstraint(it, "with", expected)),
			this);
}
