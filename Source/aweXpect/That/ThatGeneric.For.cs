using System;
using System.Linq.Expressions;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatGeneric
{
	/// <summary>
	///     Verifies the <paramref name="expectations" /> on the property selected by the <paramref name="selector" />.
	/// </summary>
	public static AndOrResult<T, IExpectSubject<T>> For<T, TProperty>(
		this IExpectSubject<T> source,
		Expression<Func<T, TProperty?>> selector,
		Action<IExpectSubject<TProperty?>> expectations)
	{
		IThat<T> should = source.Should(expectationBuilder => expectationBuilder
			.ForProperty(
				PropertyAccessor<T, TProperty?>.FromExpression(selector),
				(property, expectation) => $"for {property}{expectation}")
			.AddExpectations(e => expectations(new Expect.ThatSubject<TProperty?>(e))));
		return new AndOrResult<T, IExpectSubject<T>>(should.ExpectationBuilder, source);
	}
}
