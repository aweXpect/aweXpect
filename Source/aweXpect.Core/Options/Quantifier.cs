using System;
using aweXpect.Core.Helpers;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace aweXpect.Options;

/// <summary>
///     Quantifier an occurrence.
/// </summary>
public class Quantifier
{
	private bool _isNegated;
	private int? _maximum;
	private int? _minimum = 1;

	/// <summary>
	///     A quantifier for exactly zero items.
	/// </summary>
	public static Quantifier Never()
	{
		Quantifier quantifier = new();
		quantifier.Exactly(0);
		return quantifier;
	}

	/// <summary>
	///     Verifies, that it occurs at least <paramref name="minimum" /> times.
	/// </summary>
	public void AtLeast(int minimum)
	{
		if (minimum < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(minimum),
					"The parameter 'minimum' must be non-negative")
				.LogTrace();
		}

		_minimum = minimum;
		_maximum = null;
	}

	/// <summary>
	///     Verifies, that it occurs at most <paramref name="maximum" /> times.
	/// </summary>
	public void AtMost(int maximum)
	{
		if (maximum < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(maximum),
					"The parameter 'maximum' must be non-negative")
				.LogTrace();
		}

		_minimum = null;
		_maximum = maximum;
	}

	/// <summary>
	///     Verifies, that it occurs between <paramref name="minimum" /> and <paramref name="maximum" /> times.
	/// </summary>
	public void Between(int minimum, int maximum)
	{
		if (minimum < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(minimum),
					"The parameter 'minimum' must be non-negative")
				.LogTrace();
		}

		if (maximum < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(maximum),
					"The parameter 'maximum' must be non-negative")
				.LogTrace();
		}

		if (minimum > maximum)
		{
			throw new ArgumentException("The parameter 'maximum' must be greater than or equal to 'minimum'")
				.LogTrace();
		}

		_minimum = minimum;
		_maximum = maximum;
	}

	/// <summary>
	///     Verifies the amount against the conditions.
	/// </summary>
	/// <remarks>
	///     Returns <see langword="true" /> when the condition is satisfied,
	///     <see langword="false" /> when the condition is not satisfied
	///     and <see langword="null" /> when the condition could still be satisfied
	///     with a larger <paramref name="amount" />.
	/// </remarks>
	public bool? Check(int amount, bool isLast)
	{
		if (_maximum != null && amount > _maximum)
		{
			return _isNegated;
		}

		if ((isLast || _maximum == null) &&
		    (_minimum == null || amount >= _minimum))
		{
			return !_isNegated;
		}

		return null;
	}

	/// <summary>
	///     Verifies, that it occurs exactly <paramref name="expected" /> times.
	/// </summary>
	public void Exactly(int expected)
	{
		if (expected < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(expected),
					"The parameter 'expected' must be non-negative")
				.LogTrace();
		}

		_minimum = expected;
		_maximum = expected;
	}

	/// <inheritdoc />
	public override string ToString()
	{
		if (_isNegated)
		{
			return NegatedToString();
		}

		string? specialCases = (_minimum, _maximum) switch
		{
			(1, null) => "at least once",
			(_, 0) => "never",
			(1, 1) => "exactly once",
			(null, 1) => "at most once",
			(_, _) => null,
		};
		if (specialCases != null)
		{
			return specialCases;
		}

		if (_minimum == _maximum)
		{
			return $"exactly {_minimum} times";
		}

		if (_maximum == null)
		{
			return $"at least {_minimum} times";
		}

		if (_minimum == null)
		{
			return $"at most {_maximum} times";
		}

		return $"between {_minimum} and {_maximum} times";
	}

	private string NegatedToString()
	{
		string? specialCases = (_minimum, _maximum) switch
		{
			(1, null) => "never",
			(_, 0) => "at least once",
			(1, 1) => "not once",
			(null, 1) => "more than once",
			(_, _) => null,
		};
		if (specialCases != null)
		{
			return specialCases;
		}

		if (_minimum == _maximum)
		{
			return $"not exactly {_minimum} times";
		}

		if (_maximum == null)
		{
			return $"at most {_minimum - 1} times";
		}

		if (_minimum == null)
		{
			return $"at least {_maximum + 1} times";
		}

		return $"outside {_minimum} and {_maximum} times";
	}

	/// <summary>
	///     Negates the quantifier.
	/// </summary>
	public void Negate()
		=> _isNegated = !_isNegated;
}
