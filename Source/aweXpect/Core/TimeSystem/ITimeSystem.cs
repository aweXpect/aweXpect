namespace Testably.Expectations.Core.TimeSystem;

internal interface ITimeSystem
{
	IStopwatchFactory Stopwatch { get; }
}
