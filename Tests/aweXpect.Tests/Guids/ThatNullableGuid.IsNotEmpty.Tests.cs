namespace aweXpect.Tests;

public sealed partial class ThatNullableGuid
{
	public sealed class IsNotEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsEmpty_ShouldFail()
			{
				Guid subject = Guid.Empty;

				async Task Act()
					=> await That(subject).IsNotEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not empty,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotEmpty_ShouldSucceed()
			{
				Guid? subject = OtherGuid();

				async Task Act()
					=> await That(subject).IsNotEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).IsNotEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not empty,
					             but it was <null>
					             """);
			}
		}
	}
}
