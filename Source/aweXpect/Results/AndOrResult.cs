using Testably.Expectations.Core;

namespace Testably.Expectations.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     Allows combining multiple expectations with <see cref="AndOrResult{TResult,TValue,TSelf}.And" /> and
///     <see cref="AndOrResult{TResult,TValue,TSelf}.Or" />.
/// </summary>
public class AndOrResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue)
	: AndOrResult<TType, TThat, AndOrResult<TType, TThat>>(
		expectationBuilder,
		returnValue);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     Allows combining multiple expectations with <see cref="And" /> and <see cref="Or" />.
/// </summary>
public class AndOrResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue)
	: ExpectationResult<TType, TSelf>(expectationBuilder)
	where TSelf : AndOrResult<TType, TThat, TSelf>
{
	/// <summary>
	///     Combine multiple expectations with AND
	/// </summary>
	public TThat And
	{
		get
		{
			_expectationBuilder.And();
			return returnValue;
		}
	}

	/// <summary>
	///     Combine multiple expectations with OR
	/// </summary>
	public TThat Or
	{
		get
		{
			_expectationBuilder.Or();
			return returnValue;
		}
	}

	private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;
}
