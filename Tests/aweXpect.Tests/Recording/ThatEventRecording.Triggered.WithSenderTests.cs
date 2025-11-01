using System.ComponentModel;
using aweXpect.Recording;

namespace aweXpect.Tests;

public sealed partial class ThatEventRecording
{
	public sealed partial class Triggered
	{
		public sealed class WithSenderTests
		{
			[Fact]
			public async Task WhenEventIsTriggeredBySomethingElse_ShouldFail()
			{
				PropertyChangedClass sender = new()
				{
					MyValue = 1,
				};
				PropertyChangedClass sut = new()
				{
					MyValue = 2,
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(sut, nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Triggered(nameof(INotifyPropertyChanged.PropertyChanged))
						.WithSender(s => s == sender);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that recording
					             has recorded the PropertyChanged event on sut with sender s => s == sender at least once,
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
				PropertyChangedClass sender = new()
				{
					MyValue = 1,
				};
				PropertyChangedClass sut = new()
				{
					MyValue = 2,
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(sender, nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Triggered(nameof(INotifyPropertyChanged.PropertyChanged))
						.WithSender(s => s == sender);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithCustomEvent_WhenSenderIsTheOnlyParameter_ShouldSucceed()
			{
				CustomEventWithParametersClass<EventArgs> sut = new();
				IEventRecording<CustomEventWithParametersClass<EventArgs>> recording = sut.Record().Events();

				sut.NotifyCustomEvent(new PropertyChangedEventArgs("foo"));

				async Task Act() =>
					await That(recording).Triggered(nameof(CustomEventWithParametersClass<EventArgs>.CustomEvent))
						.WithSender(_ => true);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
