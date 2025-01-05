#if NET8_0_OR_GREATER
using System;
using System.Linq;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableJsonElementShould
{
	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> has the <paramref name="expected" /> number of items.
	/// </summary>
	/// <remarks>
	///     Fails when the <see cref="JsonValueKind" /> is neither <see cref="JsonValueKind.Array" />
	///     nor <see cref="JsonValueKind.Object" />.
	/// </remarks>
	public static AndOrResult<JsonElement?, IThat<JsonElement?>> HaveCount(this IThat<JsonElement?> source,
		int expected)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(expected);
		return new AndOrResult<JsonElement?, IThat<JsonElement?>>(source.ExpectationBuilder.AddConstraint(it
				=> new HaveCountConstraint(it, expected)),
			source);
	}

	/// <summary>
	///     Verifies that the subject <see cref="JsonElement" /> does not have
	///     the <paramref name="unexpected" /> number of items.
	/// </summary>
	/// <remarks>
	///     Fails when the <see cref="JsonValueKind" /> is neither <see cref="JsonValueKind.Array" />
	///     nor <see cref="JsonValueKind.Object" />.
	/// </remarks>
	public static AndOrResult<JsonElement?, IThat<JsonElement?>> NotHaveCount(this IThat<JsonElement?> source,
		int unexpected)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(unexpected);
		return new AndOrResult<JsonElement?, IThat<JsonElement?>>(source.ExpectationBuilder.AddConstraint(it
				=> new NotHaveCountConstraint(it, unexpected)),
			source);
	}

	private readonly struct HaveCountConstraint(string it, int expected)
		: IValueConstraint<JsonElement?>
	{
		public ConstraintResult IsMetBy(JsonElement? actual)
		{
			if (actual == null)
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, ToString(),
					$"{it} was <null>");
			}

			if (actual.Value.ValueKind is not JsonValueKind.Array and not JsonValueKind.Object)
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, ToString(),
					$"{it} had type {actual.Value.ValueKind}");
			}

			int? count = actual.Value.ValueKind switch
			{
				JsonValueKind.Array => actual.Value.GetArrayLength(),
				_ => actual.Value.EnumerateObject().Count()
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

	private readonly struct NotHaveCountConstraint(string it, int unexpected)
		: IValueConstraint<JsonElement?>
	{
		public ConstraintResult IsMetBy(JsonElement? actual)
		{
			if (actual == null)
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, ToString(),
					$"{it} was <null>");
			}

			if (actual.Value.ValueKind is not JsonValueKind.Array and not JsonValueKind.Object)
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, ToString(),
					$"{it} had type {actual.Value.ValueKind}");
			}

			int? count = actual.Value.ValueKind switch
			{
				JsonValueKind.Array => actual.Value.GetArrayLength(),
				_ => actual.Value.EnumerateObject().Count()
			};

			if (count == unexpected)
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, ToString(),
					$"{it} did");
			}

			return new ConstraintResult.Success<JsonElement?>(actual, ToString());
		}

		public override string ToString()
			=> $"not have {unexpected} items";
	}
}
#endif
