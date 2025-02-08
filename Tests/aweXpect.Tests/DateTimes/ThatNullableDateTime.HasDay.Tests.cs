﻿namespace aweXpect.Tests;

public sealed partial class ThatNullableDateTime
{
	public sealed class HasDay
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsDifferent_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay().EqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				DateTime? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).HasDay().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have day equal to <null>,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				DateTime? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).HasDay().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have day equal to 1,
					             but it was <null>
					             """);
			}
		}

		public sealed class GreaterThanOrEqualToTests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = 11;

				async Task Act()
					=> await That(subject).HasDay().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsLessThanExpected_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = 11;

				async Task Act()
					=> await That(subject).HasDay().GreaterThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsLessThanExpected_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasDay().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasDay().LessThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 11;

				async Task Act()
					=> await That(subject).HasDay().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSame_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).HasDay().NotEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).HasDay().NotEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).HasDay().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
