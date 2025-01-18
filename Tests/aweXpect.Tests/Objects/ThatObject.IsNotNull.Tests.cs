namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed class IsNotNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).IsNotNull()
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be null, because we want to test the failure,
					             but it was
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsObject_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
