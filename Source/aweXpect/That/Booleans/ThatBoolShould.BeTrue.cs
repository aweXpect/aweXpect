using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatBoolShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="true" />.
	/// </summary>
	public static AndOrResult<bool, IThatShould<bool>> BeTrue(this IThatShould<bool> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, true)),
			source);
}
