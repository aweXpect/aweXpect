#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Json;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="JsonElement" />? values.
/// </summary>
public static partial class ThatNullableJsonElementShould
{
	/// <summary>
	///     Start expectations for the current <see cref="JsonElement" />? <paramref name="subject" />.
	/// </summary>
	public static IThat<JsonElement?> Should(
		this IExpectSubject<JsonElement?> subject)
		=> subject.Should(That.WithoutAction);
	
	private readonly struct MatchConstraint(
		string it,
		object? expected,
		string expectedExpression,
		JsonOptions options)
		: IValueConstraint<JsonElement?>
	{
		public ConstraintResult IsMetBy(JsonElement? actual)
		{
			if (actual == null)
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, ToString(),
					$"{it} was <null>");
			}

			using JsonDocument expectedDocument =
				JsonDocument.Parse(JsonSerializer.Serialize(expected), options.DocumentOptions);

			JsonElementValidator.JsonComparisonResult comparisonResult = JsonElementValidator.Compare(
				actual.Value,
				expectedDocument.RootElement,
				options);

			if (comparisonResult.HasError)
			{
				return new ConstraintResult.Failure<JsonElement?>(actual, ToString(), $"{it} {comparisonResult}");
			}

			return new ConstraintResult.Success<JsonElement?>(actual, ToString());
		}

		public override string ToString()
			=> options.IgnoreAdditionalProperties switch
			{
				true => $"match {expectedExpression}",
				false => $"match {expectedExpression} exactly"
			};
	}
}
#endif
