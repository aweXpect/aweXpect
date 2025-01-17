using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject is equal to <paramref name="expected" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IExpectSubject<string?>> Is(
		this IExpectSubject<string?> source,
		string? expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IExpectSubject<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeConstraint(it, expected, options)),
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
				options.GetExtendedFailure(it, actual, expected));
		}

		/// <inheritdoc />
		public override string ToString()
			=> options.GetExpectation(expected, true);
	}
}
