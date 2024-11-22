using System;
using Testably.Expectations.Core.Sources;

namespace Testably.Expectations;

public static partial class ThatDelegateShould
{
	/// <summary>
	///     Verifies that the delegate throws an exception.
	/// </summary>
	public static ThatDelegateThrows<Exception> ThrowException(this ThatDelegate source)
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<Exception>(source.ExpectationBuilder
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint(_ => new ThrowExceptionOfTypeConstraint<Exception>(throwOptions))
				.And(" "),
			throwOptions);
	}
}
