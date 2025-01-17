namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed class IsNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsObject_ShouldFail()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsNull()
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be null, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
