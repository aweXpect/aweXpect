using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Formatting;

public static partial class ValueFormatters
{
	/// <summary>
	///     Returns the formatted <paramref name="value" /> according to the <paramref name="options" />.
	/// </summary>
	public static string Format(
		this ValueFormatter _,
		string? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			return ValueFormatter.NullString;
		}

		options ??= FormattingOptions.SingleLine;
		if (!options.UseLineBreaks)
		{
			return $"\"{value.DisplayWhitespace().TruncateWithEllipsis(100)}\"";
		}

		return $"\"{value}\"";
	}

	/// <summary>
	///     Appends the formatted <paramref name="value" /> according to the <paramref name="options" />
	///     to the <paramref name="stringBuilder" />
	/// </summary>
	public static void Format(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		string? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			stringBuilder.Append(ValueFormatter.NullString);
			return;
		}

		options ??= FormattingOptions.SingleLine;
		stringBuilder.Append('\"');
		if (!options.UseLineBreaks)
		{
			stringBuilder.Append(value.DisplayWhitespace().TruncateWithEllipsis(100));
		}
		else
		{
			stringBuilder.Append(value);
		}

		stringBuilder.Append('\"');
	}
}
