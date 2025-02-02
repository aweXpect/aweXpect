﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed class HasMonth
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMonth().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have month equal to <null>,
					             but it had month 11
					             """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsDifferent_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMonth().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month equal to {Formatter.Format(expected)},
					              but it had month 11
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSame_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 11;

				async Task Act()
					=> await That(subject).HasMonth().EqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class GreaterThanOrEqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMonth().GreaterThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have month greater than or equal to <null>,
					             but it had month 11
					             """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 10;

				async Task Act()
					=> await That(subject).HasMonth().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsLessThanExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMonth().GreaterThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month greater than or equal to {Formatter.Format(expected)},
					              but it had month 11
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 11;

				async Task Act()
					=> await That(subject).HasMonth().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class GreaterThanTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMonth().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have month greater than <null>,
					             but it had month 11
					             """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 10;

				async Task Act()
					=> await That(subject).HasMonth().GreaterThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsLessThanExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMonth().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month greater than {Formatter.Format(expected)},
					              but it had month 11
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 11;

				async Task Act()
					=> await That(subject).HasMonth().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month greater than {Formatter.Format(expected)},
					              but it had month 11
					              """);
			}
		}

		public sealed class LessThanOrEqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMonth().LessThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have month less than or equal to <null>,
					             but it had month 11
					             """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsGreaterThanExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 10;

				async Task Act()
					=> await That(subject).HasMonth().LessThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month less than or equal to {Formatter.Format(expected)},
					              but it had month 11
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsLessThanExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMonth().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 11;

				async Task Act()
					=> await That(subject).HasMonth().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class LessThanTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMonth().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have month less than <null>,
					             but it had month 11
					             """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsGreaterThanExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 10;

				async Task Act()
					=> await That(subject).HasMonth().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month less than {Formatter.Format(expected)},
					              but it had month 11
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsLessThanExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMonth().LessThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 11;

				async Task Act()
					=> await That(subject).HasMonth().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month less than {Formatter.Format(expected)},
					              but it had month 11
					              """);
			}
		}

		public sealed class NotEqualToTests
		{
			[Fact]
			public async Task WhenMonthOfSubjectIsDifferent_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? unexpected = 12;

				async Task Act()
					=> await That(subject).HasMonth().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSame_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int unexpected = 11;

				async Task Act()
					=> await That(subject).HasMonth().NotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month not equal to {Formatter.Format(unexpected)},
					              but it had month 11
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).HasMonth().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
