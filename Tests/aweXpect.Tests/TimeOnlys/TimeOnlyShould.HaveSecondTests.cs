﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	public sealed class HaveSecond
	{
		public sealed class Tests
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
	}
}
#endif
