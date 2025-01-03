using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the actual exception recursively has inner exceptions which satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	/// <remarks>
	///     Recursively applies the expectations on the <see cref="Exception.InnerException" /> (if not <see langword="null" />
	///     and for <see cref="AggregateException" /> also on the <see cref="AggregateException.InnerExceptions" />.
	/// </remarks>
	public AndOrResult<TException?, ThatDelegateThrows<TException>>
		WithRecursiveInnerExceptions(
			Action<IExpectSubject<IEnumerable<Exception>>> expectations)
		=> new(ExpectationBuilder
				.ForMember(
					MemberAccessor<Exception?, IEnumerable<Exception>>.FromFunc(
						e => e.GetInnerExpectations(), "recursive inner exceptions "),
					(property, expectation) => $"with {property}which should {expectation}")
				.AddExpectations(e
					=> expectations(new That.Subject<IEnumerable<Exception>>(e))),
			this);
}
