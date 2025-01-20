﻿namespace aweXpect.Tests;

public sealed partial class ThatNumber
{
	public sealed class IsInfinite
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ForDouble_ShouldSupportChaining()
			{
				double subject = double.PositiveInfinity;

				async Task Act()
					=> await That(subject).IsInfinite()
						.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(double.PositiveInfinity)]
			[InlineData(double.NegativeInfinity)]
			public async Task ForDouble_WhenSubjectIsInfinity_ShouldSucceed(double subject)
			{
				async Task Act() => await That(subject).IsInfinite();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(-1d)]
			[InlineData(0d)]
			[InlineData(1d)]
			[InlineData(double.MinValue)]
			[InlineData(double.MaxValue)]
			[InlineData(double.Epsilon)]
			[InlineData(double.NaN)]
			public async Task ForDouble_WhenSubjectIsNormalValue_ShouldFail(double subject)
			{
				async Task Act()
					=> await That(subject).IsInfinite();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be infinite,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForFloat_ShouldSupportChaining()
			{
				float subject = float.PositiveInfinity;

				async Task Act() => await That(subject).IsInfinite()
					.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(float.PositiveInfinity)]
			[InlineData(float.NegativeInfinity)]
			public async Task ForFloat_WhenSubjectIsInfinity_ShouldSucceed(float subject)
			{
				async Task Act()
					=> await That(subject).IsInfinite();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(-1f)]
			[InlineData(0f)]
			[InlineData(1f)]
			[InlineData(float.MinValue)]
			[InlineData(float.MaxValue)]
			[InlineData(float.Epsilon)]
			[InlineData(float.NaN)]
			public async Task ForFloat_WhenSubjectIsNormalValue_ShouldFail(float subject)
			{
				async Task Act()
					=> await That(subject).IsInfinite();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be infinite,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForNullableDouble_ShouldSupportChaining()
			{
				double? subject = double.PositiveInfinity;

				async Task Act()
					=> await That(subject).IsInfinite()
						.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(double.PositiveInfinity)]
			[InlineData(double.NegativeInfinity)]
			public async Task ForNullableDouble_WhenSubjectIsInfinity_ShouldSucceed(double? subject)
			{
				async Task Act() => await That(subject).IsInfinite();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(-1d)]
			[InlineData(0d)]
			[InlineData(1d)]
			[InlineData(double.MinValue)]
			[InlineData(double.MaxValue)]
			[InlineData(double.Epsilon)]
			[InlineData(double.NaN)]
			[InlineData(null)]
			public async Task ForNullableDouble_WhenSubjectIsNormalValueOrNull_ShouldFail(
				double? subject)
			{
				async Task Act()
					=> await That(subject).IsInfinite();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be infinite,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForNullableFloat_ShouldSupportChaining()
			{
				float? subject = float.PositiveInfinity;

				async Task Act() => await That(subject).IsInfinite()
					.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(float.PositiveInfinity)]
			[InlineData(float.NegativeInfinity)]
			public async Task ForNullableFloat_WhenSubjectIsInfinity_ShouldSucceed(float? subject)
			{
				async Task Act()
					=> await That(subject).IsInfinite();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(-1f)]
			[InlineData(0f)]
			[InlineData(1f)]
			[InlineData(float.MinValue)]
			[InlineData(float.MaxValue)]
			[InlineData(float.Epsilon)]
			[InlineData(float.NaN)]
			public async Task ForNullableFloat_WhenSubjectIsNormalValue_ShouldFail(float? subject)
			{
				async Task Act()
					=> await That(subject).IsInfinite();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be infinite,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
