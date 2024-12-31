using aweXpect.Recording;

namespace aweXpect.Tests.Recording;

public sealed partial class EventRecordingShould
{
	public sealed class HaveTriggeredPropertyChangedFor
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenPropertyNameDoesNotMatch_ShouldFail()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 2
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged("foo");

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggeredPropertyChangedFor(x => x.MyValue);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the PropertyChanged event on sut for property MyValue at least once,
					             but it was never recorded in [
					               PropertyChanged(PropertyChangedClass {
					                   MyValue = 2
					                 }, PropertyChangedEventArgs {
					                   PropertyName = "foo"
					                 })
					             ]
					             """);
			}

			[Fact]
			public async Task WhenPropertyNameMatches_ShouldSucceed()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 2
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggeredPropertyChangedFor(x => x.MyValue);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
