using System;
using aweXpect.Delegates;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegateThrows
{
	/// <summary>
	///     Verifies that the actual <see cref="Exception" /> has an <paramref name="expected" /> HResult.
	/// </summary>
	public static AndOrResult<TException, ThatDelegateThrows<TException>> WithHResult<TException>(
		this ThatDelegateThrows<TException> source,
		int expected)
		where TException : Exception?
		=> new(source.ExpectationBuilder.AddConstraint((it, form)
				=> new ThatException.HasHResultValueConstraint(it, "with", expected)),
			source);
}
