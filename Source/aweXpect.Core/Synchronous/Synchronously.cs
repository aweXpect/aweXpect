﻿using aweXpect.Results;

namespace aweXpect.Synchronous;

/// <summary>
///     Methods to support synchronous execution.
/// </summary>
/// <remarks>
///     <b>WARNING!</b><br />
///     The only intended use case is to support synchronous evaluation for <c>ref struct</c>.
/// </remarks>
public static class Synchronously
{
	/// <summary>
	///     Verifies synchronously that the expectation is satisfied.
	/// </summary>
	/// <remarks>
	///     <b>WARNING!</b><br />
	///     The only intended use case is to support synchronous evaluation for <c>ref struct</c>.
	/// </remarks>
	public static void Verify(ExpectationResult result)
		=> result.GetAwaiter().GetResult();

	/// <summary>
	///     Verifies synchronously that the expectation is satisfied.
	/// </summary>
	/// <remarks>
	///     <b>WARNING!</b><br />
	///     The only intended use case is to support synchronous evaluation for <c>ref struct</c>.
	/// </remarks>
	public static TType Verify<TType, TSelf>(ExpectationResult<TType, TSelf> result)
		where TSelf : ExpectationResult<TType, TSelf>
		=> result.GetAwaiter().GetResult();
}
