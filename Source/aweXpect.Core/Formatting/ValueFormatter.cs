using System;
using System.Collections.Concurrent;
using System.Threading;
using aweXpect.Customization;

namespace aweXpect.Formatting;

/// <summary>
///     Formatter for arbitrary objects in exception messages.
/// </summary>
public class ValueFormatter
{
	internal static readonly ConcurrentDictionary<int, IValueFormatter> RegisteredValueFormatters = new();
	private static int _index;

	/// <summary>
	///     The default string representation of <see langword="null" />.
	/// </summary>
	public static readonly string NullString = "<null>";

#pragma warning disable S1118 // Utility classes should not have public constructors
	internal ValueFormatter() { }
#pragma warning restore S1118

	/// <summary>
	///     Registers a custom <paramref name="formatter" /> to use for formatting <see cref="object" />s.
	/// </summary>
	public static IDisposable Register(IValueFormatter formatter)
	{
		int index = Interlocked.Increment(ref _index);
		RegisteredValueFormatters.TryAdd(index, formatter);
		CustomizationLifetime disposable = new(() => RegisteredValueFormatters.TryRemove(index, out _));
		return disposable;
	}
}
