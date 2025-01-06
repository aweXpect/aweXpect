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
	///     Verifies that the subject <see cref="JsonElement" /> matches the <paramref name="expected" /> value exactly.
	/// </summary>
	public static AndOrResult<JsonElement?, IThat<JsonElement?>> MatchExactly(this IThat<JsonElement?> source,
		object? expected,
		Func<JsonOptions, JsonOptions>? options = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		JsonOptions jsonOptions = new();
		if (options != null)
		{
			jsonOptions = options(jsonOptions);
		}

		return new AndOrResult<JsonElement?, IThat<JsonElement?>>(source.ExpectationBuilder.AddConstraint(it
				=> new MatchConstraint(it, expected, doNotPopulateThisValue, jsonOptions)),
			source);
	}

	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> matches the <paramref name="expected" /> array exactly.
	/// </summary>
	public static AndOrResult<JsonElement?, IThat<JsonElement?>> MatchExactly<T>(this IThat<JsonElement?> source,
		IEnumerable<T> expected,
		Func<JsonOptions, JsonOptions>? options = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		JsonOptions jsonOptions = new();
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
