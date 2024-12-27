using aweXpect.Events;

// ReSharper disable AccessToDisposedClosure

namespace aweXpect.Tests.Recordings;

public sealed partial class RecordingShould
{
	public sealed class NotHaveTriggeredPropertyChanged
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenEventIsNotTriggered_ShouldSucceed()
			{
				PropertyChangedClass sut = new();
				IRecording<PropertyChangedClass> recording = sut.Record().Events();

				async Task Act() =>
					await That(recording).Should()
						.NotHaveTriggeredPropertyChanged();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenEventIsTriggered_ShouldFail()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 422
				};
				IRecording<PropertyChangedClass> recording = sut.Record().Events();
				
				sut.NotifyPropertyChanged("SomeArbitraryProperty");

				async Task Act() =>
					await That(recording).Should()
						.NotHaveTriggeredPropertyChanged();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have never recorded the PropertyChanged event on sut,
					             but it was recorded once in [
					               PropertyChanged(PropertyChangedClass {
					                   MyValue = 422
					                 }, PropertyChangedEventArgs {
					                   PropertyName = "SomeArbitraryProperty"
					                 })
					             ]
					             """);
			}
		}
	}
}
