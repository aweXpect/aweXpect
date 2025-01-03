namespace aweXpect.Tests.Objects;

public sealed partial class ObjectShould
{
	public sealed partial class NotBe
	{
		public sealed class Tests
		{
			[Fact]
			public async Task SubjectToItself_ShouldFail()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBe(subject)
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
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
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				MyClass? subject = null;
				MyClass? expected = null;

				async Task Act()
					=> await That(subject).Should().NotBe(expected);

				await That(Act).Should().Throw<XunitException>()
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
					=> await That(subject).Should().NotBe(new MyClass());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
