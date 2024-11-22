using System;
using Testably.Expectations.Core.Sources;

namespace Testably.Expectations;

public static partial class ThatDelegateShould
{
	/// <summary>
	///     Verifies that the delegate throws an exception of type <typeparamref name="TException" />.
	/// </summary>
	public static ThatDelegateThrows<TException> Throw<TException>(this ThatDelegate source)
		where TException : Exception
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<TException>(source.ExpectationBuilder
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint(_ => new ThrowExceptionOfTypeConstraint<TException>(throwOptions))
				.And(" "),
			throwOptions);
	}

	/// <summary>
	///     Verifies that the delegate throws an exception of type <paramref name="exceptionType" />.
	/// </summary>
	public static ThatDelegateThrows<Exception> Throw(this ThatDelegate source,
		Type exceptionType)
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<Exception>(source.ExpectationBuilder
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint(_ => new ThrowsCastConstraint(exceptionType, throwOptions))
				.And(" "),
			throwOptions);
	}
}
