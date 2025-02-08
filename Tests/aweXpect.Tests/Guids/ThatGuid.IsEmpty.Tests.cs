namespace aweXpect.Tests;

public sealed partial class ThatGuid
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
				Guid subject = OtherGuid();

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be empty,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
