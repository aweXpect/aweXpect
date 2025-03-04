using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="bool" /> values.
/// </summary>
public static partial class ThatBool
{
	private class IsEqualToConstraint(string it, ExpectationGrammars grammars, bool expected)
		: ConstraintResult<bool>(grammars), IValueConstraint<bool>
	{
		public ConstraintResult IsMetBy(bool actual)
		{
			Actual = actual;
			Outcome = expected.Equals(actual) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is ");
			Formatter.Format(stringBuilder, expected, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it);
			stringBuilder.Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not ");
			Formatter.Format(stringBuilder, expected, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it);
			stringBuilder.Append(" was");
		}
	}
}
