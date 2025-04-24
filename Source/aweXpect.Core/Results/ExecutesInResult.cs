﻿using System;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an execution time expectation with no underlying value.
/// </summary>
public class ExecutesInResult<TResult>(
	TResult returnValue,
	TimeSpanEqualityOptions options)
{
	/// <summary>
	///     …at most <paramref name="maximum" /> time.
	/// </summary>
	public TResult AtMost(TimeSpan maximum)
	{
		options.AtMost(maximum);
		return returnValue;
	}

	/// <summary>
	///     …at least <paramref name="minimum" /> time.
	/// </summary>
	public TResult AtLeast(TimeSpan minimum)
	{
		options.AtLeast(minimum);
		return returnValue;
	}

	/// <summary>
	///     …approximately the <paramref name="expected" /> time using the provided <paramref name="tolerance" />.
	/// </summary>
	public TResult Approximately(TimeSpan expected, TimeSpan tolerance)
	{
		options.Approximately(expected, tolerance);
		return returnValue;
	}

	/// <summary>
	///     …between <paramref name="minimum" />…
	/// </summary>
	public BetweenResult Between(TimeSpan minimum) => new(maximum =>
	{
		options.Between(minimum, maximum);
		return returnValue;
	});

	/// <summary>
	///     An intermediate type to collect the maximum of the time range.
	/// </summary>
	public class BetweenResult(
		Func<TimeSpan, TResult> callback)
	{
		/// <summary>
		///     …and <paramref name="maximum" /> time.
		/// </summary>
		public TResult And(TimeSpan maximum)
			=> callback(maximum);
	}
}
