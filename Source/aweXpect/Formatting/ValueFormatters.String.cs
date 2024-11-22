using System.Text;
using Testably.Expectations.Core.Helpers;

namespace Testably.Expectations.Formatting;

public static partial class ValueFormatters
{
	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
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
	///     Appends the according to the <paramref name="options" /> formatted <paramref name="value" />
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
