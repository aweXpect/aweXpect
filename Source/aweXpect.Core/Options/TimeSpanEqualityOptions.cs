﻿using System;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

/// <summary>
///     Equality options for <see langword="TimeSpan" />s.
/// </summary>
public class TimeSpanEqualityOptions
{
	private Limit? _limit;

	/// <summary>
	///     Verifies if the <paramref name="actual" /> value is within the required limit.
	/// </summary>
	/// <param name="actual"></param>
	/// <returns></returns>
	public bool IsWithinLimit(TimeSpan? actual)
	{
		if (actual is null || _limit is null)
		{
			return false;
		}

		return _limit.IsWithinLimit(actual.Value);
	}

	/// <summary>
	///     Appends the failure result text of the <paramref name="actual" /> value to the <paramref name="stringBuilder" />.
	/// </summary>
	public void AppendFailureResult(StringBuilder stringBuilder, TimeSpan actual)
		=> _limit?.AppendFailureResult(stringBuilder, actual);

	/// <summary>
	///     Appends the <paramref name="prefix" /> and the option description to the <paramref name="stringBuilder" />.
	/// </summary>
	public void AppendTo(StringBuilder stringBuilder, string prefix)
		=> _limit?.AppendTo(stringBuilder, prefix);

	/// <summary>
	///     Verifies that the value is within the given <paramref name="duration" />.
	/// </summary>
	public void Within(TimeSpan duration)
		=> _limit = new MaximumLimit(duration, true);

	/// <summary>
	///     Verifies that the value is at most <paramref name="maximum" />.
	/// </summary>
	public void AtMost(TimeSpan maximum)
		=> _limit = new MaximumLimit(maximum);

	/// <summary>
	///     Verifies that the value is at least <paramref name="minimum" />.
	/// </summary>
	public void AtLeast(TimeSpan minimum)
		=> _limit = new MinimumLimit(minimum);

	/// <summary>
	///     Verifies that the value is approximately <paramref name="expected" />,
	///     using the provided <paramref name="tolerance" />.
	/// </summary>
	public void Approximately(TimeSpan expected, TimeSpan tolerance)
	{
		if (tolerance < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException(nameof(tolerance), tolerance, "The tolerance must not be negative.")
				.LogTrace();
		}

		_limit = new ApproximatelyLimit(expected, tolerance);
	}

	/// <summary>
	///     Verifies that the value is between <paramref name="minimum" /> and <paramref name="maximum" />.
	/// </summary>
	public void Between(TimeSpan minimum, TimeSpan maximum)
		=> _limit = new BetweenLimit(minimum, maximum);

	private abstract record Limit
	{
		public abstract bool IsWithinLimit(TimeSpan actual);

		public virtual void AppendFailureResult(StringBuilder stringBuilder, TimeSpan actual)
			=> Formatter.Format(stringBuilder, actual);

		public abstract void AppendTo(StringBuilder stringBuilder, string prefix);
	}

	private sealed record ApproximatelyLimit(TimeSpan Expected, TimeSpan Tolerance) : Limit
	{
		public override bool IsWithinLimit(TimeSpan actual)
			=> actual >= Expected - Tolerance && actual <= Expected + Tolerance;

		public override void AppendTo(StringBuilder stringBuilder, string prefix)
		{
			stringBuilder.Append(prefix);
			stringBuilder.Append("approximately ");
			Formatter.Format(stringBuilder, Expected);
			stringBuilder.Append(" ± ");
			Formatter.Format(stringBuilder, Tolerance);
		}

		public override void AppendFailureResult(StringBuilder stringBuilder, TimeSpan actual)
		{
			if (actual < Expected)
			{
				stringBuilder.Append("only ");
			}

			Formatter.Format(stringBuilder, actual);
		}
	}

	private sealed record BetweenLimit(TimeSpan Minimum, TimeSpan Maximum) : Limit
	{
		public override bool IsWithinLimit(TimeSpan actual)
			=> actual >= Minimum && actual <= Maximum;

		public override void AppendTo(StringBuilder stringBuilder, string prefix)
		{
			stringBuilder.Append(prefix);
			stringBuilder.Append("between ");
			Formatter.Format(stringBuilder, Minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, Maximum);
		}

		public override void AppendFailureResult(StringBuilder stringBuilder, TimeSpan actual)
		{
			if (actual < Minimum)
			{
				stringBuilder.Append("only ");
			}

			Formatter.Format(stringBuilder, actual);
		}
	}

	private sealed record MinimumLimit(TimeSpan Minimum) : Limit
	{
		public override bool IsWithinLimit(TimeSpan actual)
			=> actual >= Minimum;

		public override void AppendTo(StringBuilder stringBuilder, string prefix)
		{
			stringBuilder.Append(prefix);
			stringBuilder.Append("at least ");
			Formatter.Format(stringBuilder, Minimum);
		}

		public override void AppendFailureResult(StringBuilder stringBuilder, TimeSpan actual)
		{
			stringBuilder.Append("only ");
			Formatter.Format(stringBuilder, actual);
		}
	}

	private sealed record MaximumLimit(TimeSpan Maximum, bool IsWithin = false) : Limit
	{
		public override bool IsWithinLimit(TimeSpan actual)
			=> actual <= Maximum;

		public override void AppendTo(StringBuilder stringBuilder, string prefix)
		{
			if (IsWithin)
			{
				stringBuilder.Append("within ");
				Formatter.Format(stringBuilder, Maximum);
			}
			else
			{
				stringBuilder.Append(prefix);
				stringBuilder.Append("at most ");
				Formatter.Format(stringBuilder, Maximum);
			}
		}
	}
}
