using System;
using System.Linq.Expressions;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies the <paramref name="expectations" /> on the member selected by the <paramref name="memberSelector" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> Which<TMember>(
		Expression<Func<TException, TMember?>> memberSelector,
		Action<IExpectSubject<TMember?>> expectations)
		=> new(ExpectationBuilder.ForMember(
					MemberAccessor<TException, TMember?>.FromExpression(memberSelector),
					(member, expectation) => $"which {member}should {expectation}")
				.AddExpectations(e => expectations(new That.Subject<TMember?>(e))),
			this);
}
