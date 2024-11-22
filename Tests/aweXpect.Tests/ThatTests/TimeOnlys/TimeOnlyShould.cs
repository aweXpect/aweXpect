#if NET6_0_OR_GREATER
namespace Testably.Expectations.Tests.ThatTests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	/// <summary>
	///     Use a fixed random time in each test run to ensure, that the tests don't rely on special times.
	/// </summary>
	private static readonly Lazy<TimeOnly> CurrentTimeLazy = new(
		() => TimeOnly.MinValue.Add(TimeSpan.FromSeconds(new Random().Next(100, 86300))));

	private static TimeOnly CurrentTime()
		=> CurrentTimeLazy.Value;

	private static TimeOnly EarlierTime(int seconds = 1)
		=> CurrentTime().Add(TimeSpan.FromSeconds(-1 * seconds));

	private static TimeOnly LaterTime(int seconds = 1)
		=> CurrentTime().Add(TimeSpan.FromSeconds(seconds));
}
#endif
