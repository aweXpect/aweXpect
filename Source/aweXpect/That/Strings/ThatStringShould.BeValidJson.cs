#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Json;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is a valid JSON string.
	/// </summary>
	public static JsonWhichResult BeValidJson(
		this IThatShould<string?> source,
		Func<JsonDocumentOptions, JsonDocumentOptions>? options = null)
	{
		JsonDocumentOptions defaultOptions = Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get();
		if (options != null)
		{
			defaultOptions = options(defaultOptions);
		}

		return new JsonWhichResult(source.ExpectationBuilder.AddConstraint(it
				=> new BeValidJsonConstraint(it, defaultOptions)),
			source, defaultOptions);
	}

	private readonly struct BeValidJsonConstraint(string it, JsonDocumentOptions options) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<string?>(null, ToString(), $"{it} was <null>");
			}

			try
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(actual, options);
			}
			catch (JsonException jsonException)
			{
				return new ConstraintResult.Failure<string?>(actual, ToString(),
					$"{it} could not be parsed: {jsonException.Message}");
			}

			return new ConstraintResult.Success<string?>(actual, ToString());
		}

		public override string ToString()
			=> "be valid JSON";
	}
}
#endif
