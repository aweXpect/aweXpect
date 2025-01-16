﻿using System;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on delegate values.
/// </summary>
public static partial class ThatDelegate
{
	private static readonly string DoesNotThrowExpectation = "not throw any exception";

	private static ConstraintResult DoesNotThrowResult<TException>(Exception? exception)
		where TException : Exception?
	{
		if (exception is not null)
		{
			return new ConstraintResult.Failure<Exception?>(exception, DoesNotThrowExpectation,
				$"it did throw {exception.FormatForMessage()}",
				ConstraintResult.FurtherProcessing.IgnoreCompletely);
		}

		return new ConstraintResult.Success<TException?>(default, DoesNotThrowExpectation,
			ConstraintResult.FurtherProcessing.IgnoreCompletely);
	}

	private readonly struct ThrowsCastConstraint(Type exceptionType, ThrowsOption throwOptions)
		: IValueConstraint<Exception?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? value)
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowResult<Exception>(value);
			}

			if (exceptionType.IsAssignableFrom(value?.GetType()))
			{
				return new ConstraintResult.Success<Exception?>(value, ToString());
			}

			if (value is null)
			{
				return new ConstraintResult.Failure<Exception?>(null, ToString(), "it did not");
			}

			return new ConstraintResult.Failure<Exception?>(null, ToString(),
				$"it did throw {value.FormatForMessage()}");
		}

		public override string ToString()
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowExpectation;
			}

			return $"throw {exceptionType.Name.PrependAOrAn()}";
		}
	}

	private readonly struct ThrowExceptionOfTypeConstraint<TException>(ThrowsOption throwOptions)
		: IValueConstraint<Exception?>
		where TException : Exception
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? value)
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowResult<TException>(value);
			}

			if (value is TException typedException)
			{
				return new ConstraintResult.Success<TException?>(typedException, ToString());
			}

			if (value is null)
			{
				return new ConstraintResult.Failure<TException?>(null, ToString(),
					"it did not throw any exception",
					ConstraintResult.FurtherProcessing.IgnoreResult);
			}

			return new ConstraintResult.Failure<TException?>(null, ToString(),
				$"it did throw {value.FormatForMessage()}");
		}

		public override string ToString()
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowExpectation;
			}

			return typeof(TException) == typeof(Exception)
				? "throw an exception"
				: $"throw {typeof(TException).Name.PrependAOrAn()}";
		}
	}

	internal class ThrowsOption
	{
		public bool DoCheckThrow { get; private set; } = true;

		public void CheckThrow(bool doCheckThrow) => DoCheckThrow = doCheckThrow;
	}
}