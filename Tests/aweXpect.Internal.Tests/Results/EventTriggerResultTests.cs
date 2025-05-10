using aweXpect.Core;
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
				.AtLeast().Twice();

		await That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected that recording
			             has recorded the CustomEvent event on sut with my parameter at least twice,
			             but it was recorded once in [
			               CustomEvent("foo"),
			               CustomEvent("bar")
			             ]
			             """);
	}

	[Fact]
	public async Task WithMultipleMatchingParameters_PredicateChecksForAnyOne()
	{
		CustomEventWithParametersClass<string, int, string> sut = new();
		IEventRecording<CustomEventWithParametersClass<string, int, string>> recording = sut.Record().Events();

		sut.NotifyCustomEvent("foo", 1, "bar");
		sut.NotifyCustomEvent("bar", 2, "foo");

		async Task Act() =>
			await ((EventTriggerResult<CustomEventWithParametersClass<string, int, string>>.IExtensions)That(recording)
					.Triggered(nameof(CustomEventWithParametersClass<string, int, string>.CustomEvent)))
				.WithParameter<string>(" with my parameter", null, s => s == "foo")
				.AtLeast(2.Times());

		await That(Act).DoesNotThrow();
	}

	[Theory]
	[InlineData(0, false)]
	[InlineData(1, true)]
	[InlineData(2, false)]
	[InlineData(3, true)]
	[InlineData(4, true)]
	public async Task WithPosition_ShouldConsiderPosition(int position, bool expectFailure)
	{
		CustomEventWithParametersClass<string, string, string> sut = new();
		IEventRecording<CustomEventWithParametersClass<string, string, string>> recording = sut.Record().Events();

		sut.NotifyCustomEvent("foo", "bar", "baz");
		sut.NotifyCustomEvent("bar", "baz", "foo");

		async Task Act() =>
			await ((EventTriggerResult<CustomEventWithParametersClass<string, string, string>>.IExtensions)
					That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, string, string>.CustomEvent)))
				.WithParameter<string>(" with my parameter", position, s => s == "foo");

		await That(Act).Throws<XunitException>().OnlyIf(expectFailure)
			.WithMessage("""
			             Expected that recording
			             has recorded the CustomEvent event on sut with my parameter at least once,
			             but it was never recorded in [
			               CustomEvent("foo", "bar", "baz"),
			               CustomEvent("bar", "baz", "foo")
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

	private sealed class CustomEventWithParametersClass<T1, T2, T3>
	{
		public delegate void CustomEventDelegate(T1 arg1, T2 arg2, T3 arg3);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(T1 arg1, T2 arg2, T3 arg3)
			=> CustomEvent?.Invoke(arg1, arg2, arg3);
	}
}
