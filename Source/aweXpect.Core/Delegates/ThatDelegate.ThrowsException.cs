using System;
using aweXpect.Core.Sources;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	/// <summary>
	///     Verifies that the delegate throws an exception.
	/// </summary>
	public ThatDelegateThrows<Exception> ThrowsException()
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<Exception>(ExpectationBuilder
				.AddConstraint((_,_) => new DelegateIsNotNullConstraint())
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint((_,_) => new ThrowExceptionOfTypeConstraint<Exception>(throwOptions))
				.And(" "),
			throwOptions);
	}
}
