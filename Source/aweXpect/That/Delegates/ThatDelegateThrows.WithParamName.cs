﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegateThrows
{
	/// <summary>
	///     Verifies that the actual <see cref="ArgumentException" /> has an <paramref name="expected" /> param name.
	/// </summary>
	public static AndOrResult<TException, IThatDelegateThrows<TException>> WithParamName<TException>(
			this IThatDelegateThrows<TException> source,
			string expected)
		where TException : ArgumentException?
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ThatException.HasParamNameValueConstraint<TException>(it, "with", expected)),
			source);
}
