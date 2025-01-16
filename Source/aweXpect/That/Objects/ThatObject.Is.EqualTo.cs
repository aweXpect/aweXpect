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
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IExpectSubject<object?>> EqualTo(
		this IThatIs<object?> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<object?, IExpectSubject<object?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new IsEqualToConstraint(it, expected, doNotPopulateThisValue, options)),
			source.ExpectSubject(),
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThatShould<object?>> NotEqualTo(
		this IThatShould<object?> source,
		object? unexpected,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<object?, IThatShould<object?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new IsNotEqualToConstraint(it, unexpected, doNotPopulateThisValue, options)),
			source,
			options);
	}

	private readonly struct IsEqualToConstraint(
		string it,
		object? expected,
		string expectedExpression,
		ObjectEqualityOptions options)
		: IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (options.AreConsideredEqual(actual, expected))
			{
				return new ConstraintResult.Success<object?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), options.GetExtendedFailure(it, actual, expected));
		}

		public override string ToString()
			=> options.GetExpectation(expectedExpression);
	}

	private readonly struct IsNotEqualToConstraint(
		string it,
		object? unexpected,
		string unexpectedExpression,
		ObjectEqualityOptions options)
		: IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (options.AreConsideredEqual(actual, unexpected))
			{
				return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
			}

			return new ConstraintResult.Success<object?>(actual, ToString());
		}

		public override string ToString()
			=> "not " + options.GetExpectation(unexpectedExpression);
	}
}
