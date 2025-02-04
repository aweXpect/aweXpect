using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatException
{
	/// <summary>
	///     Verifies that the actual exception recursively has inner exceptions which satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	/// <remarks>
	///     Recursively applies the expectations on the <see cref="Exception.InnerException" /> (if not <see langword="null" />
	///     and for <see cref="AggregateException" /> also on the <see cref="AggregateException.InnerExceptions" />.
	/// </remarks>
	public static AndOrResult<Exception?, IThat<Exception?>> HasRecursiveInnerExceptions(
		this IThat<Exception?> source,
		Action<IThat<IEnumerable<Exception>>> expectations)
		=> new(source.ThatIs().ExpectationBuilder
				.ForMember(MemberAccessor<Exception?, IEnumerable<Exception?>>.FromFunc(
						e => e.GetInnerExpectations(),
						"recursive inner exceptions "),
					(property, expectation) => $"have {property}which should {expectation}",
					false)
				.AddExpectations(e => expectations(
					new ThatSubject<IEnumerable<Exception>>(e))),
			source);
}
