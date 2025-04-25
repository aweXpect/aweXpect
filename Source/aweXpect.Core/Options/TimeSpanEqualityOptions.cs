using System;
using System.Text;

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

	/// <inheritdoc />
	public override string ToString() => _limit?.ToString() ?? "<no limit specified>";

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
			throw new ArgumentOutOfRangeException(nameof(tolerance), tolerance, "The tolerance must not be negative.");
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
	}

	private sealed record ApproximatelyLimit(TimeSpan Expected, TimeSpan Tolerance) : Limit
	{
		public override bool IsWithinLimit(TimeSpan actual)
			=> actual >= Expected - Tolerance && actual <= Expected + Tolerance;

		public override string ToString()
			=> $"approximately {Formatter.Format(Expected)} ± {Formatter.Format(Tolerance)}";

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

		public override string ToString() => $"between {Formatter.Format(Minimum)} and {Formatter.Format(Maximum)}";

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

		public override string ToString() => $"at least {Formatter.Format(Minimum)}";

		public override void AppendFailureResult(StringBuilder stringBuilder, TimeSpan actual)
		{
			stringBuilder.Append("only ");
			Formatter.Format(stringBuilder, actual);
		}
	}

	private sealed record MaximumLimit(TimeSpan Maximum) : Limit
	{
		public override bool IsWithinLimit(TimeSpan actual)
			=> actual <= Maximum;

		public override string ToString() => $"at most {Formatter.Format(Maximum)}";
	}
}
