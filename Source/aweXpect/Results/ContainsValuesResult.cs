﻿using System;
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect.Results;

/// <summary>
///     The result on a set of dictionary values.
/// </summary>
/// <remarks>
///     <seealso cref="AndOrResult{TCollection, TThat}" />
/// </remarks>
public class ContainsValuesResult<TCollection, TThat, TKey, TValue>
	: AndOrResult<TCollection, TThat>
{
	private readonly ExpectationBuilder _expectationBuilder;
	private readonly TKey[] _keys;
	private readonly Func<TCollection, IEnumerable<TValue>> _memberAccessor;

	internal ContainsValuesResult(
		ExpectationBuilder expectationBuilder,
		TThat returnValue,
		TKey[] keys,
		Func<TCollection, IEnumerable<TValue>> memberAccessor)
		: base(expectationBuilder, returnValue)
	{
		_expectationBuilder = expectationBuilder;
		_keys = keys;
		_memberAccessor = memberAccessor;
	}

	/// <summary>
	///     Further expectations on the selected values of the dictionary.
	/// </summary>
	public ThatEnumerable.Elements<TValue> WhoseValues
		=> new(
			new ThatSubject<IEnumerable<TValue>>(_expectationBuilder
				.ForWhich(_memberAccessor, " whose values should ", $"values {Formatter.Format(_keys)}")),
			EnumerableQuantifier.All);
}
