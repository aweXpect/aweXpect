﻿using System.ComponentModel;
using aweXpect.Recording;

namespace aweXpect.Tests;

public sealed partial class ThatEventRecording
{
	public sealed class HasTriggeredPropertyChanged
	{
		public sealed class Tests
		{
			[Fact]
			public async Task TriggersPropertyChangedFor_WhenEventIsTriggeredOftenEnough_ShouldSucceed()
			{
				PropertyChangedClass sut = new();

				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));
				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));
				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).TriggeredPropertyChanged()
						.AtLeast(3.Times());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task TriggersPropertyChangedFor_WhenEventIsTriggeredTooFewTimes_ShouldFail()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 42
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).TriggeredPropertyChanged()
						.AtLeast(2.Times());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the PropertyChanged event on sut at least 2 times,
					             but it was recorded once in [
					               PropertyChanged(PropertyChangedClass {
					                   MyValue = 42
					                 }, PropertyChangedEventArgs {
					                   PropertyName = "MyValue"
					                 })
					             ]
					             """);
			}

			[Fact]
			public async Task WhenPropertyChangedEventIsTriggeredOftenEnough_ShouldSucceed()
			{
				PropertyChangedClass sut = new();
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged("foo");
				sut.NotifyPropertyChanged("foo");
				sut.NotifyPropertyChanged("foo");

				async Task Act() =>
					await That(recording).TriggeredPropertyChanged()
						.With<PropertyChangedEventArgs>(e => e.PropertyName == "foo")
						.AtLeast(3.Times());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenPropertyChangedEventIsTriggeredTooFewTimes_ShouldFail()
			{
				PropertyChangedClass sut = new();
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged("foo");
				sut.NotifyPropertyChanged("bar");

				async Task Act() =>
					await That(recording).TriggeredPropertyChanged()
						.With<PropertyChangedEventArgs>(e => e.PropertyName == "foo")
						.AtLeast(2.Times());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the PropertyChanged event on sut with PropertyChangedEventArgs e => e.PropertyName == "foo" at least 2 times,
					             but it was recorded once in [
					               PropertyChanged(PropertyChangedClass {
					                   MyValue = 0
					                 }, PropertyChangedEventArgs {
					                   PropertyName = "foo"
					                 }),
					               PropertyChanged(PropertyChangedClass {
					                   MyValue = 0
					                 }, PropertyChangedEventArgs {
					                   PropertyName = "bar"
					                 })
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEventRecording<PropertyChangedClass>? subject = null;

				async Task Act()
					=> await That(subject!).TriggeredPropertyChanged();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have recorded the PropertyChanged event at least once,
					             but it was <null>
					             """);
			}
		}
	}
}