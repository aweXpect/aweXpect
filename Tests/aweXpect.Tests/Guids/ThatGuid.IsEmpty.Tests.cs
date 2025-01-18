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

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNotEmpty_ShouldFail()
			{
				Guid subject = OtherGuid();

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be empty,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
