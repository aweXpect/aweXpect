namespace aweXpect.Formatting;

/// <summary>
///     Formatting options used in the <see cref="ValueFormatter" />.
/// </summary>
public class FormattingOptions
{
	private FormattingOptions(bool useLineBreaks, string indentation = "")
	{
		UseLineBreaks = useLineBreaks;
		Indentation = indentation;
	}

	/// <summary>
	///     Format the objects on multiple lines.
	/// </summary>
	public static FormattingOptions MultipleLines { get; } = new(true);

	/// <summary>
	///     Format the objects on multiple lines with an indentation of 2 blanks.
	/// </summary>
	public static FormattingOptions Indented { get; } = new(true, "  ");

	/// <summary>
	///     Format the objects on a single line.
	/// </summary>
	public static FormattingOptions SingleLine { get; } = new(false);

	internal bool UseLineBreaks { get; }
	
	internal string Indentation { get; }
}
