using System;
using System.Text;

namespace aweXpect.Formatting;

public static partial class ValueFormatters
{
	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
	/// </summary>
	public static string Format(
		this ValueFormatter _,
		Guid value,
		FormattingOptions? options = null)
		=> value.ToString();

	/// <summary>
	///     Appends the according to the <paramref name="options" /> formatted <paramref name="value" />
	///     to the <paramref name="stringBuilder" />
	/// </summary>
	public static void Format(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		Guid value,
		FormattingOptions? options = null)
		=> stringBuilder.Append(value);

	/// <summary>
	///     Returns the according to the <paramref name="options" /> formatted <paramref name="value" />.
	/// </summary>
	public static string Format(
		this ValueFormatter formatter,
		Guid? value,
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
		Guid? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			stringBuilder.Append(ValueFormatter.NullString);
			return;
		}

		Format(formatter, stringBuilder, value.Value, options);
	}
}
