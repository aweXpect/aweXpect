namespace aweXpect.Tests.Guids;

public sealed partial class NullableGuidShould
{
	public sealed class BeNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsEmpty_ShouldFail()
			{
				Guid? subject = Guid.Empty;

				async Task Act()
					=> await That(subject).Should().BeNull();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be null,
					             but it was 00000000-0000-0000-0000-000000000000
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNotNull_ShouldFail()
			{
				Guid? subject = OtherGuid();

				async Task Act()
					=> await That(subject).Should().BeNull();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be null,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).Should().BeNull();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
