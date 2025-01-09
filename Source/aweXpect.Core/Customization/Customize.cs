namespace aweXpect.Customization;

/// <summary>
///     Allows customizing the global behaviour of aweXpect.
/// </summary>
public partial class Customize
{
	/// <summary>
	///     Customize the global behaviour of aweXpect.
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public static GlobalCustomization aweXpect { get; } = new();
}
