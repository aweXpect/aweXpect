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

	/// <inheritdoc cref="IStringMatchType.GetExtendedFailure(string, string?, string?, bool, IEqualityComparer{string})" />
	public string GetExtendedFailure(
		string it,
		string? actual,
		string? pattern,
		bool ignoreCase,
		IEqualityComparer<string> comparer)
	{
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

		using JsonDocument json1 = JsonDocument.Parse(actual, options.DocumentOptions);
		using JsonDocument json2 = JsonDocument.Parse(expected, options.DocumentOptions);

		return CompareJson("",
			json1.RootElement,
			json2.RootElement);
	}

	/// <inheritdoc cref="IStringMatchType.GetExpectation(string?, bool)" />
	public string GetExpectation(string? expected, bool useActiveGrammaticVoice)
		=> $"be JSON equivalent to {expected}";

	private bool CompareJson(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
		=> jsonElement1.ValueKind switch
		{
			JsonValueKind.False => CompareJsonFalse(path, jsonElement1, jsonElement2),
			JsonValueKind.True => CompareJsonTrue(path, jsonElement1, jsonElement2),
			JsonValueKind.Number => CompareJsonNumber(path, jsonElement1, jsonElement2),
			JsonValueKind.String => CompareJsonString(path, jsonElement1, jsonElement2),
			JsonValueKind.Null => CompareJsonNull(path, jsonElement1, jsonElement2),
			JsonValueKind.Array => CompareJsonArray(path, jsonElement1, jsonElement2),
			JsonValueKind.Object => CompareJsonObject(path, jsonElement1, jsonElement2),
			JsonValueKind.Undefined => CompareJsonUndefined(path, jsonElement1, jsonElement2),
			_ => throw new ArgumentOutOfRangeException($"Unsupported JsonValueKind: {jsonElement1.ValueKind}")
		};

	private bool CompareJsonFalse(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
	{
		if (jsonElement2.ValueKind != JsonValueKind.False)
		{
			_differences[path] = jsonElement2.ValueKind == JsonValueKind.True
				? $"was {Format(jsonElement1)} instead of {Format(jsonElement2)}"
				: $"was {Format(jsonElement1, true)} instead of {Format(jsonElement2)}";
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

	private bool CompareJsonTrue(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
	{
		if (jsonElement2.ValueKind != JsonValueKind.True)
		{
			_differences[path] = jsonElement2.ValueKind == JsonValueKind.False
				? $"was {Format(jsonElement1)} instead of {Format(jsonElement2)}"
				: $"was {Format(jsonElement1, true)} instead of {Format(jsonElement2)}";
			return false;
		}

		return true;
	}

	private bool CompareJsonNumber(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
	{
		if (jsonElement2.ValueKind != JsonValueKind.Number)
		{
			_differences[path] = $"was {Format(jsonElement1, true)} instead of {Format(jsonElement2)}";
			return false;
		}

		if (jsonElement1.TryGetInt32(out int v1) && jsonElement2.TryGetInt32(out int v2))
		{
			if (v1 == v2)
			{
				return true;
			}

			_differences[path] = $"was {Format(jsonElement1)} instead of {Format(jsonElement2)}";
			return false;
		}

		if (jsonElement1.TryGetDouble(out double n1) && jsonElement2.TryGetDouble(out double n2))
		{
			if (n1.Equals(n2))
			{
				return true;
			}

			_differences[path] = $"was {Format(jsonElement1)} instead of {Format(jsonElement2)}";
			return false;
		}

		_differences[path] = "differed";
		return false;
	}

	private bool CompareJsonString(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
	{
		if (jsonElement2.ValueKind != JsonValueKind.String)
		{
			_differences[path] = $"was {Format(jsonElement1, true)} instead of {Format(jsonElement2)}";
			return false;
		}

		string? value1 = jsonElement1.GetString();
		string? value2 = jsonElement2.GetString();
		if (value1 != value2)
		{
			_differences[path] = $"was {Format(jsonElement1)} instead of {Format(jsonElement2)}";
			return false;
		}

		return true;
	}

	private bool CompareJsonNull(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
	{
		if (jsonElement2.ValueKind != JsonValueKind.Null)
		{
			_differences[path] = $"was {Format(jsonElement1, true)} instead of {Format(jsonElement2)}";
			return false;
		}

		return true;
	}

	private bool CompareJsonUndefined(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
	{
		if (jsonElement2.ValueKind != JsonValueKind.Undefined)
		{
			_differences[path] = $"was {Format(jsonElement1)} instead of {Format(jsonElement2)}";
			return false;
		}

		return true;
	}

	private bool CompareJsonObject(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
	{
		if (jsonElement2.ValueKind != JsonValueKind.Object)
		{
			_differences[path] = $"was {Format(jsonElement1, true)} instead of {Format(jsonElement2)}";
			return false;
		}

		bool isConsideredEqual = true;
		foreach (JsonProperty item in jsonElement2.EnumerateObject())
		{
			string memberPath = path + "." + item.Name;
			if (!jsonElement1.TryGetProperty(item.Name, out JsonElement property))
			{
				_differences.Add(memberPath, "was missing");
				isConsideredEqual = false;
				continue;
			}

			isConsideredEqual = CompareJson(memberPath, property, item.Value) && isConsideredEqual;
		}

		if (options.CheckForAdditionalProperties)
		{
			foreach (JsonProperty item in jsonElement1.EnumerateObject())
			{
				string memberPath = path + "." + item.Name;
				if (_differences.ContainsKey(memberPath) ||
				    jsonElement2.TryGetProperty(item.Name, out _))
				{
					continue;
				}

				_differences.Add(memberPath, "was unexpected");
				isConsideredEqual = false;
			}
		}

		return isConsideredEqual;
	}

	private bool CompareJsonArray(string path,
		JsonElement jsonElement1,
		JsonElement jsonElement2)
	{
		if (jsonElement2.ValueKind != JsonValueKind.Array)
		{
			_differences[path] = $"was {Format(jsonElement1, true)} instead of {Format(jsonElement2)}";
			return false;
		}

		bool isConsideredEqual = true;
		for (int index = 0; index < jsonElement2.GetArrayLength(); index++)
		{
			string memberPath = path + "[" + index + "]";
			JsonElement expectedElement = jsonElement2[index];
			if (jsonElement1.GetArrayLength() <= index)
			{
				_differences.Add(memberPath, $"had missing {Format(expectedElement)}");
				isConsideredEqual = false;
				continue;
			}

			JsonElement actualElement = jsonElement1[index];
			isConsideredEqual = CompareJson(memberPath, actualElement, expectedElement) && isConsideredEqual;
		}

		for (int index = jsonElement2.GetArrayLength(); index < jsonElement1.GetArrayLength(); index++)
		{
			JsonElement actualElement = jsonElement1[index];
			string memberPath = path + "[" + index + "]";
			_differences.Add(memberPath, $"had unexpected {Format(actualElement)}");
			isConsideredEqual = false;
		}

		return isConsideredEqual;
	}
}
#endif
