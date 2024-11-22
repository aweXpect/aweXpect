#if NETSTANDARD2_0
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Testably.Expectations.Polyfills;

/// <summary>
///     Provides extension methods to simplify writing platform independent tests.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class StringExtensionMethods
{
	/// <summary>
	///     Returns a value indicating whether a specified character occurs within this string, using the specified comparison
	///     rules.
	/// </summary>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="value" /> parameter occurs withing this string; otherwise,
	///     <see langword="false" />.
	/// </returns>
	internal static bool Contains(
		this string @this,
		char value,
		StringComparison comparisonType)
	{
		return @this.Contains(value);
	}

	/// <summary>
	///     Returns a value indicating whether a specified character occurs within this string, using the specified comparison
	///     rules.
	/// </summary>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="value" /> parameter occurs withing this string; otherwise,
	///     <see langword="false" />.
	/// </returns>
	internal static bool Contains(
		this string @this,
		string value,
		StringComparison comparisonType)
	{
		return @this.Contains(value);
	}

	/// <summary>
	///     Determines whether the end of this string instance matches the specified character.
	/// </summary>
	internal static bool EndsWith(
		this string @this,
		char value)
	{
		return @this.EndsWith($"{value}");
	}

	/// <summary>
	///     Reports the zero-based index of the first occurrence of the specified Unicode character in this string. A parameter
	///     specifies the type of search to use for the specified character.
	/// </summary>
	/// <returns>
	///     The zero-based index of <paramref name="value" /> if that character is found, or <c>-1</c> if it is not.
	/// </returns>
	internal static int IndexOf(
		this string @this,
		char value,
		StringComparison comparisonType)
	{
		return @this.IndexOf($"{value}", comparisonType);
	}

	/// <summary>
	///     Determines whether this string instance starts with the specified character.
	/// </summary>
	internal static bool StartsWith(
		this string @this,
		char value)
	{
		return @this.StartsWith($"{value}");
	}
}
#endif
