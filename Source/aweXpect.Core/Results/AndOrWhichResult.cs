using System;
using System.Linq.Expressions;
using aweXpect.Core;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows accessing
///     underlying
///     properties with <see cref="AndOrWhichResult{TResult,TValue,TSelf}.Which{TProperty}" />.
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
///     underlying
///     properties with <see cref="Which{TProperty}" />.
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
	///     Allows specifying <paramref name="expectations"/> on a member of the current value.
	/// </summary>
	public AdditionalAndOrWhichResult
		Which<TProperty>(
			Expression<Func<TType, TProperty?>> memberSelector,
			Action<IExpectSubject<TProperty?>> expectations)
	{
		return new AdditionalAndOrWhichResult(
			_expectationBuilder
				.ForProperty(PropertyAccessor<TType, TProperty?>.FromExpression(memberSelector),
					(property, expectation) => $" which {property}should {expectation}")
				.AddExpectations(e => expectations(new Expect.ThatSubject<TProperty?>(e))),
			_returnValue);
	}

	/// <summary>
	///     The result of an additional expectation for the underlying type.
	///     <para />
	///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows accessing
	///     underlying properties with <see cref="AndWhich{TProperty}" />.
	/// </summary>
	public class AdditionalAndOrWhichResult(
		ExpectationBuilder expectationBuilder,
		TThat returnValue)
		: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	{
		private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;
		private readonly TThat _returnValue = returnValue;

		/// <summary>
		///     Allows specifying <paramref name="expectations"/> on a member of the current value.
		/// </summary>
		public AdditionalAndOrWhichResult
			AndWhich<TProperty>(
				Expression<Func<TType, TProperty?>> memberSelector,
				Action<IExpectSubject<TProperty?>> expectations)
		{
			_expectationBuilder.And(" and");
			return new AdditionalAndOrWhichResult(
				_expectationBuilder
					.ForProperty(
						PropertyAccessor<TType, TProperty?>.FromExpression(memberSelector),
						(property, expectation) => $" which {property}should {expectation}")
					.AddExpectations(e
						=> expectations(new Expect.ThatSubject<TProperty?>(e))),
				_returnValue);
		}
	}
}
