namespace aweXpect.Core;

/// <summary>
///     Count the number of times something occurred.
/// </summary>
public readonly struct Times(int value)
{
	/// <summary>
	///     The number of times something occurred.
	/// </summary>
	public int Value { get; } = value;

	/// <summary>
	///     Implicitly convert the <paramref name="value" /> to a <see cref="Times" /> instance.
	/// </summary>
	public static implicit operator Times(int value)
	{
		return new Times(value);
	}

	/// <summary>
	///     Implicitly convert the <paramref name="value" /> to an <see langword="int" />.
	/// </summary>
	public static implicit operator int(Times value)
	{
		return value.Value;
	}
}
