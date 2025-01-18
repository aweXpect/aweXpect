using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;

namespace aweXpect.Delegates;

/// <summary>
///     Expectations on delegate values.
/// </summary>
public abstract partial class ThatDelegate(ExpectationBuilder expectationBuilder)
{
	private static readonly string DoesNotThrowExpectation = "not throw any exception";
	private static readonly string ItWasNull = "it was <null>";

	/// <summary>
	///     The expectation builder.
	/// </summary>
	public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;

	private static ConstraintResult DoesNotThrowResult<TException>(Exception? exception)
		where TException : Exception?
	{
		if (exception is not null)
		{
			return new ConstraintResult.Failure<Exception?>(exception, DoesNotThrowExpectation,
				$"it did throw {FormatForMessage(exception)}",
				ConstraintResult.FurtherProcessing.IgnoreCompletely);
		}

		return new ConstraintResult.Success<TException?>(default, DoesNotThrowExpectation,
			ConstraintResult.FurtherProcessing.IgnoreCompletely);
	}

	internal static string FormatForMessage(Exception? exception)
	{
		if (exception is null)
		{
			return "<null>";
		}

		string message = exception.GetType().Name.PrependAOrAn();
		if (!string.IsNullOrEmpty(exception.Message))
		{
			message += ":" + Environment.NewLine + exception.Message.Indent();
		}

		return message;
	}

	internal class ThrowsOption
	{
		public bool DoCheckThrow { get; private set; } = true;

		public void CheckThrow(bool doCheckThrow) => DoCheckThrow = doCheckThrow;
	}
}
