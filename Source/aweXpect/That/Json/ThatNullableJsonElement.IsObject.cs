#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Json;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableJsonElement
{
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is an <see cref="JsonValueKind.Object" />.
	/// </summary>
	public static AndOrResult<JsonElement?, IExpectSubject<JsonElement?>> IsObject(this IExpectSubject<JsonElement?> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeValueKindConstraint(it, JsonValueKind.Object)),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> is an <see cref="JsonValueKind.Object" />
	///     whose value satisfies the <paramref name="expectation" />.
	/// </summary>
	public static AndOrResult<JsonElement?, IExpectSubject<JsonElement?>> IsObject(this IExpectSubject<JsonElement?> source,
		Func<IJsonObjectResult, IJsonObjectResult> expectation,
		Func<JsonOptions, JsonOptions>? options = null)
	{
		JsonOptions jsonOptions = new();
		jsonOptions.IgnoringAdditionalProperties();
		if (options != null)
		{
			jsonOptions = options(jsonOptions);
		}
		return new AndOrResult<JsonElement?, IExpectSubject<JsonElement?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeObjectConstraint(it, expectation, jsonOptions)),
			source);
	}

	private readonly struct BeObjectConstraint(
		string it,
		Func<IJsonObjectResult, IJsonObjectResult> expectation,
		JsonOptions options)
		: IValueConstraint<JsonElement?>
	{
		public ConstraintResult IsMetBy(JsonElement? actual)
		{
			JsonValidation jsonValidation = new(actual, JsonValueKind.Object, options);
			expectation(jsonValidation);
			if (!jsonValidation.IsMet())
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, jsonValidation.GetExpectation(),
					jsonValidation.GetFailure(it));
			}

			return new ConstraintResult.Success<JsonElement?>(actual, jsonValidation.GetExpectation());
		}
	}
}
#endif
