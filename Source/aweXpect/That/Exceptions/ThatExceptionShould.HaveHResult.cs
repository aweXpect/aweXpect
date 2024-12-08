using System;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public static partial class ThatExceptionShould
{
	/// <summary>
	///     Verifies that the actual <see cref="Exception" /> has an <paramref name="expected" /> HResult.
	/// </summary>
	public static AndOrResult<TException, ThatExceptionShould<TException>>
		HaveHResult<TException>(
			this ThatExceptionShould<TException> source,
			int expected)
		where TException : Exception?
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasHResultValueConstraint(it, "have", expected)),
			source);

	/// <summary>
	///     Verifies that the actual <see cref="Exception" /> has an <paramref name="expected" /> HResult.
	/// </summary>
	public static AndOrResult<TException, ThatDelegateThrows<TException>>
		WithHResult<TException>(
			this ThatDelegateThrows<TException> source,
			int expected)
		where TException : Exception?
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasHResultValueConstraint(it, "with", expected)),
			source);

	internal readonly struct HasHResultValueConstraint(
		string it,
		string verb,
		int expected)
		: IValueConstraint<Exception?>
	{
		public ConstraintResult IsMetBy(Exception? actual)
		{
			if (actual == null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was <null>");
			}
			if (actual.HResult == expected)
			{
				return new ConstraintResult.Success<Exception?>(actual,
					ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} had HResult {Formatter.Format(actual.HResult)}");
		}

		public override string ToString()
			=> $"{verb} HResult {Formatter.Format(expected)}";
	}
}
