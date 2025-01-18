#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Core.Constraints;
using aweXpect.Json;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="JsonElement" /> values.
/// </summary>
public static partial class ThatJsonElement
{
	private readonly struct MatchConstraint(
		string it,
		object? expected,
		string expectedExpression,
		JsonOptions options)
		: IValueConstraint<JsonElement>
	{
		public ConstraintResult IsMetBy(JsonElement actual)
		{
			using JsonDocument expectedDocument =
				JsonDocument.Parse(JsonSerializer.Serialize(expected), options.DocumentOptions);

			JsonElementValidator.JsonComparisonResult comparisonResult = JsonElementValidator.Compare(
				actual,
				expectedDocument.RootElement,
				options);

			if (comparisonResult.HasError)
			{
				return new ConstraintResult.Failure<JsonElement>(actual, ToString(),
					$"{it} differed as{comparisonResult}");
			}

			return new ConstraintResult.Success<JsonElement>(actual, ToString());
		}

		public override string ToString()
			=> options.IgnoreAdditionalProperties switch
			{
				true => $"match {expectedExpression}",
				false => $"match {expectedExpression} exactly"
			};
	}


	private readonly struct BeValueKindConstraint(string it, JsonValueKind expected)
		: IValueConstraint<JsonElement>
	{
		public ConstraintResult IsMetBy(JsonElement actual)
		{
			if (actual.ValueKind != expected)
			{
				return new ConstraintResult.Failure<JsonElement>(actual, ToString(),
					$"{it} was {actual.ValueKind}");
			}

			return new ConstraintResult.Success<JsonElement>(actual, ToString());
		}

		public override string ToString()
			=> $"be {expected}";
	}
}
#endif
