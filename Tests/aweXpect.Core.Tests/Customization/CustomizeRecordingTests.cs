using aweXpect.Chronology;
using aweXpect.Customization;
using aweXpect.Recording;

namespace aweXpect.Core.Tests.Customization;

public sealed class CustomizeRecordingTests
{
	[Fact]
	public async Task MaximumNumberOfCollectionItems_ShouldBeUsedInFormatter()
	{
		SignalCounter signalCounter = new();
		await That(Customize.Recording.DefaultTimeout.TotalMilliseconds).Should().Be(30000);
		using (IDisposable __ = Customize.Recording.SetDefaultTimeout(TimeSpan.FromMilliseconds(10)))
		{
			_ = Task.Delay(1000.Milliseconds()).ContinueWith(_ => signalCounter.Signal());
			SignalCounterResult result = signalCounter.Wait();
			await That(result.IsSuccess).Should().BeFalse();
			await That(Customize.Recording.DefaultTimeout.TotalMilliseconds).Should().Be(10);
		}

		{
			_ = Task.Delay(200.Milliseconds()).ContinueWith(_ => signalCounter.Signal());
			SignalCounterResult result = signalCounter.Wait();
			await That(result.IsSuccess).Should().BeTrue();
			await That(Customize.Recording.DefaultTimeout.TotalMilliseconds).Should().Be(30000);
		}
	}
}
