using System;
using System.Collections;
using System.Net;
using System.Text;

namespace Testably.Expectations.Formatting;

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
		FormattingOptions? options = null)
	{
		if (value is null)
		{
			return ValueFormatter.NullString;
		}

		StringBuilder stringBuilder = new();
		Format(formatter, stringBuilder, value, options);
		return stringBuilder.ToString();
	}

	/// <summary>
	///     Fallback for formatting arbitrary objects.
	/// </summary>
	public static void Format(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		object? value,
		FormattingOptions? options = null)
	{
		if (value is null)
		{
			stringBuilder.Append(ValueFormatter.NullString);
		}
		else if (value is bool boolValue)
		{
			formatter.Format(stringBuilder, boolValue, options);
		}
		else if (value is string stringValue)
		{
			formatter.Format(stringBuilder, stringValue, options);
		}
		else if (value is Type typeValue)
		{
			formatter.Format(stringBuilder, typeValue, options);
		}
		else if (value is IEnumerable enumerableValue)
		{
			formatter.Format(stringBuilder, enumerableValue, options);
		}
		else if (value is HttpStatusCode httpStatusCodeValue)
		{
			formatter.Format(stringBuilder, httpStatusCodeValue, options);
		}
		else if (value is DateTime dateTimeValue)
		{
			formatter.Format(stringBuilder, dateTimeValue, options);
		}
		else if (value is DateTimeOffset dateTimeOffsetValue)
		{
			formatter.Format(stringBuilder, dateTimeOffsetValue, options);
		}
		else if (value is TimeSpan timeSpanValue)
		{
			formatter.Format(stringBuilder, timeSpanValue, options);
		}
#if NET6_0_OR_GREATER
		else if (value is DateOnly dateOnlyValue)
		{
			formatter.Format(stringBuilder, dateOnlyValue, options);
		}
		else if (value is TimeOnly timeOnlyValue)
		{
			formatter.Format(stringBuilder, timeOnlyValue, options);
		}
#endif
		else if (value is Guid guidValue)
		{
			formatter.Format(stringBuilder, guidValue, options);
		}
		else if (value is Enum enumValue)
		{
			formatter.Format(stringBuilder, enumValue, options);
		}
		else if (value is double doubleValue)
		{
			formatter.Format(stringBuilder, doubleValue, options);
		}
		else if (value is float floatValue)
		{
			formatter.Format(stringBuilder, floatValue, options);
		}
		else if (value is decimal decimalValue)
		{
			formatter.Format(stringBuilder, decimalValue, options);
		}
		else if (value is int intValue)
		{
			formatter.Format(stringBuilder, intValue, options);
		}
		else if (value is uint uintValue)
		{
			formatter.Format(stringBuilder, uintValue, options);
		}
		else if (value is long longValue)
		{
			formatter.Format(stringBuilder, longValue, options);
		}
		else if (value is ulong ulongValue)
		{
			formatter.Format(stringBuilder, ulongValue, options);
		}
		else if (value is byte byteValue)
		{
			formatter.Format(stringBuilder, byteValue, options);
		}
		else if (value is sbyte sbyteValue)
		{
			formatter.Format(stringBuilder, sbyteValue, options);
		}
		else if (value is short shortValue)
		{
			formatter.Format(stringBuilder, shortValue, options);
		}
		else if (value is ushort ushortValue)
		{
			formatter.Format(stringBuilder, ushortValue, options);
		}
		else if (value is nint nintValue)
		{
			formatter.Format(stringBuilder, nintValue, options);
		}
		else if (value is nuint nuintValue)
		{
			formatter.Format(stringBuilder, nuintValue, options);
		}
		else
		{
			formatter.FormatObject(stringBuilder, value,
				options ?? FormattingOptions.MultipleLines);
		}
	}
}
