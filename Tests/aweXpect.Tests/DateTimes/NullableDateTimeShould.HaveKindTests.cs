namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class HaveKind
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenKindOfSubjectIsDifferent_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
				DateTimeKind expected = DateTimeKind.Local;

				async Task Act()
					=> await That(subject).Should().HaveKind(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have kind of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenKindOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
				DateTimeKind expected = DateTimeKind.Utc;

				async Task Act()
					=> await That(subject).Should().HaveKind(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				DateTime? subject = null;
				DateTimeKind expected = DateTimeKind.Utc;

				async Task Act()
					=> await That(subject).Should().HaveKind(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have kind of Utc,
					             but it was <null>
					             """);
			}
		}
	}
}
