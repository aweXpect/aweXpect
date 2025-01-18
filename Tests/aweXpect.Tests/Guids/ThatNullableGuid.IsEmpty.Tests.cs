namespace aweXpect.Tests;

public sealed partial class ThatNullableGuid
{
	public sealed class IsEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsEmpty_ShouldSucceed()
			{
				Guid subject = Guid.Empty;

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNotEmpty_ShouldFail()
			{
				Guid? subject = OtherGuid();

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be empty,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be empty,
					             but it was <null>
					             """);
			}
		}
	}
}
