using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatBool
{
	/// <summary>
	///     Verifies that the subject implies the <paramref name="consequent" /> value.
	/// </summary>
	[Obsolete("TODO")]
	public static AndOrResult<bool, IThatShould<bool>> Imply(this IThatShould<bool> source,
		bool consequent)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ImplyConstraint(it, consequent)),
			source);
	
	/// <summary>
	///     Verifies that the subject implies the <paramref name="consequent" /> value.
	/// </summary>
	public static AndOrResult<bool, IExpectSubject<bool>> Implies(this IExpectSubject<bool> source,
		bool consequent)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new ImplyConstraint(it, consequent)),
			source);

	private readonly struct ImplyConstraint(string it, bool consequent) : IValueConstraint<bool>
	{
		public ConstraintResult IsMetBy(bool actual)
		{
			if (!actual || consequent)
			{
				return new ConstraintResult.Success<bool>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} did not");
		}

		public override string ToString()
			=> $"imply {Formatter.Format(consequent)}";
	}
}
