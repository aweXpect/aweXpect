using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionary
{
	/// <summary>
	///     Verifies that the dictionary contains the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>> ContainsValue<TKey,
		TValue>(
		this IThat<IDictionary<TKey, TValue>?> source,
		TValue expected)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainValueConstraint<TKey, TValue>(it, grammars, expected)),
			source
		);

	/// <summary>
	///     Verifies that the dictionary does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>>
		DoesNotContainValue<TKey, TValue>(
			this IThat<IDictionary<TKey, TValue>?> source,
			TValue unexpected)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainValueConstraint<TKey, TValue>(it, grammars, unexpected).Invert()),
			source
		);

	private sealed class ContainValueConstraint<TKey, TValue>(string it, ExpectationGrammars grammars, TValue expected)
		:ConstraintResult.WithNotNullValue<IDictionary<TKey, TValue>?>(it, grammars),
			IValueConstraint<IDictionary<TKey, TValue>?>
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue>? actual)
		{
			Actual = actual;
			Outcome = actual?.ContainsValue(expected) == true ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("contains value ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" contained only ");
			Formatter.Format(stringBuilder, Actual?.Values, FormattingOptions.MultipleLines);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("does not contain value ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" did");
		}
	}
}
