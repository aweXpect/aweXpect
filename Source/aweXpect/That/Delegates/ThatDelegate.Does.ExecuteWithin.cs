using System;
using aweXpect.Core.Constraints;
using aweXpect.Core.Sources;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegate
{
	/// <summary>
	///     Verifies that the delegate finishes execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult<TValue> ExecuteWithin<TValue>(
		this Core.ThatDelegate.WithValue<TValue> source,
		TimeSpan duration)
		=> new(source.ExpectationBuilder
			.AddConstraint(_ => new ExecuteWithinConstraint<TValue>(duration)));

	/// <summary>
	///     Verifies that the delegate finishes execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult ExecuteWithin(
		this Core.ThatDelegate.WithoutValue source,
		TimeSpan duration)
		=> new(source.ExpectationBuilder
			.AddConstraint(_ => new ExecuteWithinConstraint(duration)));

	/// <summary>
	///     Verifies that the delegate does not finish execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult<TValue> NotExecuteWithin<TValue>(
		this Core.ThatDelegate.WithValue<TValue> source,
		TimeSpan duration)
		=> new(source.ExpectationBuilder
			.AddConstraint(_ => new NotExecuteWithinConstraint<TValue>(duration)));

	/// <summary>
	///     Verifies that the delegate does not finish execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult NotExecuteWithin(
		this Core.ThatDelegate.WithoutValue source,
		TimeSpan duration)
		=> new(source.ExpectationBuilder
			.AddConstraint(_ => new NotExecuteWithinConstraint(duration)));

	private readonly struct ExecuteWithinConstraint<TValue>(TimeSpan duration)
		: IValueConstraint<DelegateValue<TValue>>
	{
		public ConstraintResult IsMetBy(DelegateValue<TValue> actual)
		{
			if (actual.IsNull)
			{
				return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(), That.ItWasNull);
			}

			if (actual.Exception is { } exception)
			{
				return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
					$"it did throw {exception.FormatForMessage()}");
			}

			if (actual.Duration <= duration)
			{
				return new ConstraintResult.Success<TValue?>(actual.Value, ToString());
			}

			return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
				$"it took {Formatter.Format(actual.Duration)}");
		}

		public override string ToString()
			=> $"execute within {Formatter.Format(duration)}";
	}

	private readonly struct ExecuteWithinConstraint(TimeSpan duration)
		: IValueConstraint<DelegateValue>
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

			if (actual.Duration <= duration)
			{
				return new ConstraintResult.Success(ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"it took {Formatter.Format(actual.Duration)}");
		}

		public override string ToString()
			=> $"execute within {Formatter.Format(duration)}";
	}

	private readonly struct NotExecuteWithinConstraint<TValue>(TimeSpan duration)
		: IValueConstraint<DelegateValue<TValue>>
	{
		public ConstraintResult IsMetBy(DelegateValue<TValue> actual)
		{
			if (actual.IsNull)
			{
				return new ConstraintResult.Failure(ToString(), That.ItWasNull);
			}

			if (actual.Exception is not null || actual.Duration > duration)
			{
				return new ConstraintResult.Success<TValue?>(actual.Value, ToString());
			}

			return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
				$"it took only {Formatter.Format(actual.Duration)}");
		}

		public override string ToString()
			=> $"not execute within {Formatter.Format(duration)}";
	}

	private readonly struct NotExecuteWithinConstraint(TimeSpan duration)
		: IValueConstraint<DelegateValue>
	{
		public ConstraintResult IsMetBy(DelegateValue actual)
		{
			if (actual.IsNull)
			{
				return new ConstraintResult.Failure(ToString(), That.ItWasNull);
			}

			if (actual.Exception is not null || actual.Duration > duration)
			{
				return new ConstraintResult.Success(ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"it took only {Formatter.Format(actual.Duration)}");
		}

		public override string ToString()
			=> $"not execute within {Formatter.Format(duration)}";
	}
}
