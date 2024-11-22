using System;
using Testably.Expectations.Results;

namespace Testably.Expectations;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public static partial class ThatExceptionShould
{
	/// <summary>
	///     Verifies that the actual <see cref="ArgumentException" /> has an <paramref name="expected" /> param name.
	/// </summary>
	public static AndOrResult<TException, ThatExceptionShould<TException>>
		HaveParamName<TException>(
			this ThatExceptionShould<TException> source,
			string expected)
		where TException : ArgumentException?
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasParamNameValueConstraint<TException>(it, "have", expected)),
			source);

	/// <summary>
	///     Verifies that the actual <see cref="ArgumentException" /> has an <paramref name="expected" /> param name.
	/// </summary>
	public static AndOrResult<TException, ThatDelegateThrows<TException>>
		WithParamName<TException>(
			this ThatDelegateThrows<TException> source,
			string expected)
		where TException : ArgumentException?
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasParamNameValueConstraint<TException>(it, "with", expected)),
			source);
}
