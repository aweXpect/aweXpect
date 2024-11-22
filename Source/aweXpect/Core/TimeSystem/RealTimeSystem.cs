using System;
using System.Diagnostics;

namespace Testably.Expectations.Core.TimeSystem;

internal class RealTimeSystem : ITimeSystem
{
	public static ITimeSystem Instance { get; } = new RealTimeSystem();

	#region ITimeSystem Members

	/// <inheritdoc />
	public IStopwatchFactory Stopwatch { get; } = new RealStopwatchFactory();

	#endregion

	private sealed class RealStopwatchFactory : IStopwatchFactory
	{
		#region IStopwatchFactory Members

		/// <inheritdoc />
		public IStopwatch New()
			=> new RealStopwatch();

		#endregion
	}

	private sealed class RealStopwatch : IStopwatch
	{
		private readonly Stopwatch _stopwatch = new();

		#region IStopwatch Members

		/// <inheritdoc />
		public TimeSpan Elapsed => _stopwatch.Elapsed;

		/// <inheritdoc />
		public bool IsRunning => _stopwatch.IsRunning;

		/// <inheritdoc />
		public void Start()
			=> _stopwatch.Start();

		/// <inheritdoc />
		public void Stop()
			=> _stopwatch.Stop();

		#endregion
	}
}
