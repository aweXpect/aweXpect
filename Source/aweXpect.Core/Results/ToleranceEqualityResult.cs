using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />
///     that allows specifying a <typeparamref name="TTolerance" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <see cref="ObjectEqualityOptions" />.
/// </summary>
public class ToleranceEqualityResult<TType, TThat, TElement, TTolerance>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityWithToleranceOptions<TElement, TTolerance> options)
	: ToleranceEqualityResult<TType, TThat, TElement, TTolerance,
		ToleranceEqualityResult<TType, TThat, TElement, TTolerance>>(
		expectationBuilder,
		returnValue,
		options);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />
///     that allows specifying a <typeparamref name="TTolerance" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <see cref="ObjectEqualityOptions" />.
/// </summary>
public class ToleranceEqualityResult<TType, TThat, TElement, TTolerance, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityWithToleranceOptions<TElement, TTolerance> options)
	: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue),
		IOptionsProvider<ObjectEqualityOptions<TElement>>
	where TSelf : ToleranceEqualityResult<TType, TThat, TElement, TTolerance, TSelf>
{
	/// <inheritdoc cref="IOptionsProvider{TOptions}.Options" />
	ObjectEqualityOptions<TElement> IOptionsProvider<ObjectEqualityOptions<TElement>>.Options => options;

	/// <summary>
	///     Specifies a <paramref name="tolerance" /> to apply.
	/// </summary>
	public AndOrResult<TType, TThat, TSelf> Within(TTolerance tolerance)
	{
		options.Within(tolerance);
		return this;
	}
}
