﻿namespace aweXpect.Tests.Guids;

public sealed partial class NullableGuidShould
{
	public sealed class NotBeEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsEmpty_ShouldFail()
			{
				Guid subject = Guid.Empty;

				async Task Act()
					=> await That(subject).Should().NotBeEmpty();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be empty,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotEmpty_ShouldSucceed()
			{
				Guid? subject = OtherGuid();

				async Task Act()
					=> await That(subject).Should().NotBeEmpty();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeEmpty();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be empty,
					             but it was <null>
					             """);
			}
		}
	}
}
