#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Customization;

namespace aweXpect.Json;

internal sealed class JsonMatchType(JsonOptions options) : IStringMatchType
{
	private readonly Dictionary<string, string> _differences = new();
	private string? _deserializationError;

	/// <inheritdoc cref="IStringMatchType.GetExtendedFailure(string, string?, string?, bool, IEqualityComparer{string})" />
	public string GetExtendedFailure(
		string it,
		string? actual,
		string? pattern,
		bool ignoreCase,
		IEqualityComparer<string> comparer)
	{
		if (_deserializationError != null)
		{
			return _deserializationError;
		}

		StringBuilder sb = new();
		sb.Append(it).Append(' ');

		if (_differences.Any())
		{
			sb.Append("differed as");
			bool hasMoreThanOneDifference = _differences.Count > 1;
			int count = 0;
			foreach (KeyValuePair<string, string> differentMember in _differences)
			{
				if (count++ >= Customize.Formatting.MaximumNumberOfCollectionItems)
				{
					sb.AppendLine().Append("   … (")
						.Append(_differences.Count - Customize.Formatting.MaximumNumberOfCollectionItems)
						.Append(" more)");
					return sb.ToString();
				}

				if (hasMoreThanOneDifference)
				{
					sb.AppendLine().Append(' ');
				}

				sb.Append(' ').Append(differentMember.Key).Append(' ').Append(differentMember.Value).Append(" and");
			}
		}

		sb.Length -= 4;

		return sb.ToString();
	}

	/// <inheritdoc cref="IStringMatchType.Matches(string?, string?, bool, IEqualityComparer{string})" />
	public bool Matches(
		string? actual,
		string? expected,
		bool ignoreCase,
		IEqualityComparer<string> comparer)
	{
		if (actual == null && expected == null)
		{
			return true;
		}

		if (actual == null || expected == null)
		{
			return false;
		}

		try
		{
			using JsonDocument expectedJson = JsonDocument.Parse(expected, options.DocumentOptions);
			try
			{
				using JsonDocument actualJson = JsonDocument.Parse(actual, options.DocumentOptions);

				return CompareJson("$",
					actualJson.RootElement,
					expectedJson.RootElement);
			}
			catch (JsonException e)
			{
				_deserializationError = "could not parse subject: " + e.Message;
			}
		}
		catch (JsonException e)
		{
			_deserializationError = "could not parse expected: " + e.Message;
		}

		return false;
	}

	/// <inheritdoc cref="IStringMatchType.GetExpectation(string?, bool)" />
	public string GetExpectation(string? expected, bool useActiveGrammaticVoice)
		=> $"be JSON equivalent to {expected}";

	private bool CompareJson(string path,
		JsonElement actualElement,
		JsonElement expectedElement)
		=> actualElement.ValueKind switch
		{
			JsonValueKind.Array => CompareJsonArray(path, actualElement, expectedElement),
			JsonValueKind.False => CompareJsonBoolean(JsonValueKind.False, path, actualElement, expectedElement),
			JsonValueKind.True => CompareJsonBoolean(JsonValueKind.True, path, actualElement, expectedElement),
			JsonValueKind.Null => CompareJsonNull(path, actualElement, expectedElement),
			JsonValueKind.Number => CompareJsonNumber(path, actualElement, expectedElement),
			JsonValueKind.String => CompareJsonString(path, actualElement, expectedElement),
			JsonValueKind.Object => CompareJsonObject(path, actualElement, expectedElement),
			_ => throw new ArgumentOutOfRangeException($"Unsupported JsonValueKind: {actualElement.ValueKind}")
		};

	private bool CompareJsonArray(
		string path,
		JsonElement actualElement,
		JsonElement expectedElement)
	{
		if (expectedElement.ValueKind != JsonValueKind.Array)
		{
			_differences[path] = $"was {Format(actualElement, true)} instead of {Format(expectedElement)}";
			return false;
		}

		bool isConsideredEqual = true;
		for (int index = 0; index < expectedElement.GetArrayLength(); index++)
		{
			string memberPath = path + "[" + index + "]";
			JsonElement expectedArrayElement = expectedElement[index];
			if (actualElement.GetArrayLength() <= index)
			{
				_differences.Add(memberPath, $"had missing {Format(expectedArrayElement)}");
				isConsideredEqual = false;
				continue;
			}

			JsonElement actualArrayElement = actualElement[index];
			isConsideredEqual = CompareJson(memberPath, actualArrayElement, expectedArrayElement) && isConsideredEqual;
		}

		for (int index = expectedElement.GetArrayLength(); index < actualElement.GetArrayLength(); index++)
		{
			JsonElement actualArrayElement = actualElement[index];
			string memberPath = path + "[" + index + "]";
			_differences.Add(memberPath, $"had unexpected {Format(actualArrayElement)}");
			isConsideredEqual = false;
		}

		return isConsideredEqual;
	}

