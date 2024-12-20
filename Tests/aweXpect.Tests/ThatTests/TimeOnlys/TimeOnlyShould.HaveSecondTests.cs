﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests.ThatTests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	public sealed class HaveSecondTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			TimeOnly subject = new(10, 11, 12);
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveSecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have second of <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSecondOfSubjectIsDifferent_ShouldFail()
		{
			TimeOnly subject = new(10, 11, 12);
			int? expected = 11;

			async Task Act()
				=> await That(subject).Should().HaveSecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have second of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSecondOfSubjectIsTheSame_ShouldSucceed()
		{
			TimeOnly subject = new(10, 11, 12);
			int expected = 12;

			async Task Act()
				=> await That(subject).Should().HaveSecond(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveSecondTests
	{
		[Fact]
		public async Task WhenSecondOfSubjectIsDifferent_ShouldSucceed()
		{
			TimeOnly subject = new(10, 11, 12);
			int? unexpected = 11;

			async Task Act()
				=> await That(subject).Should().NotHaveSecond(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSecondOfSubjectIsTheSame_ShouldFail()
		{
			TimeOnly subject = new(10, 11, 12);
			int unexpected = 12;

			async Task Act()
				=> await That(subject).Should().NotHaveSecond(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have second of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			TimeOnly subject = new(10, 11, 12);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveSecond(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
