namespace aweXpect.Formatting;

/// <summary>
///     Formatter for arbitrary objects in exception messages.
/// </summary>
public class ValueFormatter
{
	/// <summary>
	///     The default string representation of <see langword="null" />.
	/// </summary>
	public static readonly string NullString = "<null>";

#pragma warning disable S1118 // Utility classes should not have public constructors
	internal ValueFormatter() { }
#pragma warning restore S1118
}
