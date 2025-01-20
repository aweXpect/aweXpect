namespace aweXpect.Tests;

public sealed partial class ThatNullableGuid
{
	public sealed class Is
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreNull_ShouldSucceed()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).Is(null);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldFail()
			{
				Guid? subject = FixedGuid();
				Guid? expected = OtherGuid();

				async Task Act()
					=> await That(subject).Is(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).Is(FixedGuid());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {FixedGuid()},
					              but it was <null>
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldSucceed()
			{
				Guid? subject = FixedGuid();
				Guid? expected = subject;

				async Task Act()
					=> await That(subject).Is(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
