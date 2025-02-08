namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class IsEqualTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task SubjectToItself_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsEqualTo(subject);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldFail()
			{
				object subject = new MyClass();
				object expected = new MyClass();

				async Task Act()
					=> await That(subject).IsEqualTo(expected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
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
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				MyClass? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(new MyClass());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be equal to new MyClass(),
					             but it was <null>
					             """);
			}
		}

		public sealed class StructTests
		{
			[Fact]
			public async Task SubjectToItself_ShouldSucceed()
			{
				char subject = 'c';

				async Task Act()
					=> await That(subject).IsEqualTo(subject);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldFail()
			{
				char subject = 'x';
				char expected = 'y';

				async Task Act()
					=> await That(subject).IsEqualTo(expected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be equal to expected, because we want to test the failure,
					             but it was 'x'
					             """);
			}
		}

		public sealed class NullableStructTests
		{
			[Fact]
			public async Task SubjectToItself_ShouldSucceed()
			{
				char? subject = 'c';

				async Task Act()
					=> await That(subject).IsEqualTo(subject);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldFail()
			{
				char? subject = 'x';
				char? expected = 'y';

				async Task Act()
					=> await That(subject).IsEqualTo(expected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be equal to expected, because we want to test the failure,
					             but it was 'x'
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldSucceed()
			{
				char? subject = null;
				char? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				char? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo('c');

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be equal to 'c',
					             but it was <null>
					             """);
			}
		}

		public sealed class TypeEqualsTests
		{
			[Fact]
			public async Task WhenValuesAreDifferent_ShouldFail()
			{
				Type sut = typeof(long);
				Type expected = typeof(int);

				async Task Act() => await That(sut).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that sut
					             be equal to expected,
					             but it was long
					             """);
			}

			[Fact]
			public async Task WhenValuesAreSame_ShouldSucceed()
			{
				Type sut = typeof(float);
				Type expected = typeof(float);

				async Task Act() => await That(sut).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
