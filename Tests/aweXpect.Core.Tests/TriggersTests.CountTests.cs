// ReSharper disable MemberCanBePrivate.Local

namespace aweXpect.Core.Tests;

public sealed partial class TriggerTests
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

			async Task Act() =>
				await That(sut)
					.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
					.AtLeast(minimum.Times())
					.While(t => t.NotifyCustomEvents(count));

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected sut to
				              trigger event CustomEvent at least {(minimum == 1 ? "once" : $"{minimum} times")},
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

			async Task Act() =>
				await That(sut)
					.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
					.AtMost(maximum.Times())
					.While(t => t.NotifyCustomEvents(count));

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected sut to
				              trigger event CustomEvent at most {(maximum == 1 ? "once" : $"{maximum} times")},
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

			async Task Act() =>
				await That(sut)
					.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
					.Between(minimum).And(maximum.Times())
					.While(t => t.NotifyCustomEvents(count));

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected sut to
				              trigger event CustomEvent between {minimum} and {maximum} times,
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

			async Task Act() =>
				await That(sut)
					.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
					.Exactly(expected.Times())
					.While(t => t.NotifyCustomEvents(count));

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected sut to
				              trigger event CustomEvent exactly {(expected == 1 ? "once" : $"{expected} times")},
				              but it was recorded {(count == 1 ? "once" : $"{count} times")} in *
				              """).AsWildcard();
		}

		[Theory]
		[InlineData(0, true)]
		[InlineData(1, false)]
		public async Task ShouldSupportNever(int count, bool expectSuccess)
		{
			CustomEventWithoutParametersClass sut = new();

			async Task Act() =>
				await That(sut)
					.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
					.Never()
					.While(t => t.NotifyCustomEvents(count));

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage("""
				             Expected sut to
				             trigger event CustomEvent never,
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

			async Task Act() =>
				await That(sut)
					.Triggers(nameof(CustomEventWithoutParametersClass.CustomEvent))
					.Once()
					.While(t => t.NotifyCustomEvents(count));

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected sut to
				              trigger event CustomEvent exactly once,
				              but it was {(count == 0 ? "never recorded" : $"recorded {count} times")} in *
				              """).AsWildcard();
		}
	}
}
