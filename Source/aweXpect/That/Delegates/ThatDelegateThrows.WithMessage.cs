using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the thrown exception has a message equal to <paramref name="expected" />
	/// </summary>
	public StringEqualityTypeResult<TException, ThatDelegateThrows<TException>> WithMessage(string expected)
	{
		StringEqualityOptions? options = new();
		return new StringEqualityTypeResult<TException, ThatDelegateThrows<TException>>(ExpectationBuilder.AddConstraint(it
				=> new ThatExceptionShould.HasMessageValueConstraint<TException>(
					it, "with", expected, options)),
			this,
			options);
	}
}
