using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public static partial class ThatException
{
	private readonly struct HasMessageValueConstraint<TException>(
		string it,
		ExpectationGrammars grammar,
		string expected,
		StringEqualityOptions options)
		: IValueConstraint<Exception?>
		where TException : Exception?
	{
		public ConstraintResult IsMetBy(Exception? actual)
		{
			if (options.AreConsideredEqual(actual?.Message, expected))
			{
				return new ConstraintResult.Success<TException?>(actual as TException, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				options.GetExtendedFailure(it, actual?.Message, expected));
		}

		public override string ToString()
			=> grammar switch
			{
				ExpectationGrammars.Nested => $" Message is {options.GetExpectation(expected, grammar)}",
				_ => $"has Message {options.GetExpectation(expected, grammar)}",
			};
	}

	private readonly struct HasInnerExceptionValueConstraint<TInnerException>(
		string verb,
		string it)
		: IValueConstraint<Exception?>
		where TInnerException : Exception?
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? actual)
		{
			Exception? innerException = actual?.InnerException;
			if (actual?.InnerException is TInnerException)
			{
				return new ConstraintResult.Success<Exception?>(actual, ToString());
			}

			if (innerException is not null)
			{
				return new ConstraintResult.Failure<Exception?>(actual, ToString(),
					$"{it} was {innerException.FormatForMessage()}");
			}

			return new ConstraintResult.Failure<Exception?>(actual, ToString(),
				$"{it} was <null>");
		}

		public override string ToString()
			=> $"{verb} an inner {(typeof(TInnerException) == typeof(Exception) ? "exception" : Formatter.Format(typeof(TInnerException)))}";
	}

	private readonly struct HasInnerExceptionValueConstraint(
		Type innerExceptionType,
		string verb,
		string it)
		: IValueConstraint<Exception?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? actual)
		{
			Exception? innerException = actual?.InnerException;
			if (innerExceptionType.IsAssignableFrom(actual?.InnerException?.GetType()))
			{
				return new ConstraintResult.Success<Exception?>(actual, ToString());
			}

			if (innerException is not null)
			{
				return new ConstraintResult.Failure<Exception?>(actual, ToString(),
					$"{it} was {innerException.FormatForMessage()}");
			}

			return new ConstraintResult.Failure<Exception?>(actual, ToString(),
				$"{it} was <null>");
		}

		public override string ToString()
			=> $"{verb} an inner {(innerExceptionType == typeof(Exception) ? "exception" : Formatter.Format(innerExceptionType))}";
	}

	private readonly struct InnerExceptionIsTypeConstraint<TInnerException>(string it)
		: IValueConstraint<Exception?>
		where TInnerException : Exception?
	{
		#region IValueConstraint<Exception?> Members

		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? actual)
		{
			if (actual?.InnerException is TInnerException)
			{
				return new ConstraintResult.Success<Exception?>(actual, "");
			}

			return new ConstraintResult.Failure<Exception?>(actual, "",
				actual == null
					? $"{it} was <null>"
					: $"{it} was {actual.InnerException?.FormatForMessage()}");
		}

		#endregion
	}

	private readonly struct InnerExceptionIsTypeConstraint(string it, Type exceptionType)
		: IValueConstraint<Exception?>
	{
		#region IValueConstraint<Exception?> Members

		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? actual)
		{
			if (exceptionType.IsAssignableFrom(actual?.InnerException?.GetType()))
			{
				return new ConstraintResult.Success<Exception>(actual, "");
			}

			return new ConstraintResult.Failure<Exception?>(actual, "",
				actual == null
					? $"{it} was <null>"
					: $"{it} was {actual.FormatForMessage()}");
		}

		#endregion
	}
}
