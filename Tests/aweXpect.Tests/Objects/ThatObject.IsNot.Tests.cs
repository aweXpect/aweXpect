﻿namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class IsNot
	{
		public sealed class Tests
		{
			[Fact]
			public async Task SubjectToItself_ShouldFail()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsNot(subject)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
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
				object expected = new MyClass();

				async Task Act()
					=> await That(subject).IsNot(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				MyClass? subject = null;
				MyClass? expected = null;

				async Task Act()
					=> await That(subject).IsNot(expected);

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).IsNot(new MyClass());

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
					=> await That(subject).IsNot(unexpected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be equal to unexpected, because we want to test the failure,
					             but it was 'c'
					             """);
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldSucceed()
			{
				char subject = 'x';
				char expected = 'y';

				async Task Act()
					=> await That(subject).IsNot(expected);

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
					=> await That(subject).IsNot(unexpected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be equal to unexpected, because we want to test the failure,
					             but it was 'c'
					             """);
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldSucceed()
			{
				char? subject = 'x';
				char? expected = 'y';

				async Task Act()
					=> await That(subject).IsNot(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				char? subject = null;
				char? expected = null;

				async Task Act()
					=> await That(subject).IsNot(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be equal to expected,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				char? subject = null;

				async Task Act()
					=> await That(subject).IsNot('c');

				await That(Act).DoesNotThrow();
			}
		}
	}
}
