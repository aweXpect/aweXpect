using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect.Delegates;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the thrown exception has a message equal to <paramref name="expected" />
	/// </summary>
	public StringEqualityTypeResult<TException, ThatDelegateThrows<TException>> WithMessage(
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<TException, ThatDelegateThrows<TException>>(
			ExpectationBuilder.AddConstraint((it, grammars)
				=> new HasMessageValueConstraint(
					ExpectationBuilder, it, grammars, "with", expected, options)),
			this,
			options);
	}

	internal readonly struct HasMessageValueConstraint(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string verb,
		string expected,
		StringEqualityOptions options)
		: IValueConstraint<Exception?>
	{
		public ConstraintResult IsMetBy(Exception? actual)
		{
			if (options.AreConsideredEqual(actual?.Message, expected))
			{
				return new ConstraintResult.Success<TException?>(actual as TException, ToString());
			}

			expectationBuilder.UpdateContexts(contexts => contexts
				.Add(new ResultContext("Message", actual?.Message)));
			return new ConstraintResult.Failure(ToString(),
					options.GetExtendedFailure(it, grammars, actual?.Message, expected));
		}

		public override string ToString()
			=> $"{verb} Message {options.GetExpectation(expected, grammars)}";
	}
}
