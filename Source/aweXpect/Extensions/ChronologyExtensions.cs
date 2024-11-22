using System;

namespace Testably.Expectations.Extensions;

/// <summary>
///     Extension methods for chronology methods.
/// </summary>
public static class ChronologyExtensions
{
	/// <summary>
	///     Converts the <paramref name="days" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Days(this int days)
		=> TimeSpan.FromDays(days);

	/// <summary>
	///     Converts the <paramref name="days" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Days(this double days)
		=> TimeSpan.FromDays(days);

	/// <summary>
	///     Converts the <paramref name="days" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Days(this int days, TimeSpan offset)
		=> TimeSpan.FromDays(days).Add(offset);

	/// <summary>
	///     Converts the <paramref name="days" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Days(this double days, TimeSpan offset)
		=> TimeSpan.FromDays(days).Add(offset);

	/// <summary>
	///     Converts the <paramref name="hours" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Hours(this int hours)
		=> TimeSpan.FromHours(hours);

	/// <summary>
	///     Converts the <paramref name="hours" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Hours(this double hours)
		=> TimeSpan.FromHours(hours);

	/// <summary>
	///     Converts the <paramref name="hours" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Hours(this int hours, TimeSpan offset)
		=> TimeSpan.FromHours(hours).Add(offset);

	/// <summary>
	///     Converts the <paramref name="hours" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Hours(this double hours, TimeSpan offset)
		=> TimeSpan.FromHours(hours).Add(offset);

	/// <summary>
	///     Converts the <paramref name="milliseconds" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Milliseconds(this int milliseconds)
		=> TimeSpan.FromMilliseconds(milliseconds);

	/// <summary>
	///     Converts the <paramref name="milliseconds" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Milliseconds(this double milliseconds)
		=> TimeSpan.FromMilliseconds(milliseconds);

	/// <summary>
	///     Converts the <paramref name="milliseconds" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Milliseconds(this int milliseconds, TimeSpan offset)
		=> TimeSpan.FromMilliseconds(milliseconds).Add(offset);

	/// <summary>
	///     Converts the <paramref name="milliseconds" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Milliseconds(this double milliseconds, TimeSpan offset)
		=> TimeSpan.FromMilliseconds(milliseconds).Add(offset);

	/// <summary>
	///     Converts the <paramref name="minutes" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Minutes(this int minutes)
		=> TimeSpan.FromMinutes(minutes);

	/// <summary>
	///     Converts the <paramref name="minutes" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Minutes(this double minutes)
		=> TimeSpan.FromMinutes(minutes);

	/// <summary>
	///     Converts the <paramref name="minutes" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Minutes(this int minutes, TimeSpan offset)
		=> TimeSpan.FromMinutes(minutes).Add(offset);

	/// <summary>
	///     Converts the <paramref name="minutes" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Minutes(this double minutes, TimeSpan offset)
		=> TimeSpan.FromMinutes(minutes).Add(offset);

	/// <summary>
	///     Converts the <paramref name="seconds" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Seconds(this int seconds)
		=> TimeSpan.FromSeconds(seconds);

	/// <summary>
	///     Converts the <paramref name="seconds" /> into a corresponding <see cref="TimeSpan" />.
	/// </summary>
	public static TimeSpan Seconds(this double seconds)
		=> TimeSpan.FromSeconds(seconds);

	/// <summary>
	///     Converts the <paramref name="seconds" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Seconds(this int seconds, TimeSpan offset)
		=> TimeSpan.FromSeconds(seconds).Add(offset);

	/// <summary>
	///     Converts the <paramref name="seconds" /> into a corresponding <see cref="TimeSpan" /> and add the given
	///     <paramref name="offset" />.
	/// </summary>
	public static TimeSpan Seconds(this double seconds, TimeSpan offset)
		=> TimeSpan.FromSeconds(seconds).Add(offset);
}
