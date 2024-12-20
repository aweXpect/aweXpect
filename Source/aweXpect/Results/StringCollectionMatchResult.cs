using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="ObjectEqualityResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="CollectionMatchOptions" />.
/// </summary>
public class StringCollectionMatchResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: StringCollectionMatchResult<TType, TThat,
		StringCollectionMatchResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		options,
		collectionMatchOptions);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="ObjectEqualityResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="CollectionMatchOptions" />.
/// </summary>
public class StringCollectionMatchResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: StringEqualityTypeResult<TType, TThat>(expectationBuilder, returnValue, options)
	where TSelf : StringCollectionMatchResult<TType, TThat, TSelf>
{
	/// <summary>
	///     Ignores the order in the subject and expected values.
	/// </summary>
	public TSelf InAnyOrder()
	{
		collectionMatchOptions.InAnyOrder();
		return (TSelf)this;
	}

	/// <summary>
	///     Ignores duplicates in both collections.
	/// </summary>
	public TSelf IgnoringDuplicates()
	{
		collectionMatchOptions.IgnoringDuplicates();
		return (TSelf)this;
	}
}
