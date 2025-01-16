using System;
using aweXpect.Core;
using aweXpect.Core.Sources;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatDelegate
{
	/// <summary>
	///     Verifies that the delegate throws an exception of type <typeparamref name="TException" />.
	/// </summary>
	public static ThatDelegateThrows<TException> Throw<TException>(this Core.ThatDelegate source)
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
	public static ThatDelegateThrows<Exception> Throw(this Core.ThatDelegate source,
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
