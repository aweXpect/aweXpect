﻿namespace aweXpect.Tests;

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

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not null, because we want to test the failure,
					             but it was
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsObject_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class StructTests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				int? subject = null;

				async Task Act()
					=> await That(subject).IsNotNull()
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not null, because we want to test the failure,
					             but it was
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsObject_ShouldSucceed()
			{
				int? subject = 1;

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
