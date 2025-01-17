using System;
using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Guid" />? values.
/// </summary>
public static partial class ThatNullableGuid
{
	private readonly struct ValueConstraint(
		string it,
		string expectation,
		Func<Guid?, bool> successIf)
		: IValueConstraint<Guid?>
	{
		public ConstraintResult IsMetBy(Guid? actual)
		{
			if (successIf(actual))
			{
				return new ConstraintResult.Success<Guid?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}
}
