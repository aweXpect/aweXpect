namespace Testably.Expectations.Tests.ThatTests.TimeSpans;

public sealed partial class TimeSpanShould
{
	/// <summary>
	///     Use a fixed random time in each test run to ensure, that the tests don't rely on special times.
	/// </summary>
	private static readonly Lazy<TimeSpan> CurrentTimeLazy = new(
		() => TimeSpan.FromSeconds(new Random().Next(100, 100000)));

	private static TimeSpan CurrentTime()
		=> CurrentTimeLazy.Value;

	private static TimeSpan EarlierTime(int seconds = 1)
		=> CurrentTime() + TimeSpan.FromSeconds(-1 * seconds);

	private static TimeSpan LaterTime(int seconds = 1)
		=> CurrentTime() + TimeSpan.FromSeconds(seconds);
}
