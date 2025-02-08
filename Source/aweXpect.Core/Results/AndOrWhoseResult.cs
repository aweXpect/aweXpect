using System;
using System.Linq.Expressions;
using aweXpect.Core;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows accessing
///     underlying
///     properties with <see cref="AndOrWhoseResult{TResult,TValue,TSelf}.Whose{TMember}" />.
/// </summary>
public class AndOrWhoseResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue)
	: AndOrWhoseResult<TType, TThat, AndOrWhoseResult<TType, TThat>>(
		expectationBuilder, returnValue);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows accessing
///     underlying members with <see cref="Whose{TMember}" />.
/// </summary>
public class AndOrWhoseResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue)
	: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	where TSelf : AndOrWhoseResult<TType, TThat, TSelf>
{
	private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;
	private readonly TThat _returnValue = returnValue;

	/// <summary>
	///     Allows specifying <paramref name="expectations" /> on the member selected by the
	///     <paramref name="memberSelector" />.
	/// </summary>
	public AdditionalAndOrWhoseResult
		Whose<TMember>(
			Expression<Func<TType, TMember?>> memberSelector,
			Action<IThat<TMember?>> expectations)
		=> new(
			_expectationBuilder
				.ForMember(MemberAccessor<TType, TMember?>.FromExpression(memberSelector),
					(member, expectation) => $" whose {member}{expectation}")
				.AddExpectations(e => expectations(new ThatSubject<TMember?>(e))),
			_returnValue);

	/// <summary>
	///     The result of an additional expectation for the underlying type.
	///     <para />
	///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows accessing
	///     underlying members with <see cref="AndWhose{TMember}" />.
	/// </summary>
	public class AdditionalAndOrWhoseResult(
		ExpectationBuilder expectationBuilder,
		TThat returnValue)
		: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	{
		private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;
		private readonly TThat _returnValue = returnValue;

		/// <summary>
		///     Allows specifying <paramref name="expectations" /> on the member selected by the
		///     <paramref name="memberSelector" />.
		/// </summary>
		public AdditionalAndOrWhoseResult
			AndWhose<TMember>(
				Expression<Func<TType, TMember?>> memberSelector,
				Action<IThat<TMember?>> expectations)
		{
			_expectationBuilder.And(" and");
			return new AdditionalAndOrWhoseResult(
				_expectationBuilder
					.ForMember(
						MemberAccessor<TType, TMember?>.FromExpression(memberSelector),
						(member, expectation) => $" whose {member}{expectation}")
					.AddExpectations(e
						=> expectations(new ThatSubject<TMember?>(e))),
				_returnValue);
		}
	}
}
