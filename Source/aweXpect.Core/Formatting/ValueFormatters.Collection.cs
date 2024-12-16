using System.Collections;
using System.Collections.Generic;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Formatting;

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
		IEnumerable<T>? value,
		FormattingOptions? options = null)
	{
		StringBuilder stringBuilder = new();
		Format(formatter, stringBuilder, (IEnumerable?)value, options);
		return stringBuilder.ToString();
	}

	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
	/// </summary>
	public static string Format(
		this ValueFormatter formatter,
		IEnumerable? value,
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
		IEnumerable? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			stringBuilder.Append(ValueFormatter.NullString);
			return;
		}

		options ??= FormattingOptions.SingleLine;
		int maxCount = Customization.Customize.Formatting.MaximumNumberOfCollectionItems;
		int count = maxCount;
		stringBuilder.Append('[');
		bool hasMoreValues = false;
		bool isNotEmpty = false;
		foreach (object? v in value)
		{
			isNotEmpty = true;
			if (count < maxCount)
			{
				if (options.UseLineBreaks)
				{
					stringBuilder.AppendLine(",");
					stringBuilder.Append("  ");
				}
				else
				{
					stringBuilder.Append(", ");
				}
			}
			else if (options.UseLineBreaks)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
			}

			if (count-- <= 0)
			{
				hasMoreValues = true;
				break;
			}

			stringBuilder.Append(Format(formatter, v, options).Indent("  ", false));
		}

		if (hasMoreValues)
		{
			const char ellipsis = '\u2026';
			stringBuilder.Append(ellipsis);
		}

		if (options.UseLineBreaks && isNotEmpty)
		{
			stringBuilder.AppendLine();
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
		IEnumerable<T>? value,
		FormattingOptions? options = null)
		=> Format(formatter, stringBuilder, (IEnumerable?)value, options);
}
