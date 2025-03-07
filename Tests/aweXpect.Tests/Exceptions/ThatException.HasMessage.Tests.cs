﻿namespace aweXpect.Tests;

public sealed partial class ThatException
{
	public sealed class HasMessage
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenStringsAreEqual_ShouldSucceed(string actual)
			{
				Exception subject = new(actual);

				async Task Act()
					=> await That(subject).HasMessage(actual);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenStringsDiffer_ShouldFail()
			{
				string actual = "actual text";
				string expected = "expected other text";
				Exception subject = new(actual);

				async Task Act()
					=> await That(subject).HasMessage(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has Message equal to "expected other text",
					             but it was "actual text" which differs at index 0:
					                ↓ (actual)
					               "actual text"
					               "expected other text"
					                ↑ (expected)
					             
					             Message:
					             actual text
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Exception? subject = null;

				async Task Act()
					=> await That(subject).HasMessage("expected text");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has Message equal to "expected text",
					             but it was <null>
					             """);
			}
		}
	}
}
