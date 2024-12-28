using aweXpect.Recording;

// ReSharper disable AccessToDisposedClosure

namespace aweXpect.Tests.Recordings;

public sealed partial class RecordingShould
{
	public sealed class NotHaveTriggeredPropertyChangedFor
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenEventIsNotTriggeredAtAll_ShouldSucceed()
			{
				PropertyChangedClass sut = new();
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				async Task Act() =>
					await That(recording).Should()
						.NotHaveTriggeredPropertyChangedFor(x => x.MyValue);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenEventIsTriggered_ShouldFail()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 421
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged(nameof(PropertyChangedClass.MyValue));

				async Task Act() =>
					await That(recording).Should()
						.NotHaveTriggeredPropertyChangedFor(x => x.MyValue);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have never recorded the PropertyChanged event on sut for property MyValue,
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
			public async Task WhenEventIsTriggeredForOtherPropertyName_ShouldSucceed()
			{
				PropertyChangedClass sut = new();
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged("SomeOtherProperty");

				async Task Act() =>
					await That(recording).Should()
						.NotHaveTriggeredPropertyChangedFor(x => x.MyValue);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
