﻿using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <paramref name="quantifier" />.
/// </summary>
public class CountResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	Quantifier quantifier)
	: CountResult<TType, TThat,
		CountResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		quantifier);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <paramref name="quantifier" />.
/// </summary>
public class CountResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	Quantifier quantifier)
	: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	where TSelf : CountResult<TType, TThat, TSelf>
{
	/// <summary>
	///     Verifies, that it occurs at least <paramref name="minimum" /> times.
	/// </summary>
	public TSelf AtLeast(Times minimum)
	{
		quantifier.AtLeast(minimum.Value);
		return (TSelf)this;
	}

	/// <summary>
	///     Verifies, that it occurs at most <paramref name="maximum" /> times.
	/// </summary>
	public TSelf AtMost(Times maximum)
	{
		quantifier.AtMost(maximum.Value);
		return (TSelf)this;
	}

	/// <summary>
	///     Verifies, that it occurs between <paramref name="minimum" />...
	/// </summary>
	public BetweenResult<TSelf> Between(int minimum)
		=> new(maximum =>
		{
			quantifier.Between(minimum, maximum);
			return (TSelf)this;
		});

	/// <summary>
	///     Verifies, that it occurs exactly <paramref name="expected" /> times.
	/// </summary>
	public TSelf Exactly(Times expected)
	{
		quantifier.Exactly(expected.Value);
		return (TSelf)this;
	}

	/// <summary>
	///     Verifies, that it occurs never.
	/// </summary>
	public TSelf Never()
	{
		quantifier.Exactly(0);
		return (TSelf)this;
	}

	/// <summary>
	///     Verifies, that it occurs exactly once.
	/// </summary>
	public TSelf Once()
	{
		quantifier.Exactly(1);
		return (TSelf)this;
	}
}
