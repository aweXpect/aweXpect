﻿namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public class BeNullOrWhiteSpace
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldSucceed()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).Should().BeNullOrWhiteSpace();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenActualIsNotEmpty_ShouldFail(string? subject)
			{
				async Task Act()
					=> await That(subject).Should().BeNullOrWhiteSpace();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be null or white-space,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenActualIsNotEmpty_ShouldLimitDisplayedStringTo100Characters()
			{
				string subject = StringWithMoreThan100Characters;

				async Task Act()
					=> await That(subject).Should().BeNullOrWhiteSpace();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be null or white-space,
					              but it was "{StringWith100Characters}…"
					              """);
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldSucceed()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).Should().BeNullOrWhiteSpace();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsWhitespace_ShouldFail()
			{
				string subject = " \t ";

				async Task Act()
					=> await That(subject).Should().BeNullOrWhiteSpace();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
