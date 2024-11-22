using System;
using System.Linq.Expressions;
using Testably.Expectations.Core;

namespace Testably.Expectations.Results;

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
	///     Allows specifying expectations on a property of the current value.
	/// </summary>
	public WhichResult<TProperty, AdditionalAndOrWhichResult>
		Which<TProperty>(Expression<Func<TType, TProperty?>> selector)
	{
		return new WhichResult<TProperty, AdditionalAndOrWhichResult>(
			expectations =>
			{
				return new AdditionalAndOrWhichResult(
					_expectationBuilder
						.ForProperty(PropertyAccessor<TType, TProperty?>.FromExpression(selector),
							(property, expectation) => $" which {property}should {expectation}")
						.AddExpectations(e => expectations(new Expect.ThatSubject<TProperty?>(e))),
					_returnValue);
			});
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
		///     Allows specifying expectations on a property of the current value.
		/// </summary>
		public WhichResult<TProperty, AdditionalAndOrWhichResult>
			AndWhich<TProperty>(Expression<Func<TType, TProperty?>> selector)
		{
			_expectationBuilder.And(" and");
			return new WhichResult<TProperty, AdditionalAndOrWhichResult>(
				expectations =>
				{
					return new AdditionalAndOrWhichResult(
						_expectationBuilder
							.ForProperty(
								PropertyAccessor<TType, TProperty?>.FromExpression(selector),
								(property, expectation) => $" which {property}should {expectation}")
							.AddExpectations(e
								=> expectations(new Expect.ThatSubject<TProperty?>(e))),
						_returnValue);
				});
		}
	}

	/// <summary>
	///     Intermediate result for chaining Which and Should methods.
	/// </summary>
	public class WhichResult<TProperty, TReturn>(
		Func<Action<IThat<TProperty?>>, TReturn> resultCallback)
	{
		/// <summary>
		///     Specifies the expectations on the selected property.
		/// </summary>
		public TReturn Should(
			Action<IThat<TProperty?>> expectations)
			=> resultCallback(expectations);
	}
}
