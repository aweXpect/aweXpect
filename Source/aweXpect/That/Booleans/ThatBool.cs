using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="bool" /> values.
/// </summary>
public static partial class ThatBool
{
	private class IsEqualToConstraint : ConstraintResult<bool>,
			IValueConstraint<bool>
	{
		private readonly string _it;
		private readonly bool _expected;

		public IsEqualToConstraint(string it, ExpectationGrammars grammars, bool expected) : base(grammars)
		{
			_it = it;
			_expected = expected;
		}

		public ConstraintResult IsMetBy(bool actual)
		{
			Actual = actual;
			Outcome = _expected.Equals(actual) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is ");
			Formatter.Format(stringBuilder, _expected, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(_it);
			stringBuilder.Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not ");
			Formatter.Format(stringBuilder, _expected, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(_it);
			stringBuilder.Append(" was");
		}
	}
}
