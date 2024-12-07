namespace aweXpect;

/// <summary>
///     Extension methods for <see cref="Times" />.
/// </summary>
public static class TimesExtensions
{
	/// <summary>
	///     Specifies the number of times something occurs.
	/// </summary>
	public static Times Times(this int value)
		=> new(value);
}
