using System.Threading;

// ReSharper disable MemberCanBePrivate.Local

namespace aweXpect.Core.Tests;

public sealed class TriggerTests
{
	[Fact]
	public async Task MultipleEvents_WhenCombination()
	{
		MultipleEventsClass<string, int> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventA))
				.WithParameter<string>(s => s == "foo")
				.AtLeast(2.Times())
				.And
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventA))
				.WithParameter<string>(s => s == "bar")
				.AtMost(1.Times())
				.And
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventB))
				.WithParameter<int>(s => s == 3)
				.While(t =>
				{
					t.NotifyCustomEventA("foo");
					t.NotifyCustomEventA("bar");
					t.NotifyCustomEventB(2);
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			               trigger event CustomEventA
			                 [1] with string parameter s => s == "foo" at least 2 times and
			                 [2] with string parameter s => s == "bar" at most once and
			               [3] trigger event CustomEventB with int parameter s => s == 3 at least once,
			             but it was
			               [1] only recorded once in [
			                     CustomEventA("foo"),
			                     CustomEventA("bar")
			                   ] and
			               [3] never recorded in [
			                     CustomEventB(2)
			                   ]
			             """);
	}

	[Fact]
	public async Task MultipleEvents_WhenListeningMultipleTimesToDifferentEvent_ShouldDisplayAllEventQueues()
	{
		MultipleEventsClass<string, int> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventA))
				.WithParameter<string>(s => s == "foo")
				.AtLeast(2.Times())
				.And
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventB))
				.WithParameter<int>(s => s == 3)
				.While(t =>
				{
					t.NotifyCustomEventA("foo");
					t.NotifyCustomEventB(2);
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			               [1] trigger event CustomEventA with string parameter s => s == "foo" at least 2 times and
			               [2] trigger event CustomEventB with int parameter s => s == 3 at least once,
			             but it was
			               [1] only recorded once in [
			                     CustomEventA("foo")
			                   ] and
			               [2] never recorded in [
			                     CustomEventB(2)
			                   ]
			             """);
	}

	[Fact]
	public async Task
		MultipleEvents_WhenListeningMultipleTimesToDifferentEventAndOnlySomeAreSatisfied_ShouldDisplayAllFailedEventQueues()
	{
		MultipleEventsClass<string, int> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventA))
				.WithParameter<string>(s => s == "foo")
				.AtLeast(2.Times())
				.And
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventB))
				.WithParameter<int>(s => s == 3)
				.While(t =>
				{
					t.NotifyCustomEventA("foo");
					t.NotifyCustomEventB(3);
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			               [1] trigger event CustomEventA with string parameter s => s == "foo" at least 2 times and
			               [2] trigger event CustomEventB with int parameter s => s == 3 at least once,
			             but it was
			               [1] only recorded once in [
			                     CustomEventA("foo")
			                   ]
			             """);
	}

	[Fact]
	public async Task MultipleEvents_WhenListeningMultipleTimesToSameEvent_ShouldGroupEventsInFailureMessage()
	{
		MultipleEventsClass<string, int> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventA))
				.WithParameter<string>(s => s == "foo")
				.AtLeast(2.Times())
				.And
				.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventA))
				.WithParameter<string>(s => s == "bar")
				.AtLeast(3.Times())
				.While(t =>
				{
					t.NotifyCustomEventA("foo");
					t.NotifyCustomEventA("bar");
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEventA
			               [1] with string parameter s => s == "foo" at least 2 times and
			               [2] with string parameter s => s == "bar" at least 3 times,
			             but it was
			               [1] only recorded once and
			               [2] only recorded once in [
			                     CustomEventA("foo"),
			                     CustomEventA("bar")
			                   ]
			             """);
	}

	[Fact]
	public async Task ShouldConsiderCancellationTokenInAsyncCallback()
	{
		AsyncCustomEventClass sut = new();
		using CancellationTokenSource cts = new();
		// ReSharper disable once MethodHasAsyncOverload
		cts.Cancel();
		CancellationToken token = cts.Token;

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(AsyncCustomEventClass.CustomEvent))
				.AtLeast(2.Times())
				.While((t, c) => t.NotifyCustomEventAsync(c))
				.WithCancellation(token);

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent at least 2 times,
			             but it was never recorded in []
			             """);
	}

	[Fact]
	public async Task ShouldStopListeningAfterWhileCallback()
	{
		CustomEventWithoutParametersClass sut = new();
		sut.NotifyCustomEvent();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
				.AtLeast(2.Times())
				.While(t =>
				{
					t.NotifyCustomEvent();
				});

		sut.NotifyCustomEvent();
		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent at least 2 times,
			             but it was only recorded once in [
			               CustomEvent()
			             ]
			             """);
	}

	[Fact]
	public async Task ShouldSupportAsyncCallback()
	{
		AsyncCustomEventClass sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(AsyncCustomEventClass.CustomEvent))
				.AtLeast(2.Times())
				.While(t => t.NotifyCustomEventAsync());

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent at least 2 times,
			             but it was only recorded once in [
			               CustomEvent()
			             ]
			             """);
	}

	[Fact]
	public async Task ShouldSupportEventsWith4Parameters()
	{
		CustomEventWithParametersClass<string, int, bool, DateTime> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithParametersClass<string, int, bool, DateTime>.CustomEvent))
				.While(t =>
				{
					t.NotifyCustomEvent("foo", 1, true, DateTime.Now);
				});

		await That(Act).Should().NotThrow();
	}

	[Theory]
	[InlineData(0, false)]
	[InlineData(1, true)]
	[InlineData(2, false)]
	[InlineData(3, false)]
	public async Task ShouldSupportPositionalParameterFilters(int position, bool expectSuccess)
	{
		CustomEventWithParametersClass<string, string, string> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithParametersClass<string, string, string>.CustomEvent))
				.WithParameter<string>(position, s => s == "p1")
				.AtLeast(2.Times())
				.While(t =>
				{
					t.NotifyCustomEvent("p0", "p1", "p2");
					t.NotifyCustomEvent("p0", "p1", "p2");
				});

		await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
			.WithMessage($"""
			              Expected sut to
			              trigger event CustomEvent with string parameter [{position}] s => s == "p1" at least 2 times,
			              but it was never recorded in [
			                CustomEvent("p0", "p1", "p2"),
			                CustomEvent("p0", "p1", "p2")
			              ]
			              """);
	}

	[Fact]
	public async Task WhenCustomEventWithoutParametersIsTriggeredOftenEnough_ShouldSucceed()
	{
		CustomEventWithoutParametersClass sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
				.AtLeast(3.Times())
				.While(t =>
				{
					t.NotifyCustomEvent();
					t.NotifyCustomEvent();
					t.NotifyCustomEvent();
				});

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task WhenCustomEventWithoutParametersIsTriggeredTooFewTimes_ShouldFail()
	{
		CustomEventWithoutParametersClass sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
				.AtLeast(3.Times())
				.While(t =>
				{
					t.NotifyCustomEvent();
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent at least 3 times,
			             but it was only recorded once in [
			               CustomEvent()
			             ]
			             """);
	}

	[Fact]
	public async Task WhenCustomEventWithParameters_WhenFilterResultsInTooFewRecordings_ShouldFail()
	{
		CustomEventWithParametersClass<string> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithParametersClass<string>.CustomEvent))
				.WithParameter<string>(s => s == "foo")
				.AtLeast(2.Times())
				.While(t =>
				{
					t.NotifyCustomEvent("foo");
					t.NotifyCustomEvent("bar");
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent with string parameter s => s == "foo" at least 2 times,
			             but it was only recorded once in [
			               CustomEvent("foo"),
			               CustomEvent("bar")
			             ]
			             """);
	}

	[Fact]
	public async Task WhenCustomEventWithParametersIsTriggeredOftenEnough_ShouldSucceed()
	{
		CustomEventWithParametersClass<string> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithParametersClass<string>.CustomEvent))
				.WithParameter<string>(s => s == "foo")
				.AtLeast(3.Times())
				.While(t =>
				{
					t.NotifyCustomEvent("foo");
					t.NotifyCustomEvent("foo");
					t.NotifyCustomEvent("foo");
				});

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task WhenCustomEventWithParametersIsTriggeredTooFewTimes_ShouldFail()
	{
		CustomEventWithParametersClass<string> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithParametersClass<string>.CustomEvent))
				.AtLeast(3.Times())
				.While(t =>
				{
					t.NotifyCustomEvent("foo");
					t.NotifyCustomEvent("bar");
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent at least 3 times,
			             but it was only recorded 2 times in [
			               CustomEvent("foo"),
			               CustomEvent("bar")
			             ]
			             """);
	}

	[Fact]
	public async Task WhenMultipleFiltersAreSpecified_ShouldVerifyAllFilters_ShouldFail()
	{
		CustomEventWithParametersClass<string, int> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithParametersClass<string, int>.CustomEvent))
				.WithParameter<string>(s => s == "foo")
				.WithParameter<int>(i => i > 1)
				.AtLeast(1.Times())
				.While(t =>
				{
					t.NotifyCustomEvent("foo", 1);
					t.NotifyCustomEvent("bar", 2);
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent with string parameter s => s == "foo" and with int parameter i => i > 1 at least once,
			             but it was never recorded in [
			               CustomEvent("foo", 1),
			               CustomEvent("bar", 2)
			             ]
			             """);
	}

	[Fact]
	public async Task WhenUsingEventWith5Parameters_ShouldThrowNotSupportedException()
	{
		CustomEventWithParametersClass<string, int?, bool, DateTime, int> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithParametersClass<string, int?, bool, DateTime, int>.CustomEvent))
				.While(t =>
				{
					t.NotifyCustomEvent("foo", null, true, DateTime.Now, 0);
				});

		await That(Act).Should().Throw<NotSupportedException>()
			.WithMessage("The event CustomEvent contains too many parameters (5): [string, int?, bool, DateTime, int]");
	}

	private sealed class AsyncCustomEventClass
	{
		public delegate void CustomEventDelegate();

		public event CustomEventDelegate? CustomEvent;

		public async Task NotifyCustomEventAsync()
		{
			await Task.Delay(5);
			CustomEvent?.Invoke();
		}

		public async Task NotifyCustomEventAsync(CancellationToken cancellationToken)
		{
			await Task.Yield();
			if (!cancellationToken.IsCancellationRequested)
			{
				CustomEvent?.Invoke();
			}
		}
	}

	private sealed class CustomEventWithoutParametersClass
	{
		public delegate void CustomEventDelegate();

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent()
			=> CustomEvent?.Invoke();
	}

	private sealed class CustomEventWithParametersClass<T1>
	{
		public delegate void CustomEventDelegate(T1 arg1);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(T1 arg1)
			=> CustomEvent?.Invoke(arg1);
	}

	private sealed class MultipleEventsClass<T1, T2>
	{
		public delegate void CustomEventDelegateA(T1 arg1);

		public delegate void CustomEventDelegateB(T2 arg1);

		public event CustomEventDelegateA? CustomEventA;
		public event CustomEventDelegateB? CustomEventB;

		public void NotifyCustomEventA(T1 arg1)
			=> CustomEventA?.Invoke(arg1);

		public void NotifyCustomEventB(T2 arg1)
			=> CustomEventB?.Invoke(arg1);
	}

	private sealed class CustomEventWithParametersClass<T1, T2>
	{
		public delegate void CustomEventDelegate(T1 arg1, T2 arg2);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(T1 arg1, T2 arg2)
			=> CustomEvent?.Invoke(arg1, arg2);
	}

	private sealed class CustomEventWithParametersClass<T1, T2, T3>
	{
		public delegate void CustomEventDelegate(T1 arg1, T2 arg2, T3 arg3);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(T1 arg1, T2 arg2, T3 arg3)
			=> CustomEvent?.Invoke(arg1, arg2, arg3);
	}

	private sealed class CustomEventWithParametersClass<T1, T2, T3, T4>
	{
		public delegate void CustomEventDelegate(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
			=> CustomEvent?.Invoke(arg1, arg2, arg3, arg4);
	}

	private sealed class CustomEventWithParametersClass<T1, T2, T3, T4, T5>
	{
		public delegate void CustomEventDelegate(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
			=> CustomEvent?.Invoke(arg1, arg2, arg3, arg4, arg5);
	}
}
