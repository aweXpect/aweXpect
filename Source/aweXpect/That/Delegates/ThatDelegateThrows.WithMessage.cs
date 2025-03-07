using System;
using aweXpect.Core;
using aweXpect.Delegates;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegateThrows
{
	/// <summary>
	///     Verifies that the thrown exception has a message equal to <paramref name="expected" />
	/// </summary>
	public static StringEqualityTypeResult<TException, ThatDelegateThrows<TException>> WithMessage<TException>(
		this ThatDelegateThrows<TException> source,
		string expected)
		where TException : Exception?
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<TException, ThatDelegateThrows<TException>>(
			source.ExpectationBuilder.AddConstraint((it, grammars)
				=> new ThatException.HasMessageValueConstraint(
					source.ExpectationBuilder,
					it,
					grammars | ExpectationGrammars.Active | ExpectationGrammars.Nested,
					expected,
					options)),
			source,
			options);
	}
}
