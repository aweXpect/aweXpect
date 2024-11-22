namespace Testably.Expectations.Tests.ThatTests.Numbers;

public sealed partial class NumberShould
{
	public sealed class BeNaNTests
	{
		[Fact]
		public async Task ForDouble_ShouldSupportChaining()
		{
			double subject = double.NaN;

			async Task Act()
				=> await That(subject).Should().BeNaN()
					.And.Be(subject);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(double.PositiveInfinity)]
		[InlineData(double.NegativeInfinity)]
		public async Task ForDouble_WhenSubjectIsInfinity_ShouldFail(double subject)
		{
			async Task Act()
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().Throw<XunitException>()
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

			async Task Act() => await That(subject).Should().BeNaN();

			await That(Act).Should().NotThrow();
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
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().Throw<XunitException>()
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

			async Task Act() => await That(subject).Should().BeNaN()
				.And.Be(subject);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(float.PositiveInfinity)]
		[InlineData(float.NegativeInfinity)]
		public async Task ForFloat_WhenSubjectIsInfinity_ShouldFail(float subject)
		{
			async Task Act()
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().NotThrow();
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
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().BeNaN()
					.And.Be(subject);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(double.PositiveInfinity)]
		[InlineData(double.NegativeInfinity)]
		[InlineData(null)]
		public async Task ForNullableDouble_WhenSubjectIsInfinityOrNull_ShouldFail(
			double? subject)
		{
			async Task Act()
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().Throw<XunitException>()
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

			async Task Act() => await That(subject).Should().BeNaN();

			await That(Act).Should().NotThrow();
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
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().Throw<XunitException>()
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

			async Task Act() => await That(subject).Should().BeNaN()
				.And.Be(subject);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(float.PositiveInfinity)]
		[InlineData(float.NegativeInfinity)]
		public async Task ForNullableFloat_WhenSubjectIsInfinity_ShouldFail(
			float? subject)
		{
			async Task Act()
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().NotThrow();
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
				=> await That(subject).Should().BeNaN();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be NaN,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}

	public sealed class NotBeNaNTests
	{
		[Fact]
		public async Task ForDouble_ShouldSupportChaining()
		{
			double subject = double.Epsilon;

			async Task Act()
				=> await That(subject).Should().NotBeNaN()
					.And.Be(subject);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForDouble_WhenSubjectIsNaN_ShouldFail()
		{
			double subject = double.NaN;

			async Task Act() => await That(subject).Should().NotBeNaN();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().NotBeNaN();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForFloat_ShouldSupportChaining()
		{
			float subject = float.Epsilon;

			async Task Act() => await That(subject).Should().NotBeNaN()
				.And.Be(subject);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForFloat_WhenSubjectIsNaN_ShouldFail()
		{
			float subject = float.NaN;

			async Task Act()
				=> await That(subject).Should().NotBeNaN();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().NotBeNaN();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForNullableDouble_ShouldSupportChaining()
		{
			double? subject = double.Epsilon;

			async Task Act()
				=> await That(subject).Should().NotBeNaN()
					.And.Be(subject);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForNullableDouble_WhenSubjectIsNaN_ShouldFail()
		{
			double? subject = double.NaN;

			async Task Act() => await That(subject).Should().NotBeNaN();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().NotBeNaN();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForNullableFloat_ShouldSupportChaining()
		{
			float? subject = float.Epsilon;

			async Task Act() => await That(subject).Should().NotBeNaN()
				.And.Be(subject);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForNullableFloat_WhenSubjectIsNaN_ShouldFail()
		{
			float? subject = float.NaN;

			async Task Act()
				=> await That(subject).Should().NotBeNaN();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().NotBeNaN();

			await That(Act).Should().NotThrow();
		}
	}
}
