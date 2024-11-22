using System;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public partial class ThatExceptionShould<TException>
{
	/// <summary>
	///     Verifies that the actual exception has a message equal to <paramref name="expected" />
	/// </summary>
	public StringMatcherResult<TException?, ThatExceptionShould<TException>>
		HaveMessage(StringMatcher expected)
		=> new(ExpectationBuilder.AddConstraint(it
				=> new ThatExceptionShould.HasMessageValueConstraint<TException>(
					it, "have", expected)),
			this,
			expected);
}
