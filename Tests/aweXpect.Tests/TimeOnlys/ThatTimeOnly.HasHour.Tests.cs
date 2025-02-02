﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatTimeOnly
{
	public sealed class HasHour
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasHour().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have hour equal to <null>,
					             but it had hour 13
					             """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsDifferent_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 14;

				async Task Act()
					=> await That(subject).HasHour().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have hour equal to {Formatter.Format(expected)},
					              but it had hour 13
					              """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSame_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int expected = 13;

				async Task Act()
					=> await That(subject).HasHour().EqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class GreaterThanOrEqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasHour().GreaterThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have hour greater than or equal to <null>,
					             but it had hour 13
					             """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasHour().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenHourOfSubjectIsLessThanExpected_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 14;

				async Task Act()
					=> await That(subject).HasHour().GreaterThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have hour greater than or equal to {Formatter.Format(expected)},
					              but it had hour 13
					              """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int expected = 13;

				async Task Act()
					=> await That(subject).HasHour().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class GreaterThanTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasHour().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have hour greater than <null>,
					             but it had hour 13
					             """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasHour().GreaterThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenHourOfSubjectIsLessThanExpected_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 14;

				async Task Act()
					=> await That(subject).HasHour().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have hour greater than {Formatter.Format(expected)},
					              but it had hour 13
					              """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int expected = 13;

				async Task Act()
					=> await That(subject).HasHour().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have hour greater than {Formatter.Format(expected)},
					              but it had hour 13
					              """);
			}
		}

		public sealed class LessThanOrEqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasHour().LessThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have hour less than or equal to <null>,
					             but it had hour 13
					             """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsGreaterThanExpected_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasHour().LessThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have hour less than or equal to {Formatter.Format(expected)},
					              but it had hour 13
					              """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsLessThanExpected_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 14;

				async Task Act()
					=> await That(subject).HasHour().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int expected = 13;

				async Task Act()
					=> await That(subject).HasHour().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class LessThanTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasHour().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have hour less than <null>,
					             but it had hour 13
					             """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsGreaterThanExpected_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasHour().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have hour less than {Formatter.Format(expected)},
					              but it had hour 13
					              """);
			}

			[Fact]
			public async Task WhenHourOfSubjectIsLessThanExpected_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int? expected = 14;

				async Task Act()
					=> await That(subject).HasHour().LessThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int expected = 13;

				async Task Act()
					=> await That(subject).HasHour().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have hour less than {Formatter.Format(expected)},
					              but it had hour 13
					              """);
			}
		}

		public sealed class NotEqualToTests
		{
			[Fact]
			public async Task WhenHourOfSubjectIsDifferent_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int? unexpected = 14;

				async Task Act()
					=> await That(subject).HasHour().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSame_ShouldFail()
			{
				TimeOnly subject = new(13, 14, 15);
				int unexpected = 13;

				async Task Act()
					=> await That(subject).HasHour().NotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have hour not equal to {Formatter.Format(unexpected)},
					              but it had hour 13
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				TimeOnly subject = new(13, 14, 15);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).HasHour().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
