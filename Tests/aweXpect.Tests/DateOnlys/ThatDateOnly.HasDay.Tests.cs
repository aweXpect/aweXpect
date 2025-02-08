﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed class HasDay
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsDifferent_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 11;

				async Task Act()
					=> await That(subject).HasDay().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              have day equal to {Formatter.Format(expected)},
					              but it had day 12
					              """);
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSame_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay().EqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasDay().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have day equal to <null>,
					             but it had day 12
					             """);
			}
		}

		public sealed class GreaterThanOrEqualToTests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 11;

				async Task Act()
					=> await That(subject).HasDay().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsLessThanExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasDay().GreaterThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              have day greater than or equal to {Formatter.Format(expected)},
					              but it had day 12
					              """);
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasDay().GreaterThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have day greater than or equal to <null>,
					             but it had day 12
					             """);
			}
		}

		public sealed class GreaterThanTests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 11;

				async Task Act()
					=> await That(subject).HasDay().GreaterThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsLessThanExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasDay().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              have day greater than {Formatter.Format(expected)},
					              but it had day 12
					              """);
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              have day greater than {Formatter.Format(expected)},
					              but it had day 12
					              """);
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasDay().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have day greater than <null>,
					             but it had day 12
					             """);
			}
		}

		public sealed class LessThanOrEqualToTests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsGreaterThanExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 11;

				async Task Act()
					=> await That(subject).HasDay().LessThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              have day less than or equal to {Formatter.Format(expected)},
					              but it had day 12
					              """);
			}

			[Fact]
			public async Task WhenDayOfSubjectIsLessThanExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasDay().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasDay().LessThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have day less than or equal to <null>,
					             but it had day 12
					             """);
			}
		}

		public sealed class LessThanTests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsGreaterThanExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 11;

				async Task Act()
					=> await That(subject).HasDay().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              have day less than {Formatter.Format(expected)},
					              but it had day 12
					              """);
			}

			[Fact]
			public async Task WhenDayOfSubjectIsLessThanExpected_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasDay().LessThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              have day less than {Formatter.Format(expected)},
					              but it had day 12
					              """);
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasDay().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have day less than <null>,
					             but it had day 12
					             """);
			}
		}

		public sealed class NotEqualToTests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsDifferent_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? unexpected = 11;

				async Task Act()
					=> await That(subject).HasDay().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSame_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int unexpected = 12;

				async Task Act()
					=> await That(subject).HasDay().NotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              have day not equal to {Formatter.Format(unexpected)},
					              but it had day 12
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).HasDay().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
