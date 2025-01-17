#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Json;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatJsonElement
{
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> matches the <paramref name="expected" /> value exactly.
	/// </summary>
	public static AndOrResult<JsonElement, IExpectSubject<JsonElement>> MatchesExactly(
		this IExpectSubject<JsonElement> source,
		object? expected,
		Func<JsonOptions, JsonOptions>? options = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		JsonOptions jsonOptions = new();
		if (options != null)
		{
			jsonOptions = options(jsonOptions);
		}

		return new AndOrResult<JsonElement, IExpectSubject<JsonElement>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new MatchConstraint(it, expected, doNotPopulateThisValue, jsonOptions)),
			source);
	}

	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> matches the <paramref name="expected" /> array exactly.
	/// </summary>
	public static AndOrResult<JsonElement, IExpectSubject<JsonElement>> MatchesExactly<T>(
		this IExpectSubject<JsonElement> source,
		IEnumerable<T> expected,
		Func<JsonOptions, JsonOptions>? options = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		JsonOptions jsonOptions = new();
		if (options != null)
		{
			jsonOptions = options(jsonOptions);
		}

		return new AndOrResult<JsonElement, IExpectSubject<JsonElement>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new MatchConstraint(it, expected, doNotPopulateThisValue, jsonOptions)),
			source);
	}
}
#endif
