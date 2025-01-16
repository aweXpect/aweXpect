namespace aweXpect.Tests.Guids;

public sealed partial class NullableGuidShould
{
	public sealed class NotBeNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsEmpty_ShouldSucceed()
			{
				Guid? subject = Guid.Empty;

				async Task Act()
					=> await That(subject).Should().NotBeNull();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNotNull_ShouldSucceed()
			{
				Guid? subject = OtherGuid();

				async Task Act()
					=> await That(subject).Should().NotBeNull();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeNull();

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
