namespace aweXpect.Tests;

public sealed partial class ThatNumber
{
	public sealed class IsNotNaN
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ForDouble_ShouldSupportChaining()
			{
				double subject = double.Epsilon;

				async Task Act()
					=> await That(subject).IsNotNaN()
						.And.Is(subject);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ForDouble_WhenSubjectIsNaN_ShouldFail()
			{
				double subject = double.NaN;

				async Task Act() => await That(subject).IsNotNaN();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be NaN,
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
			[InlineData(double.NegativeInfinity)]
			[InlineData(double.PositiveInfinity)]
			public async Task ForDouble_WhenSubjectIsNormalValue_ShouldSucceed(double subject)
			{
				async Task Act()
					=> await That(subject).IsNotNaN();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ForFloat_ShouldSupportChaining()
			{
				float subject = float.Epsilon;

				async Task Act() => await That(subject).IsNotNaN()
					.And.Is(subject);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ForFloat_WhenSubjectIsNaN_ShouldFail()
			{
				float subject = float.NaN;

				async Task Act()
					=> await That(subject).IsNotNaN();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be NaN,
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
			[InlineData(float.NegativeInfinity)]
			[InlineData(float.PositiveInfinity)]
			public async Task ForFloat_WhenSubjectIsNormalValue_ShouldSucceed(float subject)
			{
				async Task Act()
					=> await That(subject).IsNotNaN();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ForNullableDouble_ShouldSupportChaining()
			{
				double? subject = double.Epsilon;

				async Task Act()
					=> await That(subject).IsNotNaN()
						.And.Is(subject);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ForNullableDouble_WhenSubjectIsNaN_ShouldFail()
			{
				double? subject = double.NaN;

				async Task Act() => await That(subject).IsNotNaN();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be NaN,
					             but it was NaN
					             """);
			}

			[Theory]
			[InlineData(-1d)]
			[InlineData(0d)]
			[InlineData(1d)]
			[InlineData(double.MinValue)]
			[InlineData(double.MaxValue)]
			[InlineData(double.Epsilon)]
			[InlineData(double.NegativeInfinity)]
			[InlineData(double.PositiveInfinity)]
			[InlineData(null)]
			public async Task ForNullableDouble_WhenSubjectIsNormalValueOrNull_ShouldSucceed(
				double? subject)
			{
				async Task Act()
					=> await That(subject).IsNotNaN();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ForNullableFloat_ShouldSupportChaining()
			{
				float? subject = float.Epsilon;

				async Task Act() => await That(subject).IsNotNaN()
					.And.Is(subject);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ForNullableFloat_WhenSubjectIsNaN_ShouldFail()
			{
				float? subject = float.NaN;

				async Task Act()
					=> await That(subject).IsNotNaN();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be NaN,
					             but it was NaN
					             """);
			}

			[Theory]
			[InlineData(-1f)]
			[InlineData(0f)]
			[InlineData(1f)]
			[InlineData(float.MinValue)]
			[InlineData(float.MaxValue)]
			[InlineData(float.Epsilon)]
			[InlineData(float.NegativeInfinity)]
			[InlineData(float.PositiveInfinity)]
			public async Task ForNullableFloat_WhenSubjectIsNormalValue_ShouldSucceed(float? subject)
			{
				async Task Act()
					=> await That(subject).IsNotNaN();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
