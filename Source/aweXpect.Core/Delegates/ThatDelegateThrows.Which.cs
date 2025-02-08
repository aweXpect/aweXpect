using System;
using System.Linq.Expressions;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect.Delegates;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies the <paramref name="expectations" /> on the member selected by the <paramref name="memberSelector" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> Whose<TMember>(
		Expression<Func<TException, TMember?>> memberSelector,
		Action<IThat<TMember?>> expectations)
		=> new(ExpectationBuilder.ForMember(
					MemberAccessor<TException, TMember?>.FromExpression(memberSelector),
					(member, expectation) => $"whose {member}{expectation}")
				.AddExpectations(e => expectations(new ThatSubject<TMember?>(e))),
			this);

}
