﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed class HasYear
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasYear(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have year of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenYearOfSubjectIsDifferent_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 2011;

				async Task Act()
					=> await That(subject).HasYear(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have year of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenYearOfSubjectIsTheSame_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 2010;

				async Task Act()
					=> await That(subject).HasYear(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
