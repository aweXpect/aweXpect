using System.Collections;
using System.Collections.Generic;
using System.Text;
using aweXpect.Core.Helpers;
using aweXpect.Customization;

namespace aweXpect.Formatting;

/// <summary>
///     Extension formatting options.
/// </summary>
public static partial class ValueFormatters
{
	/// <summary>
	///     Returns the formatted <paramref name="value" /> according to the <paramref name="options" />.
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
	///     Appends the formatted <paramref name="value" /> according to the <paramref name="options" />
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
		if (options.IncludeType)
		{
			Format(Formatter, stringBuilder, value.GetType());
			stringBuilder.Append(' ');
		}

		int maxCount = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
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

			stringBuilder.Append(Format(formatter, v, options with
			{
				IncludeType = false,
			}).Indent("  ", false));
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
	///     Appends the formatted <paramref name="value" /> according to the <paramref name="options" />
	///     to the <paramref name="stringBuilder" />
	/// </summary>
	public static void Format<T>(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		IEnumerable<T>? value,
		FormattingOptions? options = null)
		=> Format(formatter, stringBuilder, (IEnumerable?)value, options);
}
