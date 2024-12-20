#if NET8_0_OR_GREATER
namespace aweXpect.Tests.ThatTests.TimeOnlys;

public sealed partial class NullableTimeOnlyShould
{
	public sealed class BeTests
	{
		[Fact]
		public async Task WhenOnlyExpectedIsNull_ShouldFail()
		{
			TimeOnly? subject = CurrentTime();
			TimeOnly? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenOnlySubjectIsNull_ShouldFail()
		{
			TimeOnly? subject = null;
			TimeOnly? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedIsNull_ShouldSucceed()
		{
			TimeOnly? subject = null;
			TimeOnly? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldFail()
		{
			TimeOnly? subject = CurrentTime();
			TimeOnly? expected = LaterTime();

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsTheSame_ShouldSucceed()
		{
			TimeOnly? subject = CurrentTime();
			TimeOnly? expected = subject;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(3, 2, true)]
		[InlineData(5, 3, true)]
		[InlineData(2, 2, false)]
		[InlineData(0, 2, false)]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail(
			int actualDifference, int toleranceSeconds, bool expectToThrow)
		{
			TimeSpan tolerance = TimeSpan.FromSeconds(toleranceSeconds);
			TimeOnly? subject = EarlierTime(actualDifference);
			TimeOnly? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().Be(expected)
					.Within(tolerance)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.OnlyIf(expectToThrow)
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)} ± {Formatter.Format(tolerance)}, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}

	public sealed class NotBeTests
	{
		[Fact]
		public async Task WhenOnlySubjectIsNull_ShouldSucceed()
		{
			TimeOnly? subject = null;
			TimeOnly? unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenOnlyUnexpectedIsNull_ShouldSucceed()
		{
			TimeOnly? subject = CurrentTime();
			TimeOnly? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndUnexpectedIsNull_ShouldFail()
		{
			TimeOnly? subject = null;
			TimeOnly? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be <null>,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldSucceed()
		{
			TimeOnly? subject = CurrentTime();
			TimeOnly? unexpected = LaterTime();

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsTheSame_ShouldFail()
		{
			TimeOnly? subject = CurrentTime();
			TimeOnly? unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(3, 2, false)]
		[InlineData(5, 3, false)]
		[InlineData(2, 2, true)]
		[InlineData(0, 2, true)]
		public async Task Within_WhenValuesAreInsideTheTolerance_ShouldFail(
			int actualDifference, int toleranceSeconds, bool expectToThrow)
		{
			TimeSpan tolerance = TimeSpan.FromSeconds(toleranceSeconds);
			TimeOnly? subject = EarlierTime(actualDifference);
			TimeOnly? unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Within(tolerance)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.OnlyIf(expectToThrow)
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)} ± {Formatter.Format(tolerance)}, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}
}
#endif
