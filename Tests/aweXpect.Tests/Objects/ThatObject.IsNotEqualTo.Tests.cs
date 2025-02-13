namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class IsNotEqualTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task SubjectToItself_ShouldFail()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsNotEqualTo(subject)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equal to subject, because we want to test the failure,
					             but it was MyClass {
					               Value = 0
					             }
					             """);
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldSucceed()
			{
				object subject = new MyClass();
				object expected = new MyClass();

				async Task Act()
					=> await That(subject).IsNotEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				MyClass? subject = null;
				MyClass? expected = null;

				async Task Act()
					=> await That(subject).IsNotEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equal to expected,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				MyClass? subject = null;

				async Task Act()
					=> await That(subject).IsNotEqualTo(new MyClass());

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class StructTests
		{
			[Fact]
			public async Task SubjectToItself_ShouldFail()
			{
				char subject = 'c';
				char unexpected = 'c';

				async Task Act()
					=> await That(subject).IsNotEqualTo(unexpected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equal to unexpected, because we want to test the failure,
					             but it was 'c'
					             """);
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldSucceed()
			{
				char subject = 'x';
				char expected = 'y';

				async Task Act()
					=> await That(subject).IsNotEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class NullableStructTests
		{
			[Fact]
			public async Task SubjectToItself_ShouldFail()
			{
				char? subject = 'c';
				char? unexpected = 'c';

				async Task Act()
					=> await That(subject).IsNotEqualTo(unexpected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equal to unexpected, because we want to test the failure,
					             but it was 'c'
					             """);
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldSucceed()
			{
				char? subject = 'x';
				char? expected = 'y';

				async Task Act()
					=> await That(subject).IsNotEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				char? subject = null;
				char? expected = null;

				async Task Act()
					=> await That(subject).IsNotEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equal to expected,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				char? subject = null;

				async Task Act()
					=> await That(subject).IsNotEqualTo('c');

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class TypeEqualsTests
		{
			[Fact]
			public async Task WhenValuesAreDifferent_ShouldSucceed()
			{
				Type sut = typeof(long);
				Type unexpected = typeof(int);

				async Task Act() => await That(sut).IsNotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenValuesAreSame_ShouldFail()
			{
				Type sut = typeof(float);
				Type unexpected = typeof(float);

				async Task Act() => await That(sut).IsNotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that sut
					             is not equal to unexpected,
					             but it was float
					             """);
			}
		}
	}
}
