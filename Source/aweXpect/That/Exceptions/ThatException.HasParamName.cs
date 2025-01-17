using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public static partial class ThatException
{
	/// <summary>
	///     Verifies that the actual <see cref="ArgumentException" /> has an <paramref name="expected" /> param name.
	/// </summary>
	public static AndOrResult<TException, IExpectSubject<TException>> HasParamName<TException>(
			this IExpectSubject<TException> source,
			string expected)
		where TException : ArgumentException?
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new HasParamNameValueConstraint<TException>(it, "have", expected)),
			source);

	internal readonly struct HasParamNameValueConstraint<TArgumentException>(
		string it,
		string verb,
		string expected)
		: IValueConstraint<Exception?>
		where TArgumentException : ArgumentException?
	{
		public ConstraintResult IsMetBy(Exception? actual)
		{
			if (actual == null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was <null>");
			}

			if (actual is TArgumentException argumentException)
			{
				if (argumentException.ParamName == expected)
				{
					return new ConstraintResult.Success<TArgumentException?>(argumentException,
						ToString());
				}

				return new ConstraintResult.Failure(ToString(),
					$"{it} had ParamName {Formatter.Format(argumentException.ParamName)}");
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {actual.GetType().Name.PrependAOrAn()}");
		}

		public override string ToString()
			=> $"{verb} ParamName {Formatter.Format(expected)}";
	}
}
