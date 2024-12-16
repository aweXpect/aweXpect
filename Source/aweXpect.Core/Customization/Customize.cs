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
	private static readonly Customize Instance = new();

	private sealed class ActionDisposable(Action callback) : IDisposable
	{
		public void Dispose() => callback();
	}
}
