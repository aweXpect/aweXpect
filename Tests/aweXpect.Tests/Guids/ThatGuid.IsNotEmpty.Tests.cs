namespace aweXpect.Tests;

public sealed partial class ThatGuid
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

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be empty,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotEmpty_ShouldSucceed()
			{
				Guid subject = OtherGuid();

				async Task Act()
					=> await That(subject).IsNotEmpty();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
