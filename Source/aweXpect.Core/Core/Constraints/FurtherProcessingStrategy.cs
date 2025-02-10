namespace aweXpect.Core.Constraints;

/// <summary>
///     The strategy how to continue processing after the current result.
/// </summary>
public enum FurtherProcessingStrategy
{
	/// <summary>
	///     Continue processing.
	/// </summary>
	/// <remarks>
	///     This is the default behaviour.
	/// </remarks>
	Continue,

	/// <summary>
	///     Completely ignore all future constraints.
	/// </summary>
	IgnoreCompletely,

	/// <summary>
	///     Ignore the result of future constraints, but include their expectations.
	/// </summary>
	IgnoreResult,
}
