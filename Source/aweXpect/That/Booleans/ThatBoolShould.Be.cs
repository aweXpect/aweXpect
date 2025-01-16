using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatBoolShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<bool, IThatShould<bool>> Be(this IThatShould<bool> source,
		bool expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<bool, IThatShould<bool>> NotBe(this IThatShould<bool> source,
		bool unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, unexpected)),
			source);

	private readonly struct NotBeValueConstraint(string it, bool unexpected)
		: IValueConstraint<bool>
	{
		public ConstraintResult IsMetBy(bool actual)
		{
			if (!unexpected.Equals(actual))
			{
				return new ConstraintResult.Success<bool>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"not be {Formatter.Format(unexpected)}";
	}
}
