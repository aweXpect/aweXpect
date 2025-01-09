﻿using aweXpect.Recording;

namespace aweXpect.Tests.Recording;

public sealed partial class EventRecordingShould
{
	public sealed partial class HaveTriggered
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
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithParametersClass<string, int, bool, DateTime>.CustomEvent));

				await That(Act).Should().NotThrow();
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
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.AtLeast(3.Times());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenCustomEventWithoutParametersIsTriggeredTooFewTimes_ShouldFail()
			{
				CustomEventWithoutParametersClass sut = new();
				IEventRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvent();

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.AtLeast(3.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
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
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithParametersClass<string>.CustomEvent))
						.AtLeast(3.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
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
					=> await That(subject!).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
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

				await That(Act).Should().Throw<NotSupportedException>()
					.WithMessage(
						"The CustomEvent event contains too many parameters (5): [string, int?, bool, DateTime, int]");
			}
		}
	}
}