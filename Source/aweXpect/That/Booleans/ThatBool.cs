using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="bool" /> values.
/// </summary>
public static partial class ThatBool
{
	private readonly struct BeValueConstraint(string it, bool expected) : IValueConstraint<bool>
	{
		public ConstraintResult IsMetBy(bool actual)
		{
			if (expected.Equals(actual))
			{
				return new ConstraintResult.Success<bool>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"be {Formatter.Format(expected)}";
	}
}
