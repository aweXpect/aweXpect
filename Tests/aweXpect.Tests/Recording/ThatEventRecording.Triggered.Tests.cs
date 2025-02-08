using aweXpect.Recording;

namespace aweXpect.Tests;

public sealed partial class ThatEventRecording
{
	public sealed partial class HasTriggered
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ShouldSupportEventsWith4Parameters()
			{
				CustomEventWithParametersClass<string, int, bool, DateTime> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, int, bool, DateTime>> recording =
					sut.Record().Events();

				sut.NotifyCustomEvent("foo", 1, true, DateTime.Now);

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, int, bool, DateTime>.CustomEvent));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenCustomEventWithoutParametersIsTriggeredOftenEnough_ShouldSucceed()
			{
				CustomEventWithoutParametersClass sut = new();
				IEventRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvent();
				sut.NotifyCustomEvent();
				sut.NotifyCustomEvent();

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.AtLeast(3.Times());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenCustomEventWithoutParametersIsTriggeredTooFewTimes_ShouldFail()
			{
				CustomEventWithoutParametersClass sut = new();
				IEventRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvent();

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.AtLeast(3.Times());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             have recorded the CustomEvent event on sut at least 3 times,
					             but it was recorded once in [
					               CustomEvent()
					             ]
					             """);
			}

			[Fact]
			public async Task WhenCustomEventWithParametersIsTriggeredTooFewTimes_ShouldFail()
			{
				CustomEventWithParametersClass<string> sut = new();
				IEventRecording<CustomEventWithParametersClass<string>> recording = sut.Record().Events();

				sut.NotifyCustomEvent("foo");
				sut.NotifyCustomEvent("bar");

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithParametersClass<string>.CustomEvent))
						.AtLeast(3.Times());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             have recorded the CustomEvent event on sut at least 3 times,
					             but it was recorded 2 times in [
					               CustomEvent("foo"),
					               CustomEvent("bar")
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEventRecording<CustomEventWithoutParametersClass>? subject = null;

				async Task Act()
					=> await That(subject!).Triggered(nameof(CustomEventWithoutParametersClass.CustomEvent));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have recorded the CustomEvent event at least once,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenUsingEventWith5Parameters_ShouldThrowNotSupportedException()
			{
				CustomEventWithParametersClass<string, int?, bool, DateTime, int> sut = new();

				void Act() =>
					sut.Record().Events();

				await That(Act).Throws<NotSupportedException>()
					.WithMessage(
						"The CustomEvent event contains too many parameters (5): [string, int?, bool, DateTime, int]");
			}
		}
	}
}
