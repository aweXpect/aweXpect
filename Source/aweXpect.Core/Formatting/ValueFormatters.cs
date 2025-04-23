using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace aweXpect.Formatting;

/// <summary>
///     Extension formatting options.
/// </summary>
public static partial class ValueFormatters
{
	/// <summary>
	///     Fallback for formatting arbitrary objects.
	/// </summary>
	public static string Format(
		this ValueFormatter formatter,
		object? value,
		FormattingOptions? options = null,
		FormattingContext? context = null)
	{
		StringBuilder stringBuilder = new();
		Format(formatter, stringBuilder, value, options, context);
		return stringBuilder.ToString();
	}

	/// <summary>
	///     Fallback for formatting arbitrary objects.
	/// </summary>
	public static void Format(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		object? value,
		FormattingOptions? options = null,
		FormattingContext? context = null)
	{
		if (value is null)
		{
			stringBuilder.Append(ValueFormatter.NullString);
			return;
		}

		if (!ValueFormatter.RegisteredValueFormatters.IsEmpty &&
		    ValueFormatter.RegisteredValueFormatters.Any(item
			    => item.Value.TryFormat(stringBuilder, value, options)))
		{
			return;
		}

		switch (value)
		{
			case bool boolValue:
				formatter.Format(stringBuilder, boolValue, options);
				return;
			case string stringValue:
				formatter.Format(stringBuilder, stringValue, options);
				return;
			case char charValue:
				formatter.Format(stringBuilder, charValue, options);
				return;
			case Type typeValue:
				formatter.Format(stringBuilder, typeValue, options);
				return;
			case IEnumerable enumerableValue:
				formatter.Format(stringBuilder, enumerableValue, options);
				return;
			case HttpStatusCode httpStatusCodeValue:
				formatter.Format(stringBuilder, httpStatusCodeValue, options);
				return;
			case DateTime dateTimeValue:
				formatter.Format(stringBuilder, dateTimeValue, options);
				return;
			case DateTimeOffset dateTimeOffsetValue:
				formatter.Format(stringBuilder, dateTimeOffsetValue, options);
				return;
			case TimeSpan timeSpanValue:
				formatter.Format(stringBuilder, timeSpanValue, options);
				return;
#if NET8_0_OR_GREATER
			case DateOnly dateOnlyValue:
				formatter.Format(stringBuilder, dateOnlyValue, options);
				return;
			case TimeOnly timeOnlyValue:
				formatter.Format(stringBuilder, timeOnlyValue, options);
				return;
#endif
			case Guid guidValue:
				formatter.Format(stringBuilder, guidValue, options);
				return;
			case Enum enumValue:
				formatter.Format(stringBuilder, enumValue, options);
				return;
			case double doubleValue:
				formatter.Format(stringBuilder, doubleValue, options);
				return;
			case float floatValue:
				formatter.Format(stringBuilder, floatValue, options);
				return;
#if NET8_0_OR_GREATER
			case Half halfValue:
				formatter.Format(stringBuilder, halfValue, options);
				return;
#endif
			case decimal decimalValue:
				formatter.Format(stringBuilder, decimalValue, options);
				return;
			case int intValue:
				formatter.Format(stringBuilder, intValue, options);
				return;
			case uint uintValue:
				formatter.Format(stringBuilder, uintValue, options);
				return;
			case long longValue:
				formatter.Format(stringBuilder, longValue, options);
				return;
			case ulong ulongValue:
				formatter.Format(stringBuilder, ulongValue, options);
				return;
			case byte byteValue:
				formatter.Format(stringBuilder, byteValue, options);
				return;
			case sbyte sbyteValue:
				formatter.Format(stringBuilder, sbyteValue, options);
				return;
			case short shortValue:
				formatter.Format(stringBuilder, shortValue, options);
				return;
			case ushort ushortValue:
				formatter.Format(stringBuilder, ushortValue, options);
				return;
			case nint nintValue:
				formatter.Format(stringBuilder, nintValue, options);
				return;
			case nuint nuintValue:
				formatter.Format(stringBuilder, nuintValue, options);
				return;
		}

		Type? valueType = value.GetType();
		if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
		{
			object? key = valueType.GetProperty("Key")?.GetValue(value);
			object? item = valueType.GetProperty("Value")?.GetValue(value);
			stringBuilder.Append('[');
			Formatter.Format(stringBuilder, key, options);
			stringBuilder.Append(", ");
			Formatter.Format(stringBuilder, item, options);
			stringBuilder.Append(']');
			return;
		}

		FormatObject(stringBuilder, value,
			options ?? FormattingOptions.MultipleLines, context);
	}
}
