using System.Collections.Generic;
using aweXpect.Equivalency;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed class IsNotOneOf
	{
		public sealed class Tests
		{
			[Fact]
			public async Task SubjectToItself_ShouldFail()
			{
				object subject = new MyClass();
				IEnumerable<object> unexpected = [new MyClass(), subject,];

				async Task Act()
					=> await That(subject).IsNotOneOf(unexpected)
						.Because("we want to test the failure");
				
				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equal to one of [ThatObject.MyClass { Value = 0 }, ThatObject.MyClass { Value = 0 }], because we want to test the failure,
					             but it was ThatObject.MyClass {
					                 Value = 0
					               }
					             """);
			}

			[Fact]
			public async Task SubjectToSomeOtherValue_ShouldSucceed()
			{
				object subject = new MyClass();
				object[] unexpected = [new MyClass(),];

				async Task Act()
					=> await That(subject).IsNotOneOf(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenComparingWithEquivalenceAndContainsEquivalentValue_ShouldFail()
			{
				object subject = new MyClass();
				object[] unexpected = [new MyClass(),];

				async Task Act()
					=> await That(subject).IsNotOneOf(unexpected).Equivalent();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equivalent to one of [ThatObject.MyClass { Value = 0 }],
					             but it was considered equivalent to ThatObject.MyClass {
					                 Value = 0
					               }
					             """);
			}

			[Fact]
			public async Task WhenExpectedOnlyContainsNullValues_ShouldSucceed()
			{
				MyClass subject = new();
				IEnumerable<object?> unexpected = [null,];

				async Task Act()
					=> await That(subject).IsNotOneOf(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				MyClass? subject = null;

				async Task Act()
					=> await That(subject).IsNotOneOf(new MyClass());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not equal to one of [ThatObject.MyClass { Value = 0 }],
					             but it was <null>
					             """);
			}
		}
	}
}
