﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     An <see cref="ExpectationResult" /> when an exception was thrown.
/// </summary>
public static partial class ThatDelegateThrows;

/// <summary>
///     An <see cref="ExpectationResult" /> when an exception was thrown.
/// </summary>
public partial class ThatDelegateThrows<TException>
	: ExpectationResult<TException, ThatDelegateThrows<TException>>
	where TException : Exception?
{
	private readonly ThatDelegate.ThrowsOption _throwOptions;

	internal ThatDelegateThrows(ExpectationBuilder expectationBuilder,
		ThatDelegate.ThrowsOption throwOptions)
		: base(expectationBuilder)
	{
		ExpectationBuilder = expectationBuilder;
		_throwOptions = throwOptions;
	}

	/// <summary>
	///     The expectation builder.
	/// </summary>
	public ExpectationBuilder ExpectationBuilder { get; }
}
