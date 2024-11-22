using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.Helpers;

namespace Testably.Expectations;

/// <summary>
///     Expectations on delegate values.
/// </summary>
public static partial class ThatDelegateShould
{
	private static readonly string DoesNotThrowExpectation = "not throw any exception";

	/// <summary>
	///     Start expectations for the current <see cref="Action" /> <paramref name="subject" />.
	/// </summary>
	public static ThatDelegate.WithoutValue Should(
		this IExpectSubject<ThatDelegate.WithoutValue> subject)
		=> new(subject.Should(_ => { }).ExpectationBuilder);

	/// <summary>
	///     Start expectations for the current <see cref="Func{TValue}" /> <paramref name="subject" />.
	/// </summary>
	public static ThatDelegate.WithValue<TValue> Should<TValue>(
		this IExpectSubject<ThatDelegate.WithValue<TValue>> subject)
		=> new(subject.Should(_ => { }).ExpectationBuilder);

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

			return $"throw {typeof(TException).Name.PrependAOrAn()}";
		}
	}
}
