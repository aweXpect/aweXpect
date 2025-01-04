﻿namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public class BeNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldFail()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).Should().BeNull();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be null,
					             but it was ""
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenActualIsNotNull_ShouldFail(string? subject)
			{
				async Task Act()
					=> await That(subject).Should().BeNull();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be null,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldSucceed()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).Should().BeNull();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
