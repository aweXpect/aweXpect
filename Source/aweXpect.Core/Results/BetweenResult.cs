﻿using System;

namespace aweXpect.Results;

/// <summary>
///     An intermediate type to collect the maximum of the range.
/// </summary>
public class BetweenResult<TTarget, TExpectation>(
	Func<int, Action<TExpectation>, TTarget> callback)
{
	/// <summary>
	///     ... and <paramref name="maximum" /> items satisfy the <paramref name="expectations" />.
	/// </summary>
	public TTarget And(int maximum,
		Action<TExpectation> expectations)
		=> callback(maximum, expectations);
}

/// <summary>
///     An intermediate type to collect the maximum of the range.
/// </summary>
public class BetweenResult<TTarget>(
	Func<int, TTarget> callback)
{
	/// <summary>
	///     ... and <paramref name="maximum" /> items.
	/// </summary>
	public TTarget And(Times maximum)
		=> callback(maximum.Value);
}
