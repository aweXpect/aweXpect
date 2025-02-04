using aweXpect.Core;

namespace aweXpect.Results;

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
	private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;

	/// <summary>
	///     … AND …
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
	///     … OR …
	/// </summary>
	public TThat Or
	{
		get
		{
			_expectationBuilder.Or();
			return returnValue;
		}
	}
}
