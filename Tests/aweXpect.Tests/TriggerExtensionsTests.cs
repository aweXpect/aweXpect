﻿using System.ComponentModel;

namespace aweXpect.Tests;

public sealed class TriggerExtensionsTests
{
	[Fact]
	public async Task DoesNotTriggerPropertyChanged_WhenEventIsNotTriggered_ShouldSucceed()
	{
		PropertyChangedClass sut = new();

		async Task Act() =>
			await That(sut)
				.DoesNotTriggerPropertyChanged()
				.While(_ => { });

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task DoesNotTriggerPropertyChanged_WhenEventIsTriggered_ShouldFail()
	{
		PropertyChangedClass sut = new()
		{
			MyValue = 422
		};

		async Task Act() =>
			await That(sut)
				.DoesNotTriggerPropertyChanged()
				.While(t =>
				{
					t.NotifyPropertyChanged("SomeArbitraryProperty");
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger the PropertyChanged event never,
			             but it was recorded once in [
			               PropertyChanged(PropertyChangedClass {
			                   MyValue = 422
			                 }, PropertyChangedEventArgs {
			                   PropertyName = "SomeArbitraryProperty"
			                 })
			             ]
			             """);
	}

	[Fact]
	public async Task DoesNotTriggerPropertyChangedFor_WhenEventIsTriggered_ShouldFail()
	{
		PropertyChangedClass sut = new()
		{
			MyValue = 421
		};

		async Task Act() =>
			await That(sut)
				.DoesNotTriggerPropertyChangedFor(x => x.MyValue)
				.While(t =>
				{
					t.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger the PropertyChanged event for property MyValue never,
			             but it was recorded once in [
			               PropertyChanged(PropertyChangedClass {
			                   MyValue = 421
			                 }, PropertyChangedEventArgs {
			                   PropertyName = "MyValue"
			                 })
			             ]
			             """);
	}

	[Fact]
	public async Task DoesNotTriggerPropertyChangedFor_WhenEventIsTriggeredForOtherPropertyName_ShouldSucceed()
	{
		PropertyChangedClass sut = new();

		async Task Act() =>
			await That(sut)
				.DoesNotTriggerPropertyChangedFor(x => x.MyValue)
				.While(t =>
				{
					t.NotifyPropertyChanged("SomeOtherProperty");
				});

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task TriggersPropertyChangedFor_WhenEventIsTriggeredOftenEnough_ShouldSucceed()
	{
		PropertyChangedClass sut = new();

		async Task Act() =>
			await That(sut)
				.TriggersPropertyChangedFor(x => x.MyValue)
				.AtLeast(3.Times())
				.While(t =>
				{
					t.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));
					t.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));
					t.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));
				});

		await That(Act).Should().NotThrow();
	}

	[Fact]
	public async Task TriggersPropertyChangedFor_WhenEventIsTriggeredTooFewTimes_ShouldFail()
	{
		PropertyChangedClass sut = new()
		{
			MyValue = 42
		};

		async Task Act() =>
			await That(sut)
				.TriggersPropertyChangedFor(x => x.MyValue)
				.AtLeast(2.Times())
				.While(t =>
				{
					t.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger the PropertyChanged event for property MyValue at least 2 times,
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

		async Task Act() =>
			await That(sut)
				.TriggersPropertyChanged()
				.With<PropertyChangedEventArgs>(e => e.PropertyName == "foo")
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
				.With<PropertyChangedEventArgs>(e => e.PropertyName == "foo")
				.AtLeast(2.Times())
				.While(t =>
				{
					t.NotifyPropertyChanged("foo");
					t.NotifyPropertyChanged("bar");
				});

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             trigger the PropertyChanged event with PropertyChangedEventArgs e => e.PropertyName == "foo" at least 2 times,
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

	private sealed class PropertyChangedClass : INotifyPropertyChanged
	{
		public int MyValue { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		public void NotifyPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
