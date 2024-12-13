using System;

namespace aweXpect.Customization;

/// <summary>
///     Allows customizing the static behaviour of aweXpect.
/// </summary>
public partial class Customize
{
	private Customize() { }

	/// <summary>
	///     The current customization values.
	/// </summary>
	public static Customize Instance { get; } = new();

	private class ActionDisposable(Action callback) : IDisposable
	{
		public void Dispose() => callback();
	}
}
