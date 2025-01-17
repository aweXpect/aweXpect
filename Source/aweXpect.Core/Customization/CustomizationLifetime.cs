using System;

namespace aweXpect.Customization;

/// <summary>
///     The lifetime of a customization setting.
/// </summary>
public sealed class CustomizationLifetime(Action callback) : IDisposable
{
	/// <inheritdoc cref="IDisposable.Dispose()" />
	public void Dispose() => callback();
}