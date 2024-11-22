namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class DateTimeShould
{
	/// <summary>
	///     Use a fixed random time in each test run to ensure, that the tests don't rely on special times.
	/// </summary>
	private static readonly Lazy<DateTime> CurrentTimeLazy = new(
		() => DateTime.MinValue.AddSeconds(new Random().Next(100, 100000)));

	private static DateTime CurrentTime()
		=> CurrentTimeLazy.Value;

	private static DateTime EarlierTime(int seconds = 1)
		=> CurrentTime().AddSeconds(-1 * seconds);

	private static DateTime LaterTime(int seconds = 1)
		=> CurrentTime().AddSeconds(seconds);
}
