﻿namespace aweXpect.Tests;

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

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsObject_ShouldFail()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsNull()
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is null, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
		public sealed class StructTests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				int? subject = null;

				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsObject_ShouldFail()
			{
				int? subject = 1;

				async Task Act()
					=> await That(subject).IsNull()
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is null, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
