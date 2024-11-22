using System.Net;
using System.Text;

namespace Testably.Expectations.Formatting;

public static partial class ValueFormatters
{
	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
	/// </summary>
	public static string Format(
		this ValueFormatter _,
		HttpStatusCode? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			return ValueFormatter.NullString;
		}

		return $"{(int)value} {value}";
	}

	/// <summary>
	///     Appends the according to the <paramref name="options" /> formatted <paramref name="value" />
	///     to the <paramref name="stringBuilder" />
	/// </summary>
	public static void Format(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		HttpStatusCode? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			stringBuilder.Append(ValueFormatter.NullString);
			return;
		}

		stringBuilder.Append((int)value).Append(' ').Append(value);
	}
}
