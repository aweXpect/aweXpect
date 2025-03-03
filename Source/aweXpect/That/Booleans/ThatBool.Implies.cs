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
	public static AndOrResult<bool, IThat<bool>> Implies(this IThat<bool> source,
		bool consequent)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new ImpliesConstraint(it, grammars, consequent)),
			source);

	private readonly struct ImpliesConstraint(string it, ExpectationGrammars grammars, bool consequent)
		: IValueConstraint<bool>
	{
		public ConstraintResult IsMetBy(bool actual)
		{
			if (!actual || consequent)
			{
				string? i = it;
				return new ConstraintResult.Success<bool>(actual, ToString(), () => $"{i} did");
			}

			return new ConstraintResult.Failure(ToString(), $"{it} did not");
		}

		public override string ToString()
			=> grammars.HasFlag(ExpectationGrammars.Negated) switch
			{
				false => $"implies {Formatter.Format(consequent)}",
				true => $"does not imply {Formatter.Format(consequent)}",
			};
	}
}
