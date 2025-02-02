﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTimeOffset
{
	public sealed class HasMinute
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMinute().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have minute equal to <null>,
					             but it had minute 14
					             """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsDifferent_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasMinute().EqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute equal to {Formatter.Format(expected)},
					              but it had minute 14
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int expected = 14;

				async Task Act()
					=> await That(subject).HasMinute().EqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class GreaterThanOrEqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMinute().GreaterThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have minute greater than or equal to <null>,
					             but it had minute 14
					             """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasMinute().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsLessThanExpected_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 15;

				async Task Act()
					=> await That(subject).HasMinute().GreaterThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute greater than or equal to {Formatter.Format(expected)},
					              but it had minute 14
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int expected = 14;

				async Task Act()
					=> await That(subject).HasMinute().GreaterThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class GreaterThanTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMinute().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have minute greater than <null>,
					             but it had minute 14
					             """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsGreaterThanExpected_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasMinute().GreaterThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsLessThanExpected_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 15;

				async Task Act()
					=> await That(subject).HasMinute().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute greater than {Formatter.Format(expected)},
					              but it had minute 14
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int expected = 14;

				async Task Act()
					=> await That(subject).HasMinute().GreaterThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute greater than {Formatter.Format(expected)},
					              but it had minute 14
					              """);
			}
		}

		public sealed class LessThanOrEqualToTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMinute().LessThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have minute less than or equal to <null>,
					             but it had minute 14
					             """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsGreaterThanExpected_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasMinute().LessThanOrEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute less than or equal to {Formatter.Format(expected)},
					              but it had minute 14
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsLessThanExpected_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 15;

				async Task Act()
					=> await That(subject).HasMinute().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSameAsExpected_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int expected = 14;

				async Task Act()
					=> await That(subject).HasMinute().LessThanOrEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class LessThanTests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMinute().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have minute less than <null>,
					             but it had minute 14
					             """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsGreaterThanExpected_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 13;

				async Task Act()
					=> await That(subject).HasMinute().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute less than {Formatter.Format(expected)},
					              but it had minute 14
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsLessThanExpected_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? expected = 15;

				async Task Act()
					=> await That(subject).HasMinute().LessThan(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSameAsExpected_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int expected = 14;

				async Task Act()
					=> await That(subject).HasMinute().LessThan(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute less than {Formatter.Format(expected)},
					              but it had minute 14
					              """);
			}
		}

		public sealed class NotEqualToTests
		{
			[Fact]
			public async Task WhenMinuteOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? unexpected = 13;

				async Task Act()
					=> await That(subject).HasMinute().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int unexpected = 14;

				async Task Act()
					=> await That(subject).HasMinute().NotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute not equal to {Formatter.Format(unexpected)},
					              but it had minute 14
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTimeOffset subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				int? unexpected = null;

				async Task Act()
					=> await That(subject).HasMinute().NotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
