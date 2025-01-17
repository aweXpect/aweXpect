namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class Is
	{
		public sealed partial class EqualTo
		{
			public sealed class Tests
			{
				[Fact]
				public async Task SubjectToItself_ShouldSucceed()
				{
					object subject = new MyClass();

					async Task Act()
						=> await That(subject).Is(subject);

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task SubjectToSomeOtherValue_ShouldFail()
				{
					object subject = new MyClass();
					object expected = new MyClass();

					async Task Act()
						=> await That(subject).Is(expected)
							.Because("we want to test the failure");

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be equal to expected, because we want to test the failure,
						             but it was MyClass {
						               Value = 0
						             }
						             """);
				}

				[Fact]
				public async Task WhenSubjectAndExpectedIsNull_ShouldSucceed()
				{
					MyClass? subject = null;
					MyClass? expected = null;

					async Task Act()
						=> await That(subject).Is(expected);

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					MyClass? subject = null;

					async Task Act()
						=> await That(subject).Is(new MyClass());

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be equal to new MyClass(),
						             but it was <null>
						             """);
				}
			}
		}
	}
}
