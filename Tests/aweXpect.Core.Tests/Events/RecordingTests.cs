using aweXpect.Events;

namespace aweXpect.Core.Tests.Events;

public class RecordingTests
{
	
	[Fact]
	public async Task MissingEventName_ShouldThrowNotSupportedException()
	{
		CustomEventClass sut = new();

		using IRecording<CustomEventClass> recording = sut.Record().Events();

		sut.NotifyCustomEvent(1);

		await That(recording).Should().HaveTriggered(nameof(CustomEventClass.CustomEvent));
	}

	private sealed class CustomEventClass
	{
		public delegate void CustomEventDelegate(int arg1);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(int arg1)
			=> CustomEvent?.Invoke(arg1);
	}
}
