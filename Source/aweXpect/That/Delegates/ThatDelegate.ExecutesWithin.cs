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
			.AddConstraint((it, grammars) => new ExecutesWithinConstraint<TValue>(it, grammars, duration)));

	/// <summary>
	///     Verifies that the delegate finishes execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult ExecutesWithin(
		this IThat<Delegates.ThatDelegate.WithoutValue> source,
		TimeSpan duration)
		=> new(source.ThatIs().ExpectationBuilder
			.AddConstraint((it, grammars) => new ExecutesWithinConstraint(it, grammars, duration)));

	/// <summary>
	///     Verifies that the delegate does not finish execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult<TValue> DoesNotExecuteWithin<TValue>(
		this IThat<Delegates.ThatDelegate.WithValue<TValue>> source,
		TimeSpan duration)
		=> new(source.ThatIs().ExpectationBuilder
			.AddConstraint((it, grammars) => new DoesNotExecuteWithinConstraint<TValue>(it, grammars, duration)));

	/// <summary>
	///     Verifies that the delegate does not finish execution within the given <paramref name="duration" />.
	/// </summary>
	public static ExpectationResult DoesNotExecuteWithin(
		this IThat<Delegates.ThatDelegate.WithoutValue> source,
		TimeSpan duration)
		=> new(source.ThatIs().ExpectationBuilder
			.AddConstraint((it, grammars) => new DoesNotExecuteWithinConstraint(it, grammars, duration)));

	private class ExecutesWithinConstraint<TValue>(string it, ExpectationGrammars grammars, TimeSpan duration)
		: IValueConstraint<DelegateValue<TValue>>
	{
		public ConstraintResult IsMetBy(DelegateValue<TValue> actual)
		{
			_ = grammars;
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

	private class ExecutesWithinConstraint(string it, ExpectationGrammars grammars, TimeSpan duration)
		: IValueConstraint<DelegateValue>
	{
		public ConstraintResult IsMetBy(DelegateValue actual)
		{
			_ = grammars;
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

	private class DoesNotExecuteWithinConstraint<TValue>(string it, ExpectationGrammars grammars, TimeSpan duration)
		: IValueConstraint<DelegateValue<TValue>>
	{
		public ConstraintResult IsMetBy(DelegateValue<TValue> actual)
		{
			_ = grammars;
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

	private class DoesNotExecuteWithinConstraint(string it, ExpectationGrammars grammars, TimeSpan duration)
		: IValueConstraint<DelegateValue>
	{
		public ConstraintResult IsMetBy(DelegateValue actual)
		{
			_ = grammars;
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
