﻿using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the thrown exception has a message equal to <paramref name="expected" />
	/// </summary>
	public StringEqualityResult<TException, ThatDelegateThrows<TException>> WithMessage(string expected)
	{
		StringEqualityOptions? options = new();
		return new StringEqualityResult<TException, ThatDelegateThrows<TException>>(ExpectationBuilder.AddConstraint(it
				=> new ThatExceptionShould.HasMessageValueConstraint<TException>(
					it, "with", expected, options)),
			this,
			options);
	}
}
