using System;

namespace aweXpect.Customization;

/// <summary>
///     Writer for information traces within aweXpect.
/// </summary>
public interface ITraceWriter
{
	/// <summary>
	///     Writes a <paramref name="message" />.
	/// </summary>
	void WriteMessage(string message);

	/// <summary>
	///     Writes an <paramref name="exception" />.
	/// </summary>
	void WriteException(Exception exception);
}
