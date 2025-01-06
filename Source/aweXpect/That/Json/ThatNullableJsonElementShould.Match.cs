#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Json;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableJsonElementShould
{
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> matches the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<JsonElement?, IThat<JsonElement?>> Match(this IThat<JsonElement?> source,
		object? expected,
		Func<JsonOptions, JsonOptions>? options = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		JsonOptions jsonOptions = new();
		jsonOptions.IgnoringAdditionalProperties();
		if (options != null)
		{
			jsonOptions = options(jsonOptions);
		}

		return new AndOrResult<JsonElement?, IThat<JsonElement?>>(source.ExpectationBuilder.AddConstraint(it
				=> new MatchConstraint(it, expected, doNotPopulateThisValue, jsonOptions)),
			source);
	}

	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> matches the <paramref name="expected" /> array.
	/// </summary>
	public static AndOrResult<JsonElement?, IThat<JsonElement?>> Match<T>(this IThat<JsonElement?> source,
		IEnumerable<T> expected,
		Func<JsonOptions, JsonOptions>? options = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		JsonOptions jsonOptions = new();
		jsonOptions.IgnoringAdditionalProperties();
		if (options != null)
		{
			jsonOptions = options(jsonOptions);
		}

		return new AndOrResult<JsonElement?, IThat<JsonElement?>>(source.ExpectationBuilder.AddConstraint(it
				=> new MatchConstraint(it, expected, doNotPopulateThisValue, jsonOptions)),
			source);
	}
}
#endif
