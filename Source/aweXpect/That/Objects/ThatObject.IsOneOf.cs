using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>, object?> IsOneOf(
		this IThat<object?> source,
		params object?[] expected)
	{
		ObjectEqualityOptions<object?> options = new();
		return new ObjectEqualityResult<object?, IThat<object?>, object?>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOneOfConstraint<object?, object?>(it, grammars, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>, object?> IsOneOf(
		this IThat<object?> source,
		IEnumerable<object?> expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<object?> options = new();
		return new ObjectEqualityResult<object?, IThat<object?>, object?>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOneOfConstraint<object?, object?>(it, grammars, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>, object?> IsNotOneOf(
		this IThat<object?> source,
		params object?[] unexpected)
	{
		ObjectEqualityOptions<object?> options = new();
		return new ObjectEqualityResult<object?, IThat<object?>, object?>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOneOfConstraint<object?, object?>(it, grammars, unexpected, options)
					.Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>, object?> IsNotOneOf(
		this IThat<object?> source,
		IEnumerable<object?> unexpected,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<object?> options = new();
		return new ObjectEqualityResult<object?, IThat<object?>, object?>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOneOfConstraint<object?, object?>(it, grammars, unexpected, options)
					.Invert()),
			source,
			options);
	}

	private sealed class IsOneOfConstraint<TSubject, TExpected>(
		string it,
		ExpectationGrammars grammars,
		IEnumerable<TExpected?> expected,
		ObjectEqualityOptions<TSubject> options)
		: ConstraintResult.WithNotNullValue<TSubject>(it, grammars),
			IValueConstraint<TSubject>
	{
		public ConstraintResult IsMetBy(TSubject actual)
		{
			Actual = actual;
			Outcome = expected.Any(value => options.AreConsideredEqual(actual, value)) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation("one of " + Formatter.Format(expected).TrimCommonWhiteSpace(),
				Grammars));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(It, Grammars, Actual, expected));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation("one of " + Formatter.Format(expected).TrimCommonWhiteSpace(),
				Grammars));

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(It, Grammars, Actual, expected));
	}
}
