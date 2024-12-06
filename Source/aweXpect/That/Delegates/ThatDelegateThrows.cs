﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     An <see cref="ExpectationResult" /> when an exception was thrown.
/// </summary>
public partial class ThatDelegateThrows<TException>
	: ExpectationResult<TException, ThatDelegateThrows<TException>>
	where TException : Exception?
{
	/// <summary>
	///     The expectation builder.
	/// </summary>
	public ExpectationBuilder ExpectationBuilder { get; }

	private readonly ThrowsOption _throwOptions;

	internal ThatDelegateThrows(ExpectationBuilder expectationBuilder,
		ThrowsOption throwOptions)
		: base(expectationBuilder)
	{
		ExpectationBuilder = expectationBuilder;
		_throwOptions = throwOptions;
	}
}