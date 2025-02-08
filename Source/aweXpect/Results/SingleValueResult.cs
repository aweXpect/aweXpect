using System;
using aweXpect.Core;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a collection contains a single item.
/// </summary>
/// <remarks>
///     <seealso cref="ExpectationResult{TType,TSelf}" />
/// </remarks>
public class SingleValueResult<TCollection, TThat, TValue>
	: AndOrResult<TCollection, TThat>
{
	private readonly ExpectationBuilder _expectationBuilder;
	private readonly Func<TCollection, TValue> _memberAccessor;

	internal SingleValueResult(ExpectationBuilder expectationBuilder, TThat returnValue,
		Func<TCollection, TValue> memberAccessor)
		: base(expectationBuilder, returnValue)
	{
		_expectationBuilder = expectationBuilder;
		_memberAccessor = memberAccessor;
	}

	/// <summary>
	///     Further expectations on the value <typeparamref name="TValue" />
	/// </summary>
	public IThat<TValue> WhoseValue
		=> new ThatSubject<TValue>(_expectationBuilder.ForWhich(_memberAccessor, " whose value should ", "the value"));
}
