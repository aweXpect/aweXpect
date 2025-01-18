#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Results;

/// <summary>
///     An <see cref="AndOrResult{TType,TThat}" /> for JSON strings.
/// </summary>
public class JsonWhichResult(
	ExpectationBuilder expectationBuilder,
	IExpectSubject<string?> returnValue,
	JsonDocumentOptions options) : AndOrResult<string?, IExpectSubject<string?>>(expectationBuilder, returnValue)
{
	private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;

	/// <summary>
	///     Allows specifying <paramref name="expectations" /> on the <see cref="JsonElement" />
	///     represented by the <see langword="string" />.
	/// </summary>
	public AndOrResult<string?, IExpectSubject<string?>> Which(Action<IExpectSubject<JsonElement?>> expectations)
	{
		_expectationBuilder
			.ForMember(MemberAccessor<string?, JsonElement?>.FromFunc(s =>
				{
					if (s is null)
					{
						return null;
					}

					using JsonDocument jsonDocument = JsonDocument.Parse(s, options);
					return jsonDocument.RootElement.Clone();
				}, "it"),
				(_, expectation) => $" which should {expectation}")
			.AddExpectations(e => expectations(new That.Subject<JsonElement?>(e)));
		return this;
	}
}
#endif
