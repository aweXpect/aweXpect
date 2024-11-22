using System;
using System.Text;

namespace Testably.Expectations.Formatting;

public static partial class ValueFormatters
{
	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
	/// </summary>
	public static string Format(
		this ValueFormatter formatter,
		TimeSpan value,
		FormattingOptions? options = null)
	{
		StringBuilder stringBuilder = new();
		Format(formatter, stringBuilder, value, options);
		return stringBuilder.ToString();
	}

	/// <summary>
	///     Appends the according to the <paramref name="options" /> formatted <paramref name="value" />
	///     to the <paramref name="stringBuilder" />
	/// </summary>
	public static void Format(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		TimeSpan value,
		FormattingOptions? options = null)
		=> Format(formatter, stringBuilder, (TimeSpan?)value, options);

	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
	/// </summary>
	public static string Format(
		this ValueFormatter formatter,
		TimeSpan? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			return ValueFormatter.NullString;
		}

		return Format(formatter, value.Value, options);
	}

	/// <summary>
	///     Appends the according to the <paramref name="options" /> formatted <paramref name="value" />
	///     to the <paramref name="stringBuilder" />
	/// </summary>
	public static void Format(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		TimeSpan? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			stringBuilder.Append(ValueFormatter.NullString);
			return;
		}

		if (value == TimeSpan.MaxValue)
		{
			stringBuilder.Append("the maximum time span");
			return;
		}

		if (value == TimeSpan.MinValue)
		{
			stringBuilder.Append("the minimum time span");
			return;
		}

		TimeSpan absoluteValue = value.Value.Duration();
		bool hasDays = absoluteValue.Days > 0;
		bool hasHours = absoluteValue.Hours > 0;

		if (hasDays)
		{
			stringBuilder.Append(absoluteValue.Days);
			stringBuilder.Append('.');
		}

		if (hasDays || hasHours)
		{
			if (hasDays && absoluteValue.Hours < 10)
			{
				stringBuilder.Append('0');
			}

			stringBuilder.Append(absoluteValue.Hours);
			stringBuilder.Append(':');
		}

		if ((hasDays || hasHours) && absoluteValue.Minutes < 10)
		{
			stringBuilder.Append('0');
		}

		stringBuilder.Append(absoluteValue.Minutes);
		stringBuilder.Append(':');

		if (absoluteValue.Seconds < 10)
		{
			stringBuilder.Append('0');
		}

		stringBuilder.Append(absoluteValue.Seconds);

		if (absoluteValue.Milliseconds > 0)
		{
			stringBuilder.Append('.');
			stringBuilder.Append(absoluteValue.Milliseconds.ToString("000"));
		}
	}
}
