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
		options.Using(new JsonComparer(equivalencyOptions));
		return (TSelf)options;
	}

	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public static StringEqualityOptions AsJson(this StringEqualityOptions options,
		JsonComparerOptions equivalencyOptions)
	{
		options.UsingComparer(new JsonComparer(equivalencyOptions));
		return options;
	}

	private sealed class JsonComparer(JsonComparerOptions equivalencyOptions)
		: IEqualityComparer<string>, IComparerOptions
	{
		private readonly List<string> _additionalProperties = new();
		private readonly List<(string, string)> _differentProperties = new();
		private readonly List<string> _missingProperties = new();

		public string GetExpectation(string expectedExpression) => $"be JSON equivalent to {expectedExpression}";

		public string GetExtendedFailure(string it, object? actual, object? expected)
		{
			StringBuilder sb = new();
			sb.Append(it).Append(' ');
			if (_missingProperties.Any())
			{
				sb.Append("did not contain ");
				Formatter.Format(sb, _missingProperties, FormattingOptions.MultipleLines);
			}

			if (_additionalProperties.Any())
			{
				if (_missingProperties.Any())
				{
					sb.AppendLine(" and");
				}

				sb.Append("contained unexpected properties ");
				Formatter.Format(sb, _additionalProperties, FormattingOptions.MultipleLines);
			}

			if (_differentProperties.Any())
			{
				if (_missingProperties.Any() || _additionalProperties.Any())
				{
					sb.AppendLine(" and");
				}

				sb.Append("did not match the following properties ");
				Formatter.Format(sb, _differentProperties.Select(x => $"{x.Item1}: {x.Item2}"),
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

			try
			{
				using JsonDocument json1 = JsonDocument.Parse(x);
				using JsonDocument json2 = JsonDocument.Parse(y);

				return CompareJson("",
					json1.RootElement,
					json2.RootElement,
					equivalencyOptions);
			}
			catch (JsonException e)
			{
				Console.WriteLine(e);
				throw;
			}
			//return !_missingProperties.Any() && !_additionalProperties.Any() && !_differentProperties.Any();
		}

		public int GetHashCode(string obj) => obj.GetHashCode();

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
				_differentProperties.Add((path, "was false"));
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
				_differentProperties.Add((path, "was true"));
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
				_differentProperties.Add((path, "was no number"));
				return false;
			}

			if (jsonElement1.TryGetInt32(out var v1) && jsonElement2.TryGetInt32(out var v2) && v1 == v2)
			{
				return true;
			}

			_differentProperties.Add((path, "differed"));
			return false;
		}

		private bool CompareJsonString(string path,
			JsonElement jsonElement1,
			JsonElement jsonElement2,
			JsonComparerOptions options)
		{
			if (jsonElement2.ValueKind != JsonValueKind.String)
			{
				_differentProperties.Add((path, "was no string"));
				return false;
			}

			string? value1 = jsonElement1.GetString();
			string? value2 = jsonElement2.GetString();
			if (value1 != value2)
			{
				_differentProperties.Add((path, $"was {Formatter.Format(value1)} instead of {Formatter.Format(value2)}"));
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
				_differentProperties.Add((path, "was not null"));
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
				_differentProperties.Add((path, "was not undefined"));
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
				_differentProperties.Add((path, "was no object"));
				return false;
			}

			bool isConsideredEqual = true;
			foreach (JsonProperty item in jsonElement2.EnumerateObject())
			{
				string memberPath = path + "." + item.Name;
				if (!jsonElement1.TryGetProperty(item.Name, out JsonElement property))
				{
					_missingProperties.Add(memberPath);
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
					if (!jsonElement2.TryGetProperty(item.Name, out JsonElement property))
					{
						_additionalProperties.Add(memberPath);
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
				_differentProperties.Add((path, "was no array"));
				return false;
			}
			
			// TODO

			return true;
		}
	}
}
#endif
