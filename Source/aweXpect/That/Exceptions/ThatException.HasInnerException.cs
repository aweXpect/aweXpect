using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public partial class ThatException
{
	/// <summary>
	///     Verifies that the actual exception has an inner exception.
	/// </summary>
	public static AndOrResult<Exception?, IExpectSubject<Exception?>> HasInnerException(
		this IExpectSubject<Exception?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new HasInnerExceptionValueConstraint<Exception?>("have",
						it)),
			source);

	/// <summary>
	///     Verifies that the actual exception has an inner exception which satisfies the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<Exception?, IExpectSubject<Exception?>> HasInnerException(
		this IExpectSubject<Exception?> source,
		Action<IExpectSubject<Exception?>> expectations)
		=> new(source.ThatIs().ExpectationBuilder
				.ForMember<Exception, Exception?>(e => e.InnerException,
					"have an inner exception which should ",
					false)
				.Validate(it
					=> new InnerExceptionIsTypeConstraint<Exception>(it))
				.AddExpectations(e => expectations(new That.Subject<Exception?>(e))),
			source);
}
