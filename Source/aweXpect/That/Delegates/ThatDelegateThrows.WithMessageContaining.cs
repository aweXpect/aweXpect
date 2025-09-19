﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Delegates;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegateThrows
{
	/// <summary>
	///     Verifies that the thrown exception has a message that contains the <paramref name="expected" /> pattern.
	/// </summary>
	public static StringEqualityResult<TException, ThatDelegateThrows<TException>> WithMessageContaining<TException>(
		this ThatDelegateThrows<TException> source,
		string? expected)
		where TException : Exception?
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<TException, ThatDelegateThrows<TException>>(
			source.ExpectationBuilder.AddConstraint((it, grammars)
				=> new ThatException.HasMessageContainingConstraint(
					source.ExpectationBuilder,
					it,
					grammars | ExpectationGrammars.Active | ExpectationGrammars.Nested,
					expected,
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the thrown exception does not have a message that contains the <paramref name="unexpected" />
	///     pattern.
	/// </summary>
	public static StringEqualityResult<TException, ThatDelegateThrows<TException>> WithoutMessageContaining<TException>(
		this ThatDelegateThrows<TException> source,
		string? unexpected)
		where TException : Exception?
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<TException, ThatDelegateThrows<TException>>(
			source.ExpectationBuilder.AddConstraint((it, grammars)
				=> new ThatException.HasMessageContainingConstraint(
					source.ExpectationBuilder,
					it,
					grammars | ExpectationGrammars.Active | ExpectationGrammars.Nested,
					unexpected,
					options).Invert()),
			source,
			options);
	}
}
