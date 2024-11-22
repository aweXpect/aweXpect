using Testably.Expectations.Core.Ambient;

namespace Testably.Expectations.Formatting;

/// <summary>
///     Static class to provide the <see cref="ValueFormatter" />.
/// </summary>
public static class Format
{
	/// <summary>
	///     The formatter to use for formatting values.
	/// </summary>
	public static ValueFormatter Formatter { get; } = Initialization.State.Value.Formatter;
}
