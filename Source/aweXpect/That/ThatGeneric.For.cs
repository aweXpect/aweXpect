using System;
using System.Linq.Expressions;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGeneric
{
	/// <summary>
	///     Verifies the <paramref name="expectations" /> on the member selected by the <paramref name="memberSelector" />.
	/// </summary>
	public static AndOrResult<T, IThat<T>> For<T, TMember>(
		this IThat<T> source,
		Expression<Func<T, TMember?>> memberSelector,
		Action<IThat<TMember?>> expectations)
	{
		ExpectationBuilder expectationBuilder = source.ThatIs().ExpectationBuilder;
		expectationBuilder
			.ForMember(
				MemberAccessor<T, TMember?>.FromExpression(memberSelector),
				(member, expectation) => $"for {member}{expectation}")
			.AddExpectations(e => expectations(new ThatSubject<TMember?>(e)));
		return new AndOrResult<T, IThat<T>>(expectationBuilder, source);
	}
}
