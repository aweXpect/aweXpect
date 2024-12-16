using System.Collections.Generic;

namespace aweXpect.Formatting;

/// <summary>
///     Formatting context used in the <see cref="ValueFormatter" />.
/// </summary>
public class FormattingContext
{
	/// <summary>
	///     Tracks already formatted objects to catch recursions.
	/// </summary>
	public HashSet<object> FormattedObjects { get; } = new();
}
