using System.ComponentModel;

namespace aweXpect.Tests.ThatTests;

public sealed class TriggerTests
{
	[Fact]
	public async Task WhenEventIsTriggeredOftenEnough_ShouldSucceed()
	{
		PropertyChangedClass sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(PropertyChangedClass.PropertyChanged))
				.WithParameter<PropertyChangedEventArgs>(e => e.PropertyName == "foo")
				.While(t =>
				{
					t.NotifyPropertyChanged("foo");
					t.NotifyPropertyChanged("foo");
					t.NotifyPropertyChanged("foo");
				})
				.AtLeast(3.Times());

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task WhenEventIsTriggeredTooFewTimes_ShouldFail()
	{
		PropertyChangedClass sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(PropertyChangedClass.PropertyChanged))
				.WithParameter<PropertyChangedEventArgs>(e => e.PropertyName == "foo")
				.While(t =>
				{
					t.NotifyPropertyChanged("foo");
				})
				.AtLeast(3.Times());

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event PropertyChanged with PropertyChangedEventArgs parameter e => e.PropertyName == "foo" at least 3 times,
			             but it was only recorded once
			             """);
	}
	[Fact]
	public async Task WhenCustomEventWithoutParametersIsTriggeredOftenEnough_ShouldSucceed()
	{
		CustomEventWithoutParametersClass sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
				.While(t =>
				{
					t.NotifyCustomEvent();
					t.NotifyCustomEvent();
					t.NotifyCustomEvent();
				})
				.AtLeast(3.Times());

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task WhenCustomEventWithoutParametersIsTriggeredTooFewTimes_ShouldFail()
	{
		CustomEventWithoutParametersClass sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
				.While(t =>
				{
					t.NotifyCustomEvent();
				})
				.AtLeast(3.Times());

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent at least 3 times,
			             but it was only recorded once
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
				.While(t =>
				{
					t.NotifyCustomEvent("foo");
					t.NotifyCustomEvent("foo");
					t.NotifyCustomEvent("foo");
				})
				.AtLeast(3.Times());

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task WhenCustomEventWithParametersIsTriggeredTooFewTimes_ShouldFail()
	{
		CustomEventWithParametersClass<string> sut = new();

		async Task Act() =>
			await That(sut)
				.Triggers(nameof(CustomEventWithParametersClass<string>.CustomEvent))
				.While(t =>
				{
					t.NotifyCustomEvent("foo");
				})
				.AtLeast(3.Times());

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event CustomEvent at least 3 times,
			             but it was only recorded once
			             """);
	}

	private sealed class PropertyChangedClass : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public void NotifyPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
	
	private sealed class CustomEventWithParametersClass<T1>
	{
		public delegate void CustomEventDelegate(T1 arg1);
		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent(T1 arg1)
			=> CustomEvent?.Invoke(arg1);
	}
	
	private sealed class CustomEventWithoutParametersClass
	{
		public delegate void CustomEventDelegate();
		public event CustomEventDelegate? CustomEvent;

		public void NotifyCustomEvent()
			=> CustomEvent?.Invoke();
	}
}
