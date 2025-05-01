﻿using aweXpect.Recording;

namespace aweXpect.Tests;

public sealed partial class ThatEventRecording
{
	public sealed partial class Triggered
	{
		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenEventWith1ParameterIsNotTriggeredWithinTimeout_ShouldFail()
			{
				CustomEventWithParametersClass<string> sut = new();
				IEventRecording<CustomEventWithParametersClass<string>> recording =
					sut.Record().Events();

				_ = Task.Delay(2000.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent("foo"));

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string>.CustomEvent))
						.Within(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut at least once within 0:00.010,
					             but it was never recorded in []
					             """);
			}

			[Fact]
			public async Task WhenEventWith1ParameterIsTriggeredWithinTimeout_ShouldSucceed()
			{
				CustomEventWithParametersClass<string> sut = new();
				IEventRecording<CustomEventWithParametersClass<string>> recording =
					sut.Record().Events();

				_ = Task.Delay(20.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent("foo"));

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string>.CustomEvent))
						.Within(5.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEventWith2ParametersIsNotTriggeredWithinTimeout_ShouldFail()
			{
				CustomEventWithParametersClass<string, int> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, int>> recording =
					sut.Record().Events();

				_ = Task.Delay(2000.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent("foo", 1));

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, int>.CustomEvent))
						.Within(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut at least once within 0:00.010,
					             but it was never recorded in []
					             """);
			}

			[Fact]
			public async Task WhenEventWith2ParametersIsTriggeredWithinTimeout_ShouldSucceed()
			{
				CustomEventWithParametersClass<string, int> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, int>> recording =
					sut.Record().Events();

				_ = Task.Delay(20.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent("foo", 1));

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, int>.CustomEvent))
						.Within(5.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEventWith3ParametersIsNotTriggeredWithinTimeout_ShouldFail()
			{
				CustomEventWithParametersClass<string, int, bool> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, int, bool>> recording =
					sut.Record().Events();

				_ = Task.Delay(2000.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent("foo", 1, true));

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, int, bool>.CustomEvent))
						.Within(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut at least once within 0:00.010,
					             but it was never recorded in []
					             """);
			}

			[Fact]
			public async Task WhenEventWith3ParametersIsTriggeredWithinTimeout_ShouldSucceed()
			{
				CustomEventWithParametersClass<string, int, bool> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, int, bool>> recording =
					sut.Record().Events();

				_ = Task.Delay(20.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent("foo", 1, true));

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, int, bool>.CustomEvent))
						.Within(5.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEventWith4ParametersIsNotTriggeredWithinTimeout_ShouldFail()
			{
				CustomEventWithParametersClass<string, int, bool, DateTime> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, int, bool, DateTime>> recording =
					sut.Record().Events();

				_ = Task.Delay(2000.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent("foo", 1, true, DateTime.Now));

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, int, bool, DateTime>.CustomEvent))
						.Within(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut at least once within 0:00.010,
					             but it was never recorded in []
					             """);
			}

			[Fact]
			public async Task WhenEventWith4ParametersIsTriggeredWithinTimeout_ShouldSucceed()
			{
				CustomEventWithParametersClass<string, int, bool, DateTime> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, int, bool, DateTime>> recording =
					sut.Record().Events();

				_ = Task.Delay(20.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent("foo", 1, true, DateTime.Now));

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, int, bool, DateTime>.CustomEvent))
						.Within(5.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEventWithoutParametersIsNotTriggeredWithinTimeout_ShouldFail()
			{
				CustomEventWithoutParametersClass sut = new();
				IEventRecording<CustomEventWithoutParametersClass> recording =
					sut.Record().Events();

				_ = Task.Delay(2000.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent());

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.Within(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut at least once within 0:00.010,
					             but it was never recorded in []
					             """);
			}

			[Fact]
			public async Task WhenEventWithoutParametersIsTriggeredWithinTimeout_ShouldSucceed()
			{
				CustomEventWithoutParametersClass sut = new();
				IEventRecording<CustomEventWithoutParametersClass> recording =
					sut.Record().Events();

				_ = Task.Delay(20.Milliseconds())
					.ContinueWith(_ => sut.NotifyCustomEvent());

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.Within(5.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEventRecording<CustomEventWithoutParametersClass>? subject = null;

				async Task Act()
					=> await That(subject!).Triggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.Within(4.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has recorded the CustomEvent event at least once within 0:04,
					             but it was <null>
					             """);
			}
		}
	}
}
