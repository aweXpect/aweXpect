using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Delegates;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDelegateThrows
{
	/// <summary>
	///     Verifies that the actual exception recursively has inner exceptions which satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	/// <remarks>
	///     Recursively applies the expectations on the <see cref="Exception.InnerException" /> (if not <see langword="null" />
	///     and for <see cref="AggregateException" /> also on the <see cref="AggregateException.InnerExceptions" />.
	/// </remarks>
	public static AndOrResult<TException?, ThatDelegateThrows<TException>> WithRecursiveInnerExceptions<TException>(
		this ThatDelegateThrows<TException> source,
		Action<IThat<IEnumerable<Exception>>> expectations)
		where TException : Exception?
		=> new(source.ExpectationBuilder
				.ForMember(
					MemberAccessor<Exception?, IEnumerable<Exception>>.FromFunc(
						e => e.GetInnerExpectations(), "recursive inner exceptions "),
					(property, expectation) => $"with {property}where {expectation}")
				.AddExpectations(e
					=> expectations(new ThatSubject<IEnumerable<Exception>>(e))),
			source);
}
