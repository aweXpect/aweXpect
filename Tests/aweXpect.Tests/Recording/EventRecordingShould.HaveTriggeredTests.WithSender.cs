using System.ComponentModel;
using aweXpect.Recording;

namespace aweXpect.Tests.Recording;

public sealed partial class EventRecordingShould
{
	public sealed partial class HaveTriggered
	{
		public sealed class WithSenderTests
		{
			[Fact]
			public async Task WithSender_WhenEventIsTriggeredBySomethingElse_ShouldFail()
			{
				PropertyChangedClass sender = new()
				{
					MyValue = 1
				};
				PropertyChangedClass sut = new()
				{
					MyValue = 2
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(sut, nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(INotifyPropertyChanged.PropertyChanged))
						.WithSender(s => s == sender);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the PropertyChanged event on sut with sender s => s == sender at least once,
					             but it was never recorded in [
					               PropertyChanged(PropertyChangedClass {
					                   MyValue = 2
					                 }, PropertyChangedEventArgs {
					                   PropertyName = "MyValue"
					                 })
					             ]
					             """);
			}

			[Fact]
			public async Task WithSender_WhenEventIsTriggeredByTheExpectedSender_ShouldSucceed()
			{
				PropertyChangedClass sender = new()
				{
					MyValue = 1
				};
				PropertyChangedClass sut = new()
				{
					MyValue = 2
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(sender, nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(INotifyPropertyChanged.PropertyChanged))
						.WithSender(s => s == sender);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
