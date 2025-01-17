using aweXpect.Core.Constraints;
using aweXpect.Core.Sources;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegate
{
	/// <summary>
	///     Verifies that the delegate does not throw any exception.
	/// </summary>
	public static ExpectationResult<TValue> NotThrow<TValue>(
		this Core.ThatDelegate.WithValue<TValue> source)
		=> new(source.ExpectationBuilder
			.AddConstraint(_ => new DoesNotThrowConstraint<TValue>()));

	/// <summary>
	///     Verifies that the delegate does not throw any exception.
	/// </summary>
	public static ExpectationResult NotThrow(this Core.ThatDelegate.WithoutValue source)
		=> new(source.ExpectationBuilder
			.AddConstraint(_ => new DoesNotThrowConstraint()));

	private readonly struct DoesNotThrowConstraint : IValueConstraint<DelegateValue>
	{
		public ConstraintResult IsMetBy(DelegateValue actual)
		{
			if (actual.IsNull)
			{
				return new ConstraintResult.Failure(ToString(), That.ItWasNull);
			}

			if (actual.Exception is { } exception)
			{
				return new ConstraintResult.Failure(ToString(),
					$"it did throw {exception.FormatForMessage()}");
			}

			return new ConstraintResult.Success(ToString());
		}

		public override string ToString()
			=> DoesNotThrowExpectation;
	}

	private readonly struct DoesNotThrowConstraint<TValue> : IValueConstraint<DelegateValue<TValue>>
	{
		public ConstraintResult IsMetBy(DelegateValue<TValue> actual)
		{
			if (actual.IsNull)
			{
				return new ConstraintResult.Failure<TValue?>(default, ToString(), That.ItWasNull);
			}

			if (actual.Exception is { } exception)
			{
				return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
					$"it did throw {exception.FormatForMessage()}");
			}

			return new ConstraintResult.Success<TValue?>(actual.Value, ToString());
		}

		public override string ToString()
			=> DoesNotThrowExpectation;
	}
}
