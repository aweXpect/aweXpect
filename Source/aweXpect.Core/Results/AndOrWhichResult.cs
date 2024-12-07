using System;
using System.Linq.Expressions;
using aweXpect.Core;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows accessing
///     underlying
///     properties with <see cref="AndOrWhichResult{TResult,TValue,TSelf}.Which{TMember}" />.
/// </summary>
public class AndOrWhichResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue)
	: AndOrWhichResult<TType, TThat, AndOrWhichResult<TType, TThat>>(
		expectationBuilder, returnValue);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows accessing
///     underlying members with <see cref="Which{TMember}" />.
/// </summary>
public class AndOrWhichResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue)
	: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	where TSelf : AndOrWhichResult<TType, TThat, TSelf>
{
	private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;
	private readonly TThat _returnValue = returnValue;

	/// <summary>
	///     Allows specifying <paramref name="expectations"/> on the member selected by the <paramref name="memberSelector" />.
	/// </summary>
	public AdditionalAndOrWhichResult
		Which<TMember>(
			Expression<Func<TType, TMember?>> memberSelector,
			Action<IExpectSubject<TMember?>> expectations)
	{
		return new AdditionalAndOrWhichResult(
			_expectationBuilder
				.ForMember(MemberAccessor<TType, TMember?>.FromExpression(memberSelector),
					(member, expectation) => $" which {member}should {expectation}")
				.AddExpectations(e => expectations(new Expect.ThatSubject<TMember?>(e))),
			_returnValue);
	}

	/// <summary>
	///     The result of an additional expectation for the underlying type.
	///     <para />
	///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows accessing
	///     underlying members with <see cref="AndWhich{TMember}" />.
	/// </summary>
	public class AdditionalAndOrWhichResult(
		ExpectationBuilder expectationBuilder,
		TThat returnValue)
		: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	{
		private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;
		private readonly TThat _returnValue = returnValue;

		/// <summary>
		///     Allows specifying <paramref name="expectations"/> on the member selected by the <paramref name="memberSelector" />.
		/// </summary>
		public AdditionalAndOrWhichResult
			AndWhich<TMember>(
				Expression<Func<TType, TMember?>> memberSelector,
				Action<IExpectSubject<TMember?>> expectations)
		{
			_expectationBuilder.And(" and");
			return new AdditionalAndOrWhichResult(
				_expectationBuilder
					.ForMember(
						MemberAccessor<TType, TMember?>.FromExpression(memberSelector),
						(member, expectation) => $" which {member}should {expectation}")
					.AddExpectations(e
						=> expectations(new Expect.ThatSubject<TMember?>(e))),
				_returnValue);
		}
	}
}
