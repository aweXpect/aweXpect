﻿using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatBool
{
	/// <summary>
	///     Verifies that the subject implies the <paramref name="consequent" /> value.
	/// </summary>
	public static AndOrResult<bool, IThat<bool>> Implies(this IThat<bool> source,
		bool consequent)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new ImpliesConstraint(it, grammars, consequent)),
			source);

	private sealed class ImpliesConstraint(string it, ExpectationGrammars grammars, bool consequent)
		: ConstraintResult.WithValue<bool>(grammars),
			IValueConstraint<bool>
	{
		public ConstraintResult IsMetBy(bool actual)
		{
			Actual = actual;
			Outcome = !actual || consequent ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("implies ");
			Formatter.Format(stringBuilder, consequent, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it);
			stringBuilder.Append(" did not");
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("does not imply ");
			Formatter.Format(stringBuilder, consequent, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it);
			stringBuilder.Append(" did");
		}
	}
}
