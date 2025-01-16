using aweXpect.Recording;

// ReSharper disable AccessToDisposedClosure

namespace aweXpect.Tests.Recording;

public sealed partial class EventRecordingShould
{
	public sealed class NotHaveTriggeredPropertyChanged
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenEventIsNotTriggered_ShouldSucceed()
			{
				PropertyChangedClass sut = new();
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				async Task Act() =>
					await That(recording).Should()
						.NotHaveTriggeredPropertyChanged();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEventIsTriggered_ShouldFail()
			{
				PropertyChangedClass sut = new()
				{
					MyValue = 422
				};
				IEventRecording<PropertyChangedClass> recording = sut.Record().Events();

				sut.NotifyPropertyChanged("SomeArbitraryProperty");

				async Task Act() =>
					await That(recording).Should()
						.NotHaveTriggeredPropertyChanged();

				await That(Act).Does().Throw<XunitException>()
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

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEventRecording<PropertyChangedClass>? subject = null;

				async Task Act()
					=> await That(subject!).Should()
						.NotHaveTriggeredPropertyChanged();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have never recorded the PropertyChanged event,
					             but it was <null>
					             """);
			}
		}
	}
}
