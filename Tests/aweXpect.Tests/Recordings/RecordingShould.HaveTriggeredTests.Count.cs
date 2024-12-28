// ReSharper disable MemberCanBePrivate.Local

using aweXpect.Events;
// ReSharper disable AccessToDisposedClosure

namespace aweXpect.Tests.Recordings;

public sealed partial class RecordingShould
{
	public sealed partial class HaveTriggered
	{
		public sealed class CountTests
		{
			[Theory]
			[InlineData(1, 1, true)]
			[InlineData(2, 1, false)]
			[InlineData(1, 2, true)]
			[InlineData(2, 2, true)]
			[InlineData(8, 2, false)]
			[InlineData(2, 8, true)]
			public async Task ShouldSupportAtLeast(int minimum, int count, bool expectSuccess)
			{
				CustomEventWithoutParametersClass sut = new();
				using IRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvents(count);

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.AtLeast(minimum.Times());

				await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected recording to
					              have recorded the CustomEvent event on sut at least {(minimum == 1 ? "once" : $"{minimum} times")},
					              but it was recorded {(count == 1 ? "once" : $"{count} times")} in *
					              """).AsWildcard();
			}

			[Theory]
			[InlineData(1, 1, true)]
			[InlineData(2, 1, true)]
			[InlineData(1, 2, false)]
			[InlineData(2, 2, true)]
			[InlineData(8, 2, true)]
			[InlineData(2, 8, false)]
			public async Task ShouldSupportAtMost(int maximum, int count, bool expectSuccess)
			{
				CustomEventWithoutParametersClass sut = new();
				using IRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvents(count);

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.AtMost(maximum.Times());

				await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected recording to
					              have recorded the CustomEvent event on sut at most {(maximum == 1 ? "once" : $"{maximum} times")},
					              but it was recorded {(count == 1 ? "once" : $"{count} times")} in *
					              """).AsWildcard();
			}

			[Theory]
			[InlineData(0, 1, 3, false)]
			[InlineData(6, 8, 4, false)]
			[InlineData(6, 8, 5, false)]
			[InlineData(6, 8, 6, true)]
			[InlineData(6, 8, 7, true)]
			[InlineData(6, 8, 8, true)]
			[InlineData(6, 8, 9, false)]
			[InlineData(6, 8, 10, false)]
			public async Task ShouldSupportBetween(int minimum, int maximum, int count, bool expectSuccess)
			{
				CustomEventWithoutParametersClass sut = new();
				using IRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvents(count);

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.Between(minimum).And(maximum.Times());

				await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected recording to
					              have recorded the CustomEvent event on sut between {minimum} and {maximum} times,
					              but it was recorded {(count == 1 ? "once" : $"{count} times")} in *
					              """).AsWildcard();
			}

			[Theory]
			[InlineData(1, 1, true)]
			[InlineData(2, 1, false)]
			[InlineData(1, 2, false)]
			[InlineData(2, 2, true)]
			[InlineData(8, 2, false)]
			[InlineData(2, 8, false)]
			public async Task ShouldSupportExactly(int expected, int count, bool expectSuccess)
			{
				CustomEventWithoutParametersClass sut = new();
				using IRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvents(count);

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.Exactly(expected.Times());

				await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected recording to
					              have recorded the CustomEvent event on sut exactly {(expected == 1 ? "once" : $"{expected} times")},
					              but it was recorded {(count == 1 ? "once" : $"{count} times")} in *
					              """).AsWildcard();
			}

			[Theory]
			[InlineData(0, true)]
			[InlineData(1, false)]
			public async Task ShouldSupportNever(int count, bool expectSuccess)
			{
				CustomEventWithoutParametersClass sut = new();
				using IRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvents(count);

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.Never();

				await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage("""
					             Expected recording to
					             have never recorded the CustomEvent event on sut,
					             but it was recorded once in *
					             """).AsWildcard();
			}

			[Theory]
			[InlineData(0, false)]
			[InlineData(1, true)]
			[InlineData(2, false)]
			public async Task ShouldSupportOnce(int count, bool expectSuccess)
			{
				CustomEventWithoutParametersClass sut = new();
				using IRecording<CustomEventWithoutParametersClass> recording = sut.Record().Events();

				sut.NotifyCustomEvents(count);

				async Task Act() =>
					await That(recording).Should()
						.HaveTriggered(nameof(CustomEventWithoutParametersClass.CustomEvent))
						.Once();

				await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected recording to
					              have recorded the CustomEvent event on sut exactly once,
					              but it was {(count == 0 ? "never recorded" : $"recorded {count} times")} in *
					              """).AsWildcard();
			}
		}
	}
}
