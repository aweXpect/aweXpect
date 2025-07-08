namespace aweXpect.Delegates;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies, that the exception was thrown only if the <paramref name="predicate" /> is <see langword="true" />,
	///     otherwise it verifies, that no exception was thrown.
	/// </summary>
	public ThatDelegateThrows<TException?> OnlyIf(bool predicate)
	{
		ThrowOptions.DoCheckThrow = predicate;
		return new ThatDelegateThrows<TException?>(ExpectationBuilder, ThrowOptions);
	}
}
