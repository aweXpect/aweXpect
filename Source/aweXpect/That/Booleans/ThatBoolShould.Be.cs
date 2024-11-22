using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatBoolShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<bool, IThat<bool>> Be(this IThat<bool> source,
		bool expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<bool, IThat<bool>> NotBe(this IThat<bool> source,
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
