using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect.Delegates;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies the <paramref name="expectations" /> on the member selected by the <paramref name="memberSelector" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> Whose<TMember>(
		Func<TException, TMember?> memberSelector,
		Action<IThat<TMember?>> expectations,
		[CallerArgumentExpression("memberSelector")]
		string doNotPopulateThisValue = "")
		=> new(ExpectationBuilder.ForMember(
					MemberAccessor<TException, TMember?>.FromFuncAsMemberAccessor(memberSelector,
						doNotPopulateThisValue),
					(member, expectation) => expectation.Append("whose ").Append(member))
				.AddExpectations(e => expectations(new ThatSubject<TMember?>(e))),
			this);
}
