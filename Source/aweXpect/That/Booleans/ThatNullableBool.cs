using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="bool" />? values.
/// </summary>
public static partial class ThatNullableBool
{
	private readonly struct IsEqualToConstraint(string it, bool? expected) : IValueConstraint<bool?>
	{
		public ConstraintResult IsMetBy(bool? actual)
		{
			if (expected.Equals(actual))
			{
				return new ConstraintResult.Success<bool?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"is {Formatter.Format(expected)}";
	}

	private readonly struct IsNotEqualToConstraint(string it, bool? unexpected)
		: IValueConstraint<bool?>
	{
		public ConstraintResult IsMetBy(bool? actual)
		{
			if (!unexpected.Equals(actual))
			{
				return new ConstraintResult.Success<bool?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"is not {Formatter.Format(unexpected)}";
	}
}
