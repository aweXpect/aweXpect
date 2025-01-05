#if NET8_0_OR_GREATER
using System;
using System.Linq;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatJsonElementShould
{
	/// <summary>
	///     Verifies that the number of items in the current <see cref="JsonElement" /> matches the supplied
	///     <paramref name="expected" /> amount.
	/// </summary>
	public static AndOrResult<JsonElement, IThat<JsonElement>> HaveCount(this IThat<JsonElement> source, int expected)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(expected);
		return new AndOrResult<JsonElement, IThat<JsonElement>>(source.ExpectationBuilder.AddConstraint(it
				=> new HaveCountConstraint(it, expected)),
			source);
	}

	private readonly struct HaveCountConstraint(string it, int expected)
		: IValueConstraint<JsonElement>
	{
		public ConstraintResult IsMetBy(JsonElement actual)
		{
			if (actual.ValueKind is not JsonValueKind.Array and not JsonValueKind.Object)
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, ToString(),
					$"{it} had type {actual.ValueKind}");
			}

			int? count = actual.ValueKind switch
			{
				JsonValueKind.Array => actual.GetArrayLength(),
				_ => actual.EnumerateObject().Count()
			};

			if (count == expected)
			{
				return new ConstraintResult.Success<JsonElement?>(actual, ToString());
			}

			return new ConstraintResult.Failure<JsonElement?>(actual, ToString(),
				$"{it} had {count}");
		}

		public override string ToString()
			=> $"have {expected} items";
	}
}
#endif
