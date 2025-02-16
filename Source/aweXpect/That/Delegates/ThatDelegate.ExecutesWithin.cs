using System;
using aweXpect.Core;
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
	public static ExpectationResult<TValue> ExecutesWithin<TValue>(
		this IThat<Delegates.ThatDelegate.WithValue<TValue>> source,
		TimeSpan duration)
		=> new(source.ThatIs().ExpectationBuilder
			.AddConstraint((it, _) => new ExecutesWithinConstraint<TValue>(it, duration)));

	/// <summary>
	///     Verifies that the delegate finishes execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult ExecutesWithin(
		this IThat<Delegates.ThatDelegate.WithoutValue> source,
		TimeSpan duration)
		=> new(source.ThatIs().ExpectationBuilder
			.AddConstraint((it, _) => new ExecutesWithinConstraint(it, duration)));

	/// <summary>
	///     Verifies that the delegate does not finish execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult<TValue> DoesNotExecuteWithin<TValue>(
		this IThat<Delegates.ThatDelegate.WithValue<TValue>> source,
		TimeSpan duration)
		=> new(source.ThatIs().ExpectationBuilder
			.AddConstraint((it, _) => new DoesNotExecuteWithinConstraint<TValue>(it, duration)));

	/// <summary>
	///     Verifies that the delegate does not finish execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult DoesNotExecuteWithin(
		this IThat<Delegates.ThatDelegate.WithoutValue> source,
		TimeSpan duration)
		=> new(source.ThatIs().ExpectationBuilder
			.AddConstraint((it, _) => new DoesNotExecuteWithinConstraint(it, duration)));

	private readonly struct ExecutesWithinConstraint<TValue>(string it, TimeSpan duration)
		: IValueConstraint<DelegateValue<TValue>>
	{
		public ConstraintResult IsMetBy(DelegateValue<TValue> actual)
		{
			if (actual.IsNull)
			{
				return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(), That.ItWasNull);
			}

			if (actual.Exception is OperationCanceledException)
			{
				return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
					$"{it} was canceled within {Formatter.Format(actual.Duration)}");
			}

			if (actual.Exception is { } exception)
			{
				return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
					$"{it} did throw {exception.FormatForMessage()}");
			}

			if (actual.Duration <= duration)
			{
				return new ConstraintResult.Success<TValue?>(actual.Value, ToString());
			}

			return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
				$"{it} took {Formatter.Format(actual.Duration)}");
		}

		public override string ToString()
			=> $"executes within {Formatter.Format(duration)}";
	}

	private readonly struct ExecutesWithinConstraint(string it, TimeSpan duration)
		: IValueConstraint<DelegateValue>
	{
		public ConstraintResult IsMetBy(DelegateValue actual)
		{
			if (actual.IsNull)
			{
				return new ConstraintResult.Failure(ToString(), That.ItWasNull);
			}

			if (actual.Exception is OperationCanceledException)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was canceled within {Formatter.Format(actual.Duration)}");
			}

			if (actual.Exception is { } exception)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} did throw {exception.FormatForMessage()}");
			}

			if (actual.Duration <= duration)
			{
				return new ConstraintResult.Success(ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} took {Formatter.Format(actual.Duration)}");
		}

		public override string ToString()
			=> $"executes within {Formatter.Format(duration)}";
	}

	private readonly struct DoesNotExecuteWithinConstraint<TValue>(string it, TimeSpan duration)
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
				$"{it} took only {Formatter.Format(actual.Duration)}");
		}

		public override string ToString()
			=> $"does not execute within {Formatter.Format(duration)}";
	}

	private readonly struct DoesNotExecuteWithinConstraint(string it, TimeSpan duration)
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
				$"{it} took only {Formatter.Format(actual.Duration)}");
		}

		public override string ToString()
			=> $"does not execute within {Formatter.Format(duration)}";
	}
}
