using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <see cref="ObjectEqualityOptions" />.
/// </summary>
public class ObjectEqualityResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions options)
	: ObjectEqualityResult<TType, TThat,
		ObjectEqualityResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		options);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <see cref="ObjectEqualityOptions" />.
/// </summary>
public class ObjectEqualityResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions options)
	: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue),
		IOptionsProvider<ObjectEqualityOptions>
	where TSelf : ObjectEqualityResult<TType, TThat, TSelf>
{
	/// <inheritdoc cref="IOptionsProvider{TOptions}.Options" />
	ObjectEqualityOptions IOptionsProvider<ObjectEqualityOptions>.Options => options;

	/// <summary>
	///     Uses the provided <paramref name="comparer" /> for comparing <see langword="object" />s.
	/// </summary>
	/// <remarks>
	///     For more customization, the comparer can additionally implement the <see cref="IComparerOptions" /> interface.
	/// </remarks>
	public TSelf Using(IEqualityComparer<object> comparer)
	{
		options.Using(comparer);
		return (TSelf)this;
	}
}
