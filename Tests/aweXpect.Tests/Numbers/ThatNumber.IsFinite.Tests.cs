namespace aweXpect.Tests;

public sealed partial class ThatNumber
{
	public sealed class IsFinite
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ForDouble_ShouldSupportChaining()
			{
				double subject = double.Epsilon;

				async Task Act()
					=> await That(subject).IsFinite()
						.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(double.PositiveInfinity)]
			[InlineData(double.NegativeInfinity)]
			[InlineData(double.NaN)]
			public async Task ForDouble_WhenSubjectIsInfinityOrNaN_ShouldFail(
				double subject)
			{
				async Task Act() => await That(subject).IsFinite();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be finite,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(-1d)]
			[InlineData(0d)]
			[InlineData(1d)]
			[InlineData(double.MinValue)]
			[InlineData(double.MaxValue)]
			[InlineData(double.Epsilon)]
			public async Task ForDouble_WhenSubjectIsNormalValue_ShouldSucceed(double subject)
			{
				async Task Act()
					=> await That(subject).IsFinite();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ForFloat_ShouldSupportChaining()
			{
				float subject = float.Epsilon;

				async Task Act() => await That(subject).IsFinite()
					.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(float.PositiveInfinity)]
			[InlineData(float.NegativeInfinity)]
			[InlineData(float.NaN)]
			public async Task ForFloat_WhenSubjectIsInfinityOrNaN_ShouldFail(
				float subject)
			{
				async Task Act()
					=> await That(subject).IsFinite();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be finite,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(-1f)]
			[InlineData(0f)]
			[InlineData(1f)]
			[InlineData(float.MinValue)]
			[InlineData(float.MaxValue)]
			[InlineData(float.Epsilon)]
			public async Task ForFloat_WhenSubjectIsNormalValue_ShouldSucceed(float subject)
			{
				async Task Act()
					=> await That(subject).IsFinite();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ForNullableDouble_ShouldSupportChaining()
			{
				double? subject = double.Epsilon;

				async Task Act()
					=> await That(subject).IsFinite()
						.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(double.PositiveInfinity)]
			[InlineData(double.NegativeInfinity)]
			[InlineData(double.NaN)]
			[InlineData(null)]
			public async Task ForNullableDouble_WhenSubjectIsInfinityOrNaNOrNull_ShouldFail(
				double? subject)
			{
				async Task Act() => await That(subject).IsFinite();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be finite,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(-1d)]
			[InlineData(0d)]
			[InlineData(1d)]
			[InlineData(double.MinValue)]
			[InlineData(double.MaxValue)]
			[InlineData(double.Epsilon)]
			public async Task ForNullableDouble_WhenSubjectIsNormalValue_ShouldSucceed(double? subject)
			{
				async Task Act()
					=> await That(subject).IsFinite();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ForNullableFloat_ShouldSupportChaining()
			{
				float? subject = float.Epsilon;

				async Task Act() => await That(subject).IsFinite()
					.And.Is(subject);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(float.PositiveInfinity)]
			[InlineData(float.NegativeInfinity)]
			[InlineData(float.NaN)]
			[InlineData(null)]
			public async Task ForNullableFloat_WhenSubjectIsInfinityOrNaNOrNull_ShouldFail(
				float? subject)
			{
				async Task Act()
					=> await That(subject).IsFinite();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be finite,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(-1f)]
			[InlineData(0f)]
			[InlineData(1f)]
			[InlineData(float.MinValue)]
			[InlineData(float.MaxValue)]
			[InlineData(float.Epsilon)]
			public async Task ForNullableFloat_WhenSubjectIsNormalValue_ShouldSucceed(float? subject)
			{
				async Task Act()
					=> await That(subject).IsFinite();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
