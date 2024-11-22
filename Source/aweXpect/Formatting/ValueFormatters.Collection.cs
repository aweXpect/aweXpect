using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Testably.Expectations.Formatting;

/// <summary>
///     Extension formatting options.
/// </summary>
public static partial class ValueFormatters
{
	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
	/// </summary>
	public static string Format<T>(
		this ValueFormatter formatter,
		IEnumerable<T> value,
		FormattingOptions? options = null)
	{
		StringBuilder stringBuilder = new();
		Format(formatter, stringBuilder, (IEnumerable)value, options);
		return stringBuilder.ToString();
	}

	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
	/// </summary>
	public static string Format(
		this ValueFormatter formatter,
		IEnumerable value,
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
		IEnumerable value,
		FormattingOptions? options = null)
	{
		int maxCount = 10;
		int count = maxCount;
		stringBuilder.Append('[');
		bool hasMoreValues = false;
		foreach (object? v in value)
		{
			if (count < maxCount)
			{
				stringBuilder.Append(", ");
			}

			if (count-- <= 0)
			{
				hasMoreValues = true;
				break;
			}

			stringBuilder.Append(Format(formatter, v, options));
		}

		if (hasMoreValues)
		{
			const char ellipsis = '\u2026';
			stringBuilder.Append(ellipsis);
		}

		stringBuilder.Append(']');
	}

	/// <summary>
	///     Appends the according to the <paramref name="options" /> formatted <paramref name="value" />
	///     to the <paramref name="stringBuilder" />
	/// </summary>
	public static void Format<T>(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		IEnumerable<T> value,
		FormattingOptions? options = null)
		=> Format(formatter, stringBuilder, (IEnumerable)value, options);
}
