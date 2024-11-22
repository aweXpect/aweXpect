using System;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.Helpers;
using Testably.Expectations.Core.Sources;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatDelegateShould
{
	/// <summary>
	///     Verifies that the delegate does not throw any exception.
	/// </summary>
	public static ExpectationResult<TValue> NotThrow<TValue>(
		this ThatDelegate.WithValue<TValue> source)
		=> new(source.ExpectationBuilder
			.AddConstraint(_ => new DoesNotThrowConstraint<TValue>()));

	/// <summary>
	///     Verifies that the delegate does not throw any exception.
	/// </summary>
	public static ExpectationResult NotThrow(this ThatDelegate.WithoutValue source)
		=> new(source.ExpectationBuilder
			.AddConstraint(_ => new DoesNotThrowConstraint()));

	private readonly struct DoesNotThrowConstraint : IValueConstraint<DelegateValue>
	{
		public ConstraintResult IsMetBy(DelegateValue? actual)
		{
			if (actual?.Exception is { } exception)
			{
				return new ConstraintResult.Failure(ToString(),
					$"it did throw {exception.FormatForMessage()}");
			}

			if (actual is not null)
			{
				return new ConstraintResult.Success(ToString());
			}

			throw new InvalidOperationException("Received <null> in DoesNotThrowConstraint.");
		}

		public override string ToString()
			=> DoesNotThrowExpectation;
	}

	private readonly struct DoesNotThrowConstraint<TValue> : IValueConstraint<DelegateValue<TValue>>
	{
		public ConstraintResult IsMetBy(DelegateValue<TValue>? actual)
		{
			if (actual?.Exception is { } exception)
			{
				return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
					$"it did throw {exception.FormatForMessage()}");
			}

			if (actual is not null)
			{
				return new ConstraintResult.Success<TValue?>(actual.Value, ToString());
			}

			throw new InvalidOperationException("Received <null> in DoesNotThrowConstraint.");
		}

		public override string ToString()
			=> DoesNotThrowExpectation;
	}
}
