namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class Is
	{
		public sealed class NotEqualTo
		{
			public sealed class Tests
			{
				[Fact]
				public async Task SubjectToItself_ShouldFail()
				{
					object subject = new MyClass();

					async Task Act()
						=> await That(subject).Is().NotEqualTo(subject)
							.Because("we want to test the failure");

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             not be equal to subject, because we want to test the failure,
						             but it was MyClass {
						               Value = 0
						             }
						             """);
				}

				[Fact]
				public async Task SubjectToSomeOtherValue_ShouldSucceed()
				{
					object subject = new MyClass();
					object unexpected = new MyClass();

					async Task Act()
						=> await That(subject).Is().NotEqualTo(unexpected);

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
				{
					MyClass? subject = null;
					MyClass? expected = null;

					async Task Act()
						=> await That(subject).Is().NotEqualTo(expected);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             not be equal to expected,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldSucceed()
				{
					MyClass? subject = null;

					async Task Act()
						=> await That(subject).Is().NotEqualTo(new MyClass());

					await That(Act).Does().NotThrow();
				}
			}
		}
	}
}
