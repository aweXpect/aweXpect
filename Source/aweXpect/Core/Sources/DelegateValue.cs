using System;

namespace Testably.Expectations.Core.Sources;

/// <summary>
///     An expectation source from a delegate can contain a <see cref="Value" /> or a thrown <see cref="Exception" />.
/// </summary>
internal class DelegateValue<TValue>(in TValue? value, Exception? exception, TimeSpan duration)
	: DelegateValue(exception, duration)
{
	/// <summary>
	///     The value of the delegate, if no exception was thrown.
	/// </summary>
	public TValue? Value { get; } = value;
}

/// <summary>
///     An expectation source from a delegate without value can represent <see langword="void" /> or a thrown
///     <see cref="Exception" />.
/// </summary>
internal class DelegateValue(Exception? exception, TimeSpan duration)
{
	/// <summary>
	///     The duration it took the delegate to complete.
	/// </summary>
	public TimeSpan Duration { get; } = duration;

	/// <summary>
	///     The thrown exception of the delegate.
	/// </summary>
	public Exception? Exception { get; } = exception;
}
