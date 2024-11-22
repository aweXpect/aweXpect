namespace Testably.Expectations.Formatting;

/// <summary>
///     Formatting options used in the <see cref="ValueFormatter" />.
/// </summary>
public class FormattingOptions
{
	/// <summary>
	///     Format the objects on multiple lines.
	/// </summary>
	public static FormattingOptions MultipleLines { get; } = new(true);

	/// <summary>
	///     Format the objects on a single line.
	/// </summary>
	public static FormattingOptions SingleLine { get; } = new(false);

	internal bool UseLineBreaks { get; }

	private FormattingOptions(bool useLineBreaks)
	{
		UseLineBreaks = useLineBreaks;
	}
}