	private bool CompareJsonBoolean(
		JsonValueKind valueKind,
		string path,
		JsonElement actualElement,
		JsonElement expectedElement)
	{
		if (expectedElement.ValueKind != valueKind)
		{
			_differences[path] = expectedElement.ValueKind is JsonValueKind.False or JsonValueKind.True
				? $"was {Format(actualElement)} instead of {Format(expectedElement)}"
				: $"was {Format(actualElement, true)} instead of {Format(expectedElement)}";
			return false;
		}

		return true;
	}

	private bool CompareJsonNull(string path,
		JsonElement actualElement,
		JsonElement expectedElement)
	{
		if (expectedElement.ValueKind != JsonValueKind.Null)
		{
			_differences[path] = $"was {Format(actualElement, true)} instead of {Format(expectedElement)}";
			return false;
		}

		return true;
	}

	private bool CompareJsonNumber(string path,
		JsonElement actualElement,
		JsonElement expectedElement)
	{
		if (expectedElement.ValueKind != JsonValueKind.Number)
		{
			_differences[path] = $"was {Format(actualElement, true)} instead of {Format(expectedElement)}";
			return false;
		}

		if (actualElement.TryGetInt32(out int v1) && expectedElement.TryGetInt32(out int v2))
		{
			if (v1 == v2)
			{
				return true;
			}

			_differences[path] = $"was {Format(actualElement)} instead of {Format(expectedElement)}";
			return false;
		}

		if (actualElement.TryGetDouble(out double n1) && expectedElement.TryGetDouble(out double n2))
		{
			if (n1.Equals(n2))
			{
				return true;
			}

			_differences[path] = $"was {Format(actualElement)} instead of {Format(expectedElement)}";
			return false;
		}

		_differences[path] = "differed";
		return false;
	}

	private bool CompareJsonObject(string path,
		JsonElement actualElement,
		JsonElement expectedElement)
	{
		if (expectedElement.ValueKind != JsonValueKind.Object)
		{
			_differences[path] = $"was {Format(actualElement, true)} instead of {Format(expectedElement)}";
			return false;
		}

		bool isConsideredEqual = true;
		foreach (JsonProperty item in expectedElement.EnumerateObject())
		{
			string memberPath = path + "." + item.Name;
			if (!actualElement.TryGetProperty(item.Name, out JsonElement property))
			{
				_differences.Add(memberPath, "was missing");
				isConsideredEqual = false;
				continue;
			}

			isConsideredEqual = CompareJson(memberPath, property, item.Value) && isConsideredEqual;
		}

		if (!options.IgnoreAdditionalProperties)
		{
			foreach (string propertyName in actualElement.EnumerateObject().Select(x => x.Name))
			{
				string memberPath = path + "." + propertyName;
				if (_differences.ContainsKey(memberPath) ||
				    expectedElement.TryGetProperty(propertyName, out _))
				{
					continue;
				}

				_differences.Add(memberPath, "was unexpected");
				isConsideredEqual = false;
			}
		}

		return isConsideredEqual;
	}

	private bool CompareJsonString(string path,
		JsonElement actualElement,
		JsonElement expectedElement)
	{
		if (expectedElement.ValueKind != JsonValueKind.String)
		{
			_differences[path] = $"was {Format(actualElement, true)} instead of {Format(expectedElement)}";
			return false;
		}

		string? value1 = actualElement.GetString();
		string? value2 = expectedElement.GetString();
		if (value1 != value2)
		{
			_differences[path] = $"was {Format(actualElement)} instead of {Format(expectedElement)}";
			return false;
		}

		return true;
	}

	private static string Format(JsonElement jsonElement, bool includeType = false)
	{
		string GetKindName(JsonValueKind kind)
			=> kind switch
			{
				JsonValueKind.False => "boolean",
				JsonValueKind.True => "boolean",
				JsonValueKind.Number => "number",
				JsonValueKind.String => "string",
				JsonValueKind.Array => "array",
				JsonValueKind.Object => "object",
				_ => ""
			};

		if (jsonElement.ValueKind == JsonValueKind.Null)
		{
			return "Null";
		}

		if (jsonElement.ValueKind == JsonValueKind.String)
		{
			return includeType
				? $"string \"{jsonElement}\""
				: $"\"{jsonElement}\"";
		}

		return includeType
			? $"{GetKindName(jsonElement.ValueKind)} {jsonElement}"
			: jsonElement.ToString();
	}
}
#endif
