using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatException
{
	/// <summary>
	///     Verifies that the actual <see cref="Exception" /> has an <paramref name="expected" /> HResult.
	/// </summary>
	public static AndOrResult<TException, IThat<TException>> HasHResult<TException>(
		this IThat<TException> source,
		int expected)
		where TException : Exception?
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new HasHResultValueConstraint(it, "has", expected)),
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
