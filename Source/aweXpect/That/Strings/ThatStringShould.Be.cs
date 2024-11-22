using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is equal to <paramref name="expected" />.
	/// </summary>
	public static StringMatcherResult<string?, IThat<string?>> Be(
		this IThat<string?> source,
		StringMatcher expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeConstraint(it, expected)),
			source,
			expected);

	private readonly struct BeConstraint(string it, StringMatcher expected)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (expected.Matches(actual))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure<string?>(actual, ToString(),
				expected.GetExtendedFailure(it, actual));
		}

		/// <inheritdoc />
		public override string ToString()
			=> expected.GetExpectation(StringMatcher.GrammaticVoice.ActiveVoice);
	}
}
