// ReSharper disable MemberCanBePrivate.Local

namespace aweXpect.Core.Tests;

public sealed partial class TriggerTests
{
	public sealed class MultipleEventTests
	{
		[Fact]
		public async Task ShouldSupportCombinationOfOneAndMultipleEvents()
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
				               trigger the CustomEventA event
				                 [1] with string parameter s => s == "foo" at least 2 times and
				                 [2] with string parameter s => s == "bar" at most once and
				               [3] trigger the CustomEventB event with int parameter s => s == 3 at least once,
				             but it was
				               [1] recorded once in [
				                     CustomEventA("foo"),
				                     CustomEventA("bar")
				                   ] and
				               [3] never recorded in [
				                     CustomEventB(2)
				                   ]
				             """);
		}

		[Fact]
		public async Task WhenListeningMultipleTimesToSameEvent_ShouldGroupEventsInFailureMessage()
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
				             trigger the CustomEventA event
				               [1] with string parameter s => s == "foo" at least 2 times and
				               [2] with string parameter s => s == "bar" at least 3 times,
				             but it was
				               [1] recorded once and
				               [2] recorded once in [
				                     CustomEventA("foo"),
				                     CustomEventA("bar")
				                   ]
				             """);
		}

		[Fact]
		public async Task
			WhenListeningOnceToMultipleDifferentEvents_AndOnlySomeAreSatisfied_ShouldDisplayAllFailedEventQueues()
		{
			MultipleEventsClass<string, int> sut = new();

			async Task Act() =>
				await That(sut)
					.Triggers(nameof(MultipleEventsClass<string, int>.CustomEventA))
					.WithParameter<string>(s => s == "foo")
					.Never()
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
				               [1] trigger the CustomEventA event with string parameter s => s == "foo" never and
				               [2] trigger the CustomEventB event with int parameter s => s == 3 at least once,
				             but it was
				               [1] recorded once in [
				                     CustomEventA("foo")
				                   ]
				             """);
		}

		[Fact]
		public async Task WhenListeningOnceToMultipleDifferentEvents_ShouldDisplayAllEventQueues()
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
				               [1] trigger the CustomEventA event with string parameter s => s == "foo" at least 2 times and
				               [2] trigger the CustomEventB event with int parameter s => s == 3 at least once,
				             but it was
				               [1] recorded once in [
				                     CustomEventA("foo")
				                   ] and
				               [2] never recorded in [
				                     CustomEventB(2)
				                   ]
				             """);
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
	}
}
