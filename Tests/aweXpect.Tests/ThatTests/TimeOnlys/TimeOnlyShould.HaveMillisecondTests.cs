#if NET6_0_OR_GREATER
namespace Testably.Expectations.Tests.ThatTests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	public sealed class HaveMillisecondTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			TimeOnly subject = new(10, 11, 12, 345);
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveMillisecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have millisecond of <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenMillisecondOfSubjectIsDifferent_ShouldFail()
		{
			TimeOnly subject = new(10, 11, 12, 345);
			int? expected = 12;

			async Task Act()
				=> await That(subject).Should().HaveMillisecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have millisecond of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenMillisecondOfSubjectIsTheSame_ShouldSucceed()
		{
			TimeOnly subject = new(10, 11, 12, 345);
			int expected = 345;

			async Task Act()
				=> await That(subject).Should().HaveMillisecond(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveMillisecondTests
	{
		[Fact]
		public async Task WhenMillisecondOfSubjectIsDifferent_ShouldSucceed()
		{
			TimeOnly subject = new(10, 11, 12, 345);
			int? unexpected = 12;

			async Task Act()
				=> await That(subject).Should().NotHaveMillisecond(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenMillisecondOfSubjectIsTheSame_ShouldFail()
		{
			TimeOnly subject = new(10, 11, 12, 345);
			int unexpected = 345;

			async Task Act()
				=> await That(subject).Should().NotHaveMillisecond(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have millisecond of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			TimeOnly subject = new(10, 11, 12, 345);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveMillisecond(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
