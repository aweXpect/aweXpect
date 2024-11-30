using aweXpect.Core.Ambient;

namespace aweXpect.Formatting;

/// <summary>
///     Static class to provide the <see cref="ValueFormatter" />.
/// </summary>
public static class Format
{
	/// <summary>
	///     The number of collection items that are formatted.
	/// </summary>
	public static int CollectionFormatCount { get; } = 10;

	/// <summary>
	///     The formatter to use for formatting values.
	/// </summary>
	public static ValueFormatter Formatter { get; } = Initialization.State.Value.Formatter;
}
