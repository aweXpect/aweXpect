﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTimeOffset
{
	public sealed partial class Nullable
	{
		public sealed class HasYear
		{
			public sealed class EqualToTests
			{
				[Fact]
				public async Task WhenExpectedIsNull_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = null;

					async Task Act()
						=> await That(subject).HasYear().EqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has year equal to <null>,
						             but it had year 2010
						             """);
				}

				[Fact]
				public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
				{
					DateTimeOffset? subject = null;
					int? expected = null;

					async Task Act()
						=> await That(subject).HasYear().EqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has year equal to <null>,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					DateTimeOffset? subject = null;
					int? expected = 1;

					async Task Act()
						=> await That(subject).HasYear().EqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has year equal to 1,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsDifferent_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2011;

					async Task Act()
						=> await That(subject).HasYear().EqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year equal to {Formatter.Format(expected)},
						              but it had year 2010
						              """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsTheSame_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int expected = 2010;

					async Task Act()
						=> await That(subject).HasYear().EqualTo(expected);

					await That(Act).DoesNotThrow();
				}
			}

			public sealed class GreaterThanOrEqualToTests
			{
				[Fact]
				public async Task WhenExpectedIsNull_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = null;

					async Task Act()
						=> await That(subject).HasYear().GreaterThanOrEqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has year greater than or equal to <null>,
						             but it had year 2010
						             """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsGreaterThanExpected_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2009;

					async Task Act()
						=> await That(subject).HasYear().GreaterThanOrEqualTo(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenYearOfSubjectIsLessThanExpected_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2011;

					async Task Act()
						=> await That(subject).HasYear().GreaterThanOrEqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year greater than or equal to {Formatter.Format(expected)},
						              but it had year 2010
						              """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsTheSameAsExpected_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int expected = 2010;

					async Task Act()
						=> await That(subject).HasYear().GreaterThanOrEqualTo(expected);

					await That(Act).DoesNotThrow();
				}
			}

			public sealed class GreaterThanTests
			{
				[Fact]
				public async Task WhenExpectedIsNull_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = null;

					async Task Act()
						=> await That(subject).HasYear().GreaterThan(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has year greater than <null>,
						             but it had year 2010
						             """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsGreaterThanExpected_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2009;

					async Task Act()
						=> await That(subject).HasYear().GreaterThan(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenYearOfSubjectIsLessThanExpected_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2011;

					async Task Act()
						=> await That(subject).HasYear().GreaterThan(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year greater than {Formatter.Format(expected)},
						              but it had year 2010
						              """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsTheSameAsExpected_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int expected = 2010;

					async Task Act()
						=> await That(subject).HasYear().GreaterThan(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year greater than {Formatter.Format(expected)},
						              but it had year 2010
						              """);
				}
			}

			public sealed class LessThanOrEqualToTests
			{
				[Fact]
				public async Task WhenExpectedIsNull_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = null;

					async Task Act()
						=> await That(subject).HasYear().LessThanOrEqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has year less than or equal to <null>,
						             but it had year 2010
						             """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsGreaterThanExpected_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2009;

					async Task Act()
						=> await That(subject).HasYear().LessThanOrEqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year less than or equal to {Formatter.Format(expected)},
						              but it had year 2010
						              """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsLessThanExpected_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2011;

					async Task Act()
						=> await That(subject).HasYear().LessThanOrEqualTo(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenYearOfSubjectIsTheSameAsExpected_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int expected = 2010;

					async Task Act()
						=> await That(subject).HasYear().LessThanOrEqualTo(expected);

					await That(Act).DoesNotThrow();
				}
			}

			public sealed class LessThanTests
			{
				[Fact]
				public async Task WhenExpectedIsNull_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = null;

					async Task Act()
						=> await That(subject).HasYear().LessThan(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has year less than <null>,
						             but it had year 2010
						             """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsGreaterThanExpected_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2009;

					async Task Act()
						=> await That(subject).HasYear().LessThan(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year less than {Formatter.Format(expected)},
						              but it had year 2010
						              """);
				}

				[Fact]
				public async Task WhenYearOfSubjectIsLessThanExpected_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? expected = 2011;

					async Task Act()
						=> await That(subject).HasYear().LessThan(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenYearOfSubjectIsTheSameAsExpected_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int expected = 2010;

					async Task Act()
						=> await That(subject).HasYear().LessThan(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year less than {Formatter.Format(expected)},
						              but it had year 2010
						              """);
				}
			}

			public sealed class NotEqualToTests
			{
				[Fact]
				public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
				{
					DateTimeOffset? subject = null;
					int? expected = null;

					async Task Act()
						=> await That(subject).HasYear().NotEqualTo(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					DateTimeOffset? subject = null;
					int? unexpected = 1;

					async Task Act()
						=> await That(subject).HasYear().NotEqualTo(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year not equal to {unexpected},
						              but it was <null>
						              """);
				}

				[Fact]
				public async Task WhenUnexpectedIsNull_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? unexpected = null;

					async Task Act()
						=> await That(subject).HasYear().NotEqualTo(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenYearOfSubjectIsDifferent_ShouldSucceed()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int? unexpected = 2011;

					async Task Act()
						=> await That(subject).HasYear().NotEqualTo(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenYearOfSubjectIsTheSame_ShouldFail()
				{
					DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
					int unexpected = 2010;

					async Task Act()
						=> await That(subject).HasYear().NotEqualTo(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has year not equal to {Formatter.Format(unexpected)},
						              but it had year 2010
						              """);
				}
			}
		}
	}
}
