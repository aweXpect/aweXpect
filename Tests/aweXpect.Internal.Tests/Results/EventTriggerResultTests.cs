using aweXpect.Recording;
using aweXpect.Results;

namespace aweXpect.Internal.Tests.Results;

public sealed class EventTriggerResultTests
{
	[Fact]
	public async Task WhenCastingToIExtensions_CanSpecifyNameOfParameter()
	{
		CustomEventWithParametersClass<string> sut = new();
		IEventRecording<CustomEventWithParametersClass<string>> recording = sut.Record().Events();

		sut.NotifyCustomEvent("foo");
		sut.NotifyCustomEvent("bar");

		async Task Act() =>
			await ((EventTriggerResult<CustomEventWithParametersClass<string>>.IExtensions)That(recording)
					.Triggered(nameof(CustomEventWithParametersClass<string>.CustomEvent)))
				.WithParameter<string>(" with my parameter", null, s => s == "foo")
				.AtLeast(2.Times());

		await That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected that recording
			             has recorded the CustomEvent event on sut with my parameter at least 2 times,
			             but it was recorded once in [
			               CustomEvent("foo"),
			               CustomEvent("bar")
			             ]
			             """);
	}

	private sealed class CustomEventWithParametersClass<T1>
	{
		public delegate void CustomEventDelegate(T1 arg1);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(T1 arg1)
			=> CustomEvent?.Invoke(arg1);
	}
}
