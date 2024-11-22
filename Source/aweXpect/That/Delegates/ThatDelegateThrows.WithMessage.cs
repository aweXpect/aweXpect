using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the thrown exception has a message equal to <paramref name="expected" />
	/// </summary>
	public StringMatcherResult<TException, ThatDelegateThrows<TException>>
		WithMessage(
			StringMatcher expected)
		=> new(ExpectationBuilder.AddConstraint(it
				=> new ThatExceptionShould.HasMessageValueConstraint<TException>(
					it, "with", expected)),
			this,
			expected);
}
