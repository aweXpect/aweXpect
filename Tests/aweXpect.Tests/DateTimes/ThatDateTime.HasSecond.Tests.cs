﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class HasSecond
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasSecond().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have second equal to <null>,
					              but it had second 15
					              """);
			}

			[Fact]
			public async Task WhenSecondOfSubjectIsDifferent_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = 14;

				async Task Act()
					=> await That(subject).HasSecond().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have second equal to {Formatter.Format(expected)},
					              but it had second 15
					              """);
			}

			[Fact]
			public async Task WhenSecondOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int expected = 15;

				async Task Act()
					=> await That(subject).HasSecond().EqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class NotEqualToTests
		{
			[Fact]
			public async Task WhenSecondOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 14;

				async Task Act()
					=> await That(subject).HasSecond().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSecondOfSubjectIsTheSame_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int unexpected = 15;

				async Task Act()
					=> await That(subject).HasSecond().NotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have second not equal to {Formatter.Format(unexpected)},
					              but it had second 15
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).HasSecond().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
