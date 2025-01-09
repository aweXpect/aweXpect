using aweXpect.Chronology;
using aweXpect.Customization;
using aweXpect.Recording;
using aweXpect.Signaling;

namespace aweXpect.Core.Tests.Customization;

public sealed class CustomizeRecordingTests
{
	[Fact]
	public async Task MaximumNumberOfCollectionItems_ShouldBeUsedInFormatter()
	{
		Signaler signaler = new();
		await That(Customize.aweXpect.Recording().DefaultTimeout.Get()).Should().Be(30000.Milliseconds());
		using (IDisposable __ = Customize.aweXpect.Recording().DefaultTimeout.Set(10.Milliseconds()))
		{
			_ = Task.Delay(1000.Milliseconds()).ContinueWith(_ => signaler.Signal());
			SignalerResult result = signaler.Wait();
			await That(result.IsSuccess).Should().BeFalse();
			await That(Customize.aweXpect.Recording().DefaultTimeout.Get()).Should().Be(10.Milliseconds());
		}

		{
			_ = Task.Delay(200.Milliseconds()).ContinueWith(_ => signaler.Signal());
			SignalerResult result = signaler.Wait();
			await That(result.IsSuccess).Should().BeTrue();
			await That(Customize.aweXpect.Recording().DefaultTimeout.Get()).Should().Be(30000.Milliseconds());
		}
	}
}
