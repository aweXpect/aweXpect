using System;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public partial class ThatExceptionShould<TException>
{
	/// <summary>
	///     Verifies that the actual exception has a message equal to <paramref name="expected" />
	/// </summary>
	public StringEqualityTypeResult<TException?, ThatExceptionShould<TException>>
		HaveMessage(string expected)
	{
		StringEqualityOptions options = new StringEqualityOptions();
		return new StringEqualityTypeResult<TException?, ThatExceptionShould<TException>>(
			ExpectationBuilder.AddConstraint(it
				=> new ThatExceptionShould.HasMessageValueConstraint<TException>(
					it, "have", expected, options)),
			this,
			options);
	}
}
