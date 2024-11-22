using System;
using System.Diagnostics;

namespace Testably.Expectations.Core.TimeSystem;

internal interface IStopwatch
{
	/// <summary>
	///     Gets the total elapsed time measured by the current instance.
	/// </summary>
	/// <remarks>Wrapper around <see cref="Stopwatch.Elapsed" /></remarks>
	TimeSpan Elapsed { get; }

	/// <summary>
	///     Gets a value indicating whether the <see cref="IStopwatch" /> timer is running.
	/// </summary>
	/// <remarks>Wrapper around <see cref="Stopwatch.IsRunning" /></remarks>
	bool IsRunning { get; }

	/// <summary>
	///     Starts, or resumes, measuring elapsed time for an interval.
	/// </summary>
	/// <remarks>Wrapper around <see cref="Stopwatch.Start" /></remarks>
	void Start();

	/// <summary>
	///     Stops measuring elapsed time for an interval.
	/// </summary>
	/// <remarks>Wrapper around <see cref="Stopwatch.Stop()" /></remarks>
	void Stop();
}
