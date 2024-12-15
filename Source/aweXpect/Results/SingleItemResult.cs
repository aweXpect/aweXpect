﻿using System;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Results;

/// <summary>
///     An <see cref="ExpectationResult" /> for a single item from a collection.
/// </summary>
public class SingleItemResult<TCollection, TItem>
	: ExpectationResult<TItem, SingleItemResult<TCollection, TItem>>
{
	private readonly ExpectationBuilder _expectationBuilder;
	private readonly Func<TCollection, TItem?> _memberAccessor;

	internal SingleItemResult(ExpectationBuilder expectationBuilder, Func<TCollection, TItem?> memberAccessor)
		: base(expectationBuilder)
	{
		_expectationBuilder = expectationBuilder;
		_memberAccessor = memberAccessor;
	}

	/// <summary>
	///     Further expectations on the single <typeparamref name="TItem" />
	/// </summary>
	public IExpectSubject<TItem> Which
		=> new That.Subject<TItem>(_expectationBuilder.ForWhich(_memberAccessor, " which should "));

	/// <summary>
	///     An <see cref="ExpectationResult" /> for a single item from an asynchronous collection.
	/// </summary>
	public class Async : ExpectationResult<TItem, Async>
	{
		private readonly Func<TCollection, Task<TItem?>> _asyncMemberAccessor;
		private readonly ExpectationBuilder _expectationBuilder;

		internal Async(ExpectationBuilder expectationBuilder, Func<TCollection, Task<TItem?>> asyncMemberAccessor)
			: base(expectationBuilder)
		{
			_expectationBuilder = expectationBuilder;
			_asyncMemberAccessor = asyncMemberAccessor;
		}

		/// <summary>
		///     Further expectations on the single item.
		/// </summary>
		public IExpectSubject<TItem> Which
			=> new That.Subject<TItem>(_expectationBuilder.ForWhich(_asyncMemberAccessor, " which should "));
	}
}