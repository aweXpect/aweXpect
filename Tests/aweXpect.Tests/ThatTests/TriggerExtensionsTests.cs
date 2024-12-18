﻿using System.ComponentModel;

namespace aweXpect.Tests.ThatTests;

public sealed class TriggerExtensionsTests
{
	[Fact]
	public async Task WhenPropertyChangedEventIsTriggeredOftenEnough_ShouldSucceed()
	{
		PropertyChangedClass sut = new();

		async Task Act() =>
			await That(sut)
				.TriggersPropertyChanged()
				.WithPropertyChangedEventArgs(e => e.PropertyName == "foo")
				.AtLeast(3.Times())
				.While(t =>
				{
					t.NotifyPropertyChanged("foo");
					t.NotifyPropertyChanged("foo");
					t.NotifyPropertyChanged("foo");
				});

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task WhenPropertyChangedEventIsTriggeredTooFewTimes_ShouldFail()
	{
		PropertyChangedClass sut = new();

		async Task Act() =>
			await That(sut)
				.TriggersPropertyChanged()
				.WithPropertyChangedEventArgs(e => e.PropertyName == "foo")
				.AtLeast(2.Times())
				.While(t =>
				{
					t.NotifyPropertyChanged("foo");
					t.NotifyPropertyChanged("bar");
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger event PropertyChanged with PropertyChangedEventArgs parameter e => e.PropertyName == "foo" at least 2 times,
			             but it was recorded once in [
			               PropertyChanged(PropertyChangedClass { }, PropertyChangedEventArgs {
			                   PropertyName = "foo"
			                 }),
			               PropertyChanged(PropertyChangedClass { }, PropertyChangedEventArgs {
			                   PropertyName = "bar"
			                 })
			             ]
			             """);
	}

	private sealed class PropertyChangedClass : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public void NotifyPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
