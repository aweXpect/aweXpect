#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Json;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable once CheckNamespace
namespace aweXpect;

/// <summary>
///     Extension methods for working with JSON strings.
/// </summary>
public static class JsonExtensions
{
	/// <summary>
	///     Interpret the <see cref="string" /> as JSON.
	/// </summary>
	public static TSelf AsJson<TType, TThat, TSelf>(this StringEqualityResult<TType, TThat, TSelf> options,
		Func<JsonComparerOptions, JsonComparerOptions>? optionsCallback = null)
		where TSelf : StringEqualityResult<TType, TThat, TSelf>
	{
		JsonComparerOptions equivalencyOptions =
			optionsCallback?.Invoke(new JsonComparerOptions()) ?? new JsonComparerOptions();
		options.Options.SetMatchType(new JsonComparer(equivalencyOptions));
		return (TSelf)options;
	}

	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public static StringEqualityOptions AsJson(this StringEqualityOptions options,
		JsonComparerOptions equivalencyOptions)
	{
		options.SetMatchType(new JsonComparer(equivalencyOptions));
		return options;
	}

	private sealed class JsonComparer(JsonComparerOptions equivalencyOptions)
		: IStringMatchType, IEqualityComparer<string>, IComparerOptions
	{
		private readonly List<string> _additionalMembers = new();
		private readonly Dictionary<string, string> _differentMembers = new();
		private readonly List<string> _missingMembers = new();

		public string GetExpectation(string expectedExpression) => $"be JSON equivalent to {expectedExpression}";

		public string GetExtendedFailure(string it, object? actual, object? expected)
		{
			StringBuilder sb = new();
			sb.Append(it).Append(' ');
			if (_missingMembers.Any())
			{
				sb.Append("did not contain ");
				Formatter.Format(sb, _missingMembers, FormattingOptions.MultipleLines);
			}

			if (_additionalMembers.Any())
			{
				if (_missingMembers.Any())
				{
					sb.AppendLine(" and");
				}

				sb.Append("contained unexpected properties ");
				Formatter.Format(sb, _additionalMembers, FormattingOptions.MultipleLines);
			}

			if (_differentMembers.Any())
			{
				if (_missingMembers.Any() || _additionalMembers.Any())
				{
					sb.AppendLine(" and");
				}

				sb.Append("did not match the following properties ");
				Formatter.Format(sb, _differentMembers.Select(x => $"{x.Key}: {x.Value}"),
					FormattingOptions.MultipleLines);
			}

			return sb.ToString();
		}

		bool IEqualityComparer<string>.Equals(string? x, string? y)
		{
			if (x == null && y == null)
			{
				return true;
			}

			if (x == null || y == null)
			{
				return false;
			}

			using JsonDocument json1 = JsonDocument.Parse(x);
			using JsonDocument json2 = JsonDocument.Parse(y);

			return CompareJson("",
				json1.RootElement,
				json2.RootElement,
				equivalencyOptions);
		}

		public int GetHashCode(string obj) => obj.GetHashCode();

		public string GetExtendedFailure(string it, string? actual, string? pattern, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			StringBuilder sb = new();
			sb.Append(it).Append(' ');
			if (_missingMembers.Any())
			{
				sb.Append("did not contain:");
				foreach (string missingMember in _missingMembers)
				{
					sb.AppendLine();
					sb.Append(" - ").Append(missingMember);
				}
			}

			if (_additionalMembers.Any())
			{
				if (_missingMembers.Any())
				{
					sb.AppendLine().Append("and ");
				}

				sb.Append("contained unexpected members:");
				foreach (string additionalMember in _additionalMembers)
				{
					sb.AppendLine();
					sb.Append(" - ").Append(additionalMember);
				}
			}

			if (_differentMembers.Any())
			{
				if (_missingMembers.Any() || _additionalMembers.Any())
				{
					sb.AppendLine().Append("and ");
				}

				sb.Append("differed in:");
				foreach (KeyValuePair<string, string> differentMember in _differentMembers)
				{
					sb.AppendLine();
					sb.Append(" - ").Append(differentMember.Key).Append(" ").Append(differentMember.Value);
				}
			}

			return sb.ToString();
		}

		public bool Matches(string? actual, string? expected, bool ignoreCase, IEqualityComparer<string> comparer)
		{
			if (actual == null && expected == null)
			{
				return true;
			}

			if (actual == null || expected == null)
			{
				return false;
			}

			using JsonDocument json1 = JsonDocument.Parse(actual);
			using JsonDocument json2 = JsonDocument.Parse(expected);

			return CompareJson("",
				json1.RootElement,
				json2.RootElement,
				equivalencyOptions);
		}

		public string GetExpectation(string? expected, bool useActiveGrammaticVoice)
			=> $"be JSON equivalent to {expected}";

		private bool CompareJson(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
			=> jsonElement1.ValueKind switch
			{
				JsonValueKind.False => CompareJsonFalse(path, jsonElement1, jsonElement2, options),
				JsonValueKind.True => CompareJsonTrue(path, jsonElement1, jsonElement2, options),
				JsonValueKind.Number => CompareJsonNumber(path, jsonElement1, jsonElement2, options),
				JsonValueKind.String => CompareJsonString(path, jsonElement1, jsonElement2, options),
				JsonValueKind.Null => CompareJsonNull(path, jsonElement1, jsonElement2, options),
				JsonValueKind.Array => CompareJsonArray(path, jsonElement1, jsonElement2, options),
				JsonValueKind.Object => CompareJsonObject(path, jsonElement1, jsonElement2, options),
				JsonValueKind.Undefined => CompareJsonUndefined(path, jsonElement1, jsonElement2, options),
				_ => throw new ArgumentOutOfRangeException($"Unsupported JsonValueKind: {jsonElement1.ValueKind}")
			};

		private bool CompareJsonFalse(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.False)
			{
				_differentMembers[path] = "was false";
				return false;
			}

			return true;
		}

		private bool CompareJsonTrue(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.True)
			{
				_differentMembers[path] = "was true";
				return false;
			}

			return true;
		}

