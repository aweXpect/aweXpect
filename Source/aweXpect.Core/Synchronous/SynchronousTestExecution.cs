﻿using aweXpect.Results;

namespace aweXpect.Synchronous;

/// <summary>
///     Extension methods to support synchronous execution.
/// </summary>
/// <remarks>
///     <b>WARNING!</b><br />
///     The only intended use case is to support synchronous evaluation for <c>ref struct</c>.
/// </remarks>
public static class SynchronousTestExecution
{
	/// <summary>
	///     Verifies synchronously that the expectation is satisfied.
	/// </summary>
	/// <remarks>
	///     <b>WARNING!</b><br />
	///     The only intended use case is to support synchronous evaluation for <c>ref struct</c>.
	/// </remarks>
	public static void Verify(this ExpectationResult result)
		=> result.GetAwaiter().GetResult();

	/// <summary>
	///     Verifies synchronously that the expectation is satisfied.
	/// </summary>
	/// <remarks>
	///     <b>WARNING!</b><br />
	///     The only intended use case is to support synchronous evaluation for <c>ref struct</c>.
	/// </remarks>
	public static TType Verify<TType, TSelf>(this ExpectationResult<TType, TSelf> result)
		where TSelf : ExpectationResult<TType, TSelf>
		=> result.GetAwaiter().GetResult();
}
