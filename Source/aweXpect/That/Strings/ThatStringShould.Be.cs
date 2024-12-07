using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is equal to <paramref name="expected" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> Be(
		this IThat<string?> source,
		string? expected)
	{
		var options = new StringEqualityOptions();
		return new StringEqualityTypeResult<string?, IThat<string?>>(source.ExpectationBuilder.AddConstraint(it
				=> new BeConstraint(it, expected, options)),
			source,
			options);
	}

	private readonly struct BeConstraint(string it, string? expected, StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (options.AreConsideredEqual(actual, expected))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure<string?>(actual, ToString(),
				options.GetExtendedFailure(it, expected, actual));
		}

		/// <inheritdoc />
		public override string ToString()
			=> options.GetExpectation(expected, true);
	}
}