		private bool CompareJsonNumber(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.Number)
			{
				_differentMembers[path] = "was no number";
				return false;
			}

			if (jsonElement1.TryGetInt32(out int v1) && jsonElement2.TryGetInt32(out int v2))
			{
				if (v1 == v2)
				{
					return true;
				}

				_differentMembers[path] = $"was {Formatter.Format(v1)} instead of {Formatter.Format(v2)}";
				return false;
			}

			if (jsonElement1.TryGetDouble(out double n1) && jsonElement2.TryGetDouble(out double n2))
			{
				if (n1.Equals(n2))
				{
					return true;
				}

				_differentMembers[path] = $"was {Formatter.Format(n1)} instead of {Formatter.Format(n2)}";
				return false;
			}

			if (jsonElement1.TryGetDecimal(out decimal d1) && jsonElement2.TryGetDecimal(out decimal d2))
			{
				if (d1.Equals(d2))
				{
					return true;
				}

				_differentMembers[path] = $"was {Formatter.Format(d1)} instead of {Formatter.Format(d2)}";
				return false;
			}

			_differentMembers[path] = "differed";
			return false;
		}

		private bool CompareJsonString(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.String)
			{
				_differentMembers[path] = "was no string";
				return false;
			}

			string? value1 = jsonElement1.GetString();
			string? value2 = jsonElement2.GetString();
			if (value1 != value2)
			{
				_differentMembers.Add(path, $"was {Formatter.Format(value1)} instead of {Formatter.Format(value2)}");
				return false;
			}

			return true;
		}

		private bool CompareJsonNull(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.Null)
			{
				_differentMembers[path] = "was not null";
				return false;
			}

			return true;
		}

		private bool CompareJsonUndefined(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.Undefined)
			{
				_differentMembers[path] = "was not undefined";
				return false;
			}

			return true;
		}

		private bool CompareJsonObject(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.Object)
			{
				_differentMembers[path] = "was no object";
				return false;
			}

			bool isConsideredEqual = true;
			foreach (JsonProperty item in jsonElement2.EnumerateObject())
			{
				string memberPath = path + "." + item.Name;
				if (!jsonElement1.TryGetProperty(item.Name, out JsonElement property))
				{
					_missingMembers.Add(memberPath);
					isConsideredEqual = false;
					continue;
				}

				isConsideredEqual = CompareJson(memberPath, property, item.Value, options) && isConsideredEqual;
			}

			if (!options.IgnoreAdditionalMembers)
			{
				foreach (JsonProperty item in jsonElement1.EnumerateObject())
				{
					string memberPath = path + "." + item.Name;
					if (_differentMembers.ContainsKey(memberPath))
					{
						continue;
					}

					if (!jsonElement2.TryGetProperty(item.Name, out JsonElement property))
					{
						_additionalMembers.Add(memberPath);
						isConsideredEqual = false;
						continue;
					}

					isConsideredEqual = CompareJson(memberPath, property, item.Value, options) && isConsideredEqual;
				}
			}

			return isConsideredEqual;
		}

		private bool CompareJsonArray(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.Array)
			{
				_differentMembers[path] = "was no array";
				return false;
			}

			// TODO

			return true;
		}
	}
}
#endif
