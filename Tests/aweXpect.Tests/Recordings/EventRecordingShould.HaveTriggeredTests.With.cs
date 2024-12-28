using System.ComponentModel;
using aweXpect.Recording;

namespace aweXpect.Tests.Recordings;

public sealed partial class EventRecordingShould
{
	public sealed partial class HaveTriggered
	{
		public sealed class WithTests
		{
			[Fact]
			public async Task WithEventArgs_WhenEventIsTriggeredBySomethingElse_ShouldFail()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 2
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(INotifyPropertyChanged.PropertyChanged))
						.With<PropertyChangedEventArgs>(e => e.PropertyName == "SomethingElse");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the PropertyChanged event on sut with PropertyChangedEventArgs e => e.PropertyName == "SomethingElse" at least once,
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
			public async Task WithEventArgs_WhenEventIsTriggeredByTheExpectedSender_ShouldSucceed()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 2
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(INotifyPropertyChanged.PropertyChanged))
						.With<PropertyChangedEventArgs>(e => e.PropertyName == "MyValue");

				await That(Act).Should().NotThrow();
			}
		}
	}
}
