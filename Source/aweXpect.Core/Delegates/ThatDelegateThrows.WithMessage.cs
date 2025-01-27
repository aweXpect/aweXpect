using System;
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
			ExpectationBuilder.AddConstraint(it
				=> new HasMessageValueConstraint(
					it, "with", expected, options)),
			this,
			options);
	}

	internal readonly struct HasMessageValueConstraint(
		string it,
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

			return new ConstraintResult.Failure(ToString(),
				options.GetExtendedFailure(it, actual?.Message, expected));
		}

		public override string ToString()
			=> $"{verb} Message {options.GetExpectation(expected, false)}";
	}
}
