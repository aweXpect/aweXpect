using aweXpect.Core.Events;

namespace aweXpect.Core.Tests.Core.Events;

public sealed class EventRecordingTests
{
	[Fact]
	public async Task MissingEventName_ShouldThrowNotSupportedException()
	{
		CustomEventClass sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers("someMissingEventName")
				.While(t =>
				{
					t.NotifyCustomEvent(1);
				});

		await That(Act).Should().Throw<NotSupportedException>()
			.WithMessage("Event someMissingEventName is not supported on CustomEventWithoutParametersClass { }");
	}

	[Fact]
	public async Task ShouldStopListeningOnDispose()
	{
		CustomEventClass subject = new();
		EventRecording<CustomEventClass> sut = new(subject, [nameof(CustomEventClass.CustomEvent)]);
		subject.NotifyCustomEvent(1);
		subject.NotifyCustomEvent(2);

		sut.Dispose();

		subject.NotifyCustomEvent(3);
		await That(sut.GetEventCount(nameof(CustomEventClass.CustomEvent), null)).Should().Be(2);
	}

	private sealed class CustomEventClass
	{
		public delegate void CustomEventDelegate(int arg1);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(int arg1)
			=> CustomEvent?.Invoke(arg1);
	}
}
