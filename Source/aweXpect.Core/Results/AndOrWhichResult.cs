using System;
using System.Runtime.CompilerServices;
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
	///     Allows specifying <paramref name="expectations" /> on the member selected by the
	///     <paramref name="memberSelector" />.
	/// </summary>
	public AdditionalAndOrWhichResult
		Which<TMember>(
			Func<TType, TMember?> memberSelector,
			Action<IThat<TMember?>> expectations,
			[CallerArgumentExpression("memberSelector")]
			string doNotPopulateThisValue = "")
		=> new(
			_expectationBuilder
				.ForMember(
					MemberAccessor<TType, TMember?>.FromFuncAsMemberAccessor(memberSelector, doNotPopulateThisValue),
					(member, stringBuilder) => stringBuilder.Append(" which ").Append(member))
				.AddExpectations(e => expectations(new ThatSubject<TMember?>(e))),
			_returnValue);

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
		///     Allows specifying <paramref name="expectations" /> on the member selected by the
		///     <paramref name="memberSelector" />.
		/// </summary>
		public AdditionalAndOrWhichResult
			AndWhich<TMember>(
				Func<TType, TMember?> memberSelector,
				Action<IThat<TMember?>> expectations,
				[CallerArgumentExpression("memberSelector")]
				string doNotPopulateThisValue = "")
		{
			_expectationBuilder.And(" and");
			return new AdditionalAndOrWhichResult(
				_expectationBuilder
					.ForMember(
						MemberAccessor<TType, TMember?>.FromFuncAsMemberAccessor(memberSelector,
							doNotPopulateThisValue),
						(member, stringBuilder) => stringBuilder.Append(" which ").Append(member))
					.AddExpectations(e
						=> expectations(new ThatSubject<TMember?>(e))),
				_returnValue);
		}
	}
}
