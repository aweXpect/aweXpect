﻿using System;
using aweXpect.Core;
using aweXpect.Delegates;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegateThrows
{
	/// <summary>
	///     Verifies that the actual <see cref="ArgumentException" /> has an <paramref name="expected" /> param name.
	/// </summary>
	/// <remarks>
	///     If <paramref name="expected" /> is <see langword="null" />, does not verify anything.
	/// </remarks>
	public static AndOrResult<TException, ThatDelegateThrows<TException>> WithParamName<TException>(
		this ThatDelegateThrows<TException> source,
		string? expected)
		where TException : ArgumentException?
	{
		if (expected == null)
		{
			return new AndOrResult<TException, ThatDelegateThrows<TException>>(source.ExpectationBuilder, source);
		}

		return new AndOrResult<TException, ThatDelegateThrows<TException>>(
			source.ExpectationBuilder.AddConstraint((it, grammars)
				=> new ThatException.HasParamNameValueConstraint<TException>(
					it,
					grammars | ExpectationGrammars.Active | ExpectationGrammars.Nested,
					expected)),
			source);
	}
}
