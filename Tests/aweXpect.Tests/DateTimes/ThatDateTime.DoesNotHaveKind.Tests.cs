﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class DoesNotHaveKind
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenKindOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
				DateTimeKind unexpected = DateTimeKind.Local;

				async Task Act()
					=> await That(subject).DoesNotHaveKind(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenKindOfSubjectIsTheSame_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
				DateTimeKind unexpected = DateTimeKind.Utc;

				async Task Act()
					=> await That(subject).DoesNotHaveKind(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have kind of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}