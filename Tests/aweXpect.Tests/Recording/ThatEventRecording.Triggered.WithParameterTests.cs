﻿using aweXpect.Core;
using aweXpect.Recording;

namespace aweXpect.Tests;

public sealed partial class ThatEventRecording
{
	public sealed partial class Triggered
	{
		public sealed class WithParameterTests
		{
			[Theory]
			[InlineData(0, false)]
			[InlineData(1, true)]
			[InlineData(2, false)]
			[InlineData(3, false)]
			public async Task ShouldSupportPositionalParameterFilters(int position, bool expectSuccess)
			{
				CustomEventWithParametersClass<string, string, string> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, string, string>> recording =
					sut.Record().Events();

				sut.NotifyCustomEvent("p0", "p1", "p2");
				sut.NotifyCustomEvent("p0", "p1", "p2");

				async Task Act() =>
					await That(recording)
						.Triggered(nameof(CustomEventWithParametersClass<string, string, string>.CustomEvent))
						.WithParameter<string>(position, s => s == "p1")
						.AtLeast(2.Times());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected that recording
					              has recorded the CustomEvent event on sut with string parameter [{position}] s => s == "p1" at least twice,
					              but it was never recorded in [
					                CustomEvent("p0", "p1", "p2"),
					                CustomEvent("p0", "p1", "p2")
					              ]
					              """);
			}

			[Fact]
			public async Task WhenCustomEventWithParameters_WhenFilterResultsInTooFewRecordings_ShouldFail()
			{
				CustomEventWithParametersClass<string> sut = new();
				IEventRecording<CustomEventWithParametersClass<string>> recording = sut.Record().Events();

				sut.NotifyCustomEvent("foo");
				sut.NotifyCustomEvent("bar");

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithParametersClass<string>.CustomEvent))
						.WithParameter<string>(s => s == "foo")
						.AtLeast(2.Times());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut with string parameter s => s == "foo" at least twice,
					             but it was recorded once in [
					               CustomEvent("foo"),
					               CustomEvent("bar")
					             ]
					             """);
			}

			[Fact]
			public async Task WhenCustomEventWithParametersIsTriggeredOftenEnough_ShouldSucceed()
			{
				CustomEventWithParametersClass<string> sut = new();
				IEventRecording<CustomEventWithParametersClass<string>> recording = sut.Record().Events();

				sut.NotifyCustomEvent("foo");
				sut.NotifyCustomEvent("foo");
				sut.NotifyCustomEvent("foo");

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithParametersClass<string>.CustomEvent))
						.WithParameter<string>(s => s == "foo")
						.AtLeast(3.Times());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMultipleFiltersAreSpecified_ShouldVerifyAllFilters_ShouldFail()
			{
				CustomEventWithParametersClass<string, int> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, int>> recording = sut.Record().Events();

				sut.NotifyCustomEvent("foo", 1);
				sut.NotifyCustomEvent("bar", 2);

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithParametersClass<string, int>.CustomEvent))
						.WithParameter<string>(s => s == "foo")
						.WithParameter<int>(i => i > 1)
						.AtLeast(1.Times());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut with string parameter s => s == "foo" and with int parameter i => i > 1 at least once,
					             but it was never recorded in [
					               CustomEvent("foo", 1),
					               CustomEvent("bar", 2)
					             ]
					             """);
			}

			[Fact]
			public async Task WhenMultiplePositionFiltersAreSpecified_ShouldVerifyAllFilters_ShouldFail()
			{
				CustomEventWithParametersClass<string, string> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, string>> recording = sut.Record().Events();

				sut.NotifyCustomEvent("foo1", "bar2");
				sut.NotifyCustomEvent("bar1", "foo2");

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithParametersClass<string, int>.CustomEvent))
						.WithParameter<string>(0, s => s == "foo1")
						.WithParameter<string>(1, s => s == "foo2");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the CustomEvent event on sut with string parameter [0] s => s == "foo1" and with string parameter [1] s => s == "foo2" at least once,
					             but it was never recorded in [
					               CustomEvent("foo1", "bar2"),
					               CustomEvent("bar1", "foo2")
					             ]
					             """);
			}

			[Fact]
			public async Task WhenTypeIsNotUnique_ShouldCheckAllMatchingParameters()
			{
				CustomEventWithParametersClass<string, string> sut = new();
				IEventRecording<CustomEventWithParametersClass<string, string>> recording = sut.Record().Events();

				sut.NotifyCustomEvent("foo", "bar");

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithParametersClass<string, int>.CustomEvent))
						.WithParameter<string>(s => s == "bar");

				await That(Act).DoesNotThrow();
			}
		}
	}
}
