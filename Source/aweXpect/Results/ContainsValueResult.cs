﻿using System;
using aweXpect.Core;

namespace aweXpect.Results;

/// <summary>
///     The result on a single dictionary value.
/// </summary>
/// <remarks>
///     <seealso cref="AndOrResult{TCollection, TThat}" />
/// </remarks>
public class ContainsValueResult<TCollection, TThat, TValue>
	: AndOrResult<TCollection, TThat>
{
	private readonly ExpectationBuilder _expectationBuilder;
	private readonly Func<TCollection, TValue> _memberAccessor;

	internal ContainsValueResult(ExpectationBuilder expectationBuilder, TThat returnValue,
		Func<TCollection, TValue> memberAccessor)
		: base(expectationBuilder, returnValue)
	{
		_expectationBuilder = expectationBuilder;
		_memberAccessor = memberAccessor;
	}

	/// <summary>
	///     Further expectations on the selected value of the dictionary.
	/// </summary>
	#if DEBUG // TODO: Replace after Core update
	public IThat<TValue> WhoseValue
		=> new ThatSubject<TValue>(_expectationBuilder.ForWhich(_memberAccessor, " whose value should ", "the value"));
	#else
	public IThat<TValue> WhoseValue
		=> new ThatSubject<TValue>(_expectationBuilder.ForWhich(_memberAccessor, " whose value should "));
	#endif
}
