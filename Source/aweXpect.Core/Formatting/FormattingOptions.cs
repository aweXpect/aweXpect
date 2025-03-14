namespace aweXpect.Formatting;

/// <summary>
///     Formatting options used in the <see cref="ValueFormatter" />.
/// </summary>
public record FormattingOptions
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
	///     Format the objects on a single line.
	/// </summary>
	public static FormattingOptions SingleLine { get; } = new(false);

	/// <summary>
	///     Flag indicating, if line-breaks should be used during formatting.
	/// </summary>
	public bool UseLineBreaks { get; }

	/// <summary>
	///     The indentation prefix for subsequent lines when <see cref="UseLineBreaks" /> is <see langword="true" />
	/// </summary>
	public string Indentation { get; }

	/// <summary>
	///     Format the objects on multiple lines with the given <paramref name="indentation" />.
	/// </summary>
	/// <remarks>
	///     Use an indentation of 2 blanks, if the <paramref name="indentation" /> is <see langword="null" />.
	/// </remarks>
	public static FormattingOptions Indented(string? indentation = null) => new(true, indentation ?? "  ");
}
