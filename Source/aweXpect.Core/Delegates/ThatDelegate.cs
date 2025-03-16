using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Sources;

namespace aweXpect.Delegates;

/// <summary>
///     Expectations on delegate values.
/// </summary>
public abstract partial class ThatDelegate(ExpectationBuilder expectationBuilder)
{
	/// <summary>
	///     The expectation builder.
	/// </summary>
	public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;

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

	private sealed class DelegateIsNotNullConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult(grammars),
			IValueConstraint<DelegateValue>
	{
		public ConstraintResult IsMetBy(DelegateValue value)
		{
			Outcome = value.IsNull ? Outcome.Failure : Outcome.Success;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			// Do nothing
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.ItWasNull(it);

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			value = default;
			return false;
		}

		public override ConstraintResult Negate()
			=> this;
	}

	internal class ThrowsOption
	{
		public bool DoCheckThrow { get; private set; } = true;

		public void CheckThrow(bool doCheckThrow) => DoCheckThrow = doCheckThrow;
	}
}
