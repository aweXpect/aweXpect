using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegateThrows
{
	/// <summary>
	///     Verifies that the actual <see cref="Exception" /> has an <paramref name="expected" /> HResult.
	/// </summary>
	public static AndOrResult<TException, IThatDelegateThrows<TException>> WithHResult<TException>(
		this IThatDelegateThrows<TException> source,
		int expected)
		where TException : Exception?
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ThatException.HasHResultValueConstraint(it, "with", expected)),
			source);
}
