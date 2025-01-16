using System;
using aweXpect.Core;
using aweXpect.Core.Sources;

namespace aweXpect;

public static partial class ThatDelegate
{
	/// <summary>
	///     Verifies that the delegate throws an exception.
	/// </summary>
	public static ThatDelegateThrows<Exception> ThrowException(this Core.ThatDelegate source)
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<Exception>(source.ExpectationBuilder
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint(_ => new ThrowExceptionOfTypeConstraint<Exception>(throwOptions))
				.And(" "),
			throwOptions);
	}
}
