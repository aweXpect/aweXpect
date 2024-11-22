using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableBoolShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> BeNull(this IThat<bool?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, null)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> NotBeNull(this IThat<bool?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, null)),
			source);
}
