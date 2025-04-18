﻿using System.ComponentModel;
using aweXpect.Recording;

namespace aweXpect.Tests;

public sealed partial class ThatEventRecording
{
	public sealed partial class Triggered
	{
		public sealed class WithTests
		{
			[Fact]
			public async Task WhenEventIsTriggeredBySomethingElse_ShouldFail()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 2,
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Triggered(nameof(INotifyPropertyChanged.PropertyChanged))
						.With<PropertyChangedEventArgs>(e => e.PropertyName == "SomethingElse");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the PropertyChanged event on sut with PropertyChangedEventArgs e => e.PropertyName == "SomethingElse" at least once,
					             but it was never recorded in [
					               PropertyChanged(ThatEventRecording.PropertyChangedClass {
					                   MyValue = 2
					                 }, PropertyChangedEventArgs {
					                   PropertyName = "MyValue"
					                 })
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEventIsTriggeredByTheExpectedSender_ShouldSucceed()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 2,
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Triggered(nameof(INotifyPropertyChanged.PropertyChanged))
						.With<PropertyChangedEventArgs>(e => e.PropertyName == "MyValue");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithCustomEvent_WhenEventArgsAreFirstParameter_ShouldFail()
			{
				CustomEventWithParametersClass<EventArgs> sut = new();
				IEventRecording<CustomEventWithParametersClass<EventArgs>> recording = sut.Record().Events();

				sut.NotifyCustomEvent(new PropertyChangedEventArgs("foo"));

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithParametersClass<EventArgs>.CustomEvent))
						.With<PropertyChangedEventArgs>(e => e.PropertyName == "foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut with PropertyChangedEventArgs e => e.PropertyName == "foo" at least once,
					             but it was never recorded in [
					               CustomEvent(PropertyChangedEventArgs {
					                   PropertyName = "foo"
					                 })
					             ]
					             """);
			}
		}
	}
}
