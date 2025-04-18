﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using aweXpect.Core.Initialization;

namespace aweXpect;

/// <summary>
///     Methods for explicitly failing the running test.
/// </summary>
[StackTraceHidden]
public static class Fail
{
	/// <summary>
	///     Explicitly fails the current test.
	/// </summary>
	/// <param name="reason">The reason why the test failed</param>
	[DoesNotReturn]
	public static void Test(string reason)
		=> FailIf(true, reason);

	/// <summary>
	///     Explicitly fails the current test when the <paramref name="condition" /> is <c>false</c>.
	/// </summary>
	/// <param name="condition">When <c>false</c>, the test will be failed; otherwise it will continue to run</param>
	/// <param name="reason">The reason why the test was failed</param>
	public static void Unless([DoesNotReturnIf(false)] bool condition, string reason)
		=> FailIf(!condition, reason);

	/// <summary>
	///     Explicitly fails the current test when the <paramref name="condition" /> is <c>true</c>.
	/// </summary>
	/// <param name="condition">When <c>true</c>, the test will be failed; otherwise it will continue to run</param>
	/// <param name="reason">The reason why the test was failed</param>
	public static void When([DoesNotReturnIf(true)] bool condition, string reason)
		=> FailIf(condition, reason);

	/// <summary>
	///     Explicitly fails the current test as inconclusive.
	/// </summary>
	/// <param name="reason">The reason why the test failed</param>
	[DoesNotReturn]
	public static void Inconclusive(string reason)
		=> AweXpectInitialization.State.Value.Inconclusive(reason);

	private static void FailIf([DoesNotReturnIf(true)] bool condition, string reason)
	{
		if (!condition)
		{
			return;
		}

		AweXpectInitialization.State.Value.Fail(reason);
	}
}
