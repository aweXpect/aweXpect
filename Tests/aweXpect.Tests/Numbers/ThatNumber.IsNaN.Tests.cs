﻿namespace aweXpect.Tests;

public sealed partial class ThatNumber
{
	public sealed class IsNaN
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ForDouble_ShouldSupportChaining()
			{
				double subject = double.NaN;

				async Task Act()
					=> await That(subject).IsNaN()
						.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(double.PositiveInfinity)]
			[InlineData(double.NegativeInfinity)]
			public async Task ForDouble_WhenSubjectIsInfinity_ShouldFail(double subject)
			{
				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be NaN,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForDouble_WhenSubjectIsNaN_ShouldSucceed()
			{
				double subject = double.NaN;

				async Task Act() => await That(subject).IsNaN();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(-1d)]
			[InlineData(0d)]
			[InlineData(1d)]
			[InlineData(double.MinValue)]
			[InlineData(double.MaxValue)]
			[InlineData(double.Epsilon)]
			public async Task ForDouble_WhenSubjectIsNormalValue_ShouldFail(double subject)
			{
				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be NaN,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForFloat_ShouldSupportChaining()
			{
				float subject = float.NaN;

				async Task Act() => await That(subject).IsNaN()
					.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(float.PositiveInfinity)]
			[InlineData(float.NegativeInfinity)]
			public async Task ForFloat_WhenSubjectIsInfinity_ShouldFail(float subject)
			{
				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be NaN,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForFloat_WhenSubjectIsNaN_ShouldSucceed()
			{
				float subject = float.NaN;

				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(-1f)]
			[InlineData(0f)]
			[InlineData(1f)]
			[InlineData(float.MinValue)]
			[InlineData(float.MaxValue)]
			[InlineData(float.Epsilon)]
			public async Task ForFloat_WhenSubjectIsNormalValue_ShouldFail(float subject)
			{
				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be NaN,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForNullableDouble_ShouldSupportChaining()
			{
				double? subject = double.NaN;

				async Task Act()
					=> await That(subject).IsNaN()
						.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(double.PositiveInfinity)]
			[InlineData(double.NegativeInfinity)]
			[InlineData(null)]
			public async Task ForNullableDouble_WhenSubjectIsInfinityOrNull_ShouldFail(
				double? subject)
			{
				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be NaN,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForNullableDouble_WhenSubjectIsNaN_ShouldSucceed()
			{
				double? subject = double.NaN;

				async Task Act() => await That(subject).IsNaN();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(-1d)]
			[InlineData(0d)]
			[InlineData(1d)]
			[InlineData(double.MinValue)]
			[InlineData(double.MaxValue)]
			[InlineData(double.Epsilon)]
			public async Task ForNullableDouble_WhenSubjectIsNormalValue_ShouldFail(
				double? subject)
			{
				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be NaN,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForNullableFloat_ShouldSupportChaining()
			{
				float? subject = float.NaN;

				async Task Act() => await That(subject).IsNaN()
					.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(float.PositiveInfinity)]
			[InlineData(float.NegativeInfinity)]
			public async Task ForNullableFloat_WhenSubjectIsInfinity_ShouldFail(
				float? subject)
			{
				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be NaN,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForNullableFloat_WhenSubjectIsNaN_ShouldSucceed()
			{
				float? subject = float.NaN;

				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(-1f)]
			[InlineData(0f)]
			[InlineData(1f)]
			[InlineData(float.MinValue)]
			[InlineData(float.MaxValue)]
			[InlineData(float.Epsilon)]
			public async Task ForNullableFloat_WhenSubjectIsNormalValue_ShouldFail(float? subject)
			{
				async Task Act()
					=> await That(subject).IsNaN();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be NaN,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
