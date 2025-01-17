﻿namespace aweXpect.Tests;

public sealed partial class ThatNullableDateTime
{
	public sealed class HasKind
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenKindOfSubjectIsDifferent_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
				DateTimeKind expected = DateTimeKind.Local;

				async Task Act()
					=> await That(subject).HasKind(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have kind of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenKindOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
				DateTimeKind expected = DateTimeKind.Utc;

				async Task Act()
					=> await That(subject).HasKind(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				DateTime? subject = null;
				DateTimeKind expected = DateTimeKind.Utc;

				async Task Act()
					=> await That(subject).HasKind(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have kind of Utc,
					             but it was <null>
					             """);
			}
		}
	}
}
