﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class IsEqualTo
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(DateTimeKind.Local)]
			[InlineData(DateTimeKind.Utc)]
			[InlineData(DateTimeKind.Unspecified)]
			public async Task WhenExpectedKindIsUnspecified_ShouldSucceed(DateTimeKind subjectKind)
			{
				DateTime subject = new(2024, 11, 1, 14, 15, 0, subjectKind);
				DateTime? expected = new(2024, 11, 1, 14, 15, 0, DateTimeKind.Unspecified);

				async Task Act()
					=> await That(subject).IsEqualTo(expected)
						.Because("the subject has Unspecified kind");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				DateTime subject = DateTime.MaxValue;
				DateTime expected = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				DateTime subject = DateTime.MinValue;
				DateTime expected = DateTime.MinValue;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldFail()
			{
				DateTime subject = CurrentTime();
				DateTime? expected = LaterTime();

				async Task Act()
					=> await That(subject).IsEqualTo(expected).Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is {Formatter.Format(expected)}, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsTheExpectedValue_ShouldSucceed()
			{
				DateTime subject = CurrentTime();
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(DateTimeKind.Local)]
			[InlineData(DateTimeKind.Utc)]
			[InlineData(DateTimeKind.Unspecified)]
			public async Task WhenSubjectKindIsUnspecified_ShouldSucceed(DateTimeKind expectedKind)
			{
				DateTime subject = new(2024, 11, 1, 14, 15, 0, DateTimeKind.Unspecified);
				DateTime? expected = new(2024, 11, 1, 14, 15, 0, expectedKind);

				async Task Act()
					=> await That(subject).IsEqualTo(expected)
						.Because("the subject has Unspecified kind");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectOnlyDiffersInKind_ShouldFail()
			{
				DateTime subject = new(2024, 11, 1, 14, 15, 0, DateTimeKind.Utc);
				DateTime? expected = new(2024, 11, 1, 14, 15, 0, DateTimeKind.Local);

				async Task Act()
					=> await That(subject).IsEqualTo(expected)
						.Because("we also test the kind property");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is {Formatter.Format(expected)}, because we also test the kind property,
					              but it differed in the Kind property
					              """);
			}

			[Fact]
			public async Task Within_NegativeTolerance_ShouldThrowArgumentOutOfRangeException()
			{
				DateTime subject = CurrentTime();
				DateTime? expected = LaterTime(4);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).Within(-1.Seconds());

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*Tolerance must be non-negative*").AsWildcard().And
					.WithParamName("tolerance");
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTime subject = CurrentTime();
				DateTime? expected = LaterTime(4);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).Within(3.Seconds())
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is {Formatter.Format(expected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTime subject = CurrentTime();
				DateTime? expected = LaterTime(3);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
