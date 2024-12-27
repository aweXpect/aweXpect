using aweXpect.Events;

namespace aweXpect.Core.Tests.Events;

public sealed class EventRecordingTests
{
	[Fact]
	public async Task MissingEventName_ShouldThrowNotSupportedException()
	{
		CustomEventClass sut = new();

		void Act()
			=> sut.Record().Events("someMissingEventName");

		await That(Act).Should().Throw<NotSupportedException>()
			.WithMessage("Event someMissingEventName is not supported on CustomEventClass { }");
	}

	[Fact]
	public async Task ShouldStopListeningOnDispose()
	{
		CustomEventClass subject = new();

		IRecording<CustomEventClass> recording = subject.Record().Events();
		subject.NotifyCustomEvent(1);
		subject.NotifyCustomEvent(2);

		recording.Dispose();

		subject.NotifyCustomEvent(3);

		await That(recording.GetEventCount(nameof(CustomEventClass.CustomEvent), _ => true)).Should().Be(2);
	}

	private sealed class CustomEventClass
	{
		public delegate void CustomEventDelegate(int arg1);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(int arg1)
			=> CustomEvent?.Invoke(arg1);
	}
}
