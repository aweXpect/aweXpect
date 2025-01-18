using aweXpect.Recording;

namespace aweXpect.Core.Tests.Recording;

public sealed class EventRecordingTests
{
	[Fact]
	public async Task MissingEventName_ShouldThrowNotSupportedException()
	{
		CustomEventClass sut = new();

		void Act()
			=> sut.Record().Events("someMissingEventName");

		await That(Act).Throws<NotSupportedException>()
			.WithMessage("Event someMissingEventName is not supported on CustomEventClass { }");
	}

	[Fact]
	public async Task WhenStopIsCalled_ShouldStopListening()
	{
		CustomEventClass subject = new();

		IEventRecording<CustomEventClass> recording = subject.Record().Events();
		subject.NotifyCustomEvent(1);
		subject.NotifyCustomEvent(2);

		IEventRecordingResult result = recording.Stop();

		subject.NotifyCustomEvent(3);

		await That(result.GetEventCount(nameof(CustomEventClass.CustomEvent), _ => true)).Is(2);
	}

	[Fact]
	public async Task WhenStopIsCalledTwice_ShouldNotThrowAnyException()
	{
		CustomEventClass subject = new();

		IEventRecording<CustomEventClass> recording = subject.Record().Events();
		recording.Stop();

		await That(() => recording.Stop()).DoesNotThrow();
	}

	private sealed class CustomEventClass
	{
		public delegate void CustomEventDelegate(int arg1);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(int arg1)
			=> CustomEvent?.Invoke(arg1);
	}
}
