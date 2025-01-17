namespace aweXpect.Tests;

public sealed partial class ThatNullableGuid
{
	public sealed class IsNotNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsEmpty_ShouldSucceed()
			{
				Guid? subject = Guid.Empty;

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNotNull_ShouldSucceed()
			{
				Guid? subject = OtherGuid();

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be null,
					             but it was <null>
					             """);
			}
		}
	}
}
