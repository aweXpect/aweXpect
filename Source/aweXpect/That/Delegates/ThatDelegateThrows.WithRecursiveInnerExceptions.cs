using System;
using System.Collections.Generic;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Helpers;
using Testably.Expectations.Results;

namespace Testably.Expectations;

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
			Action<IThat<IEnumerable<Exception>>> expectations)
		=> new(ExpectationBuilder
				.ForProperty(
					PropertyAccessor<Exception?, IEnumerable<Exception>>.FromFunc(
						e => e.GetInnerExpectations(), "recursive inner exceptions "),
					(property, expectation) => $"with {property}which {expectation}")
				.AddExpectations(e
					=> expectations(new Expect.ThatSubject<IEnumerable<Exception>>(e))),
			this);
}
