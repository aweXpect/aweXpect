﻿namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class DoesNotEndWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task IgnoringCase_WhenSubjectDoesEndWithExpected_ShouldFail()
			{
				string subject = "some text";
				string expected = "TEXT";

				async Task Act()
					=> await That(subject).DoesNotEndWith(expected).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with "TEXT" ignoring case,
					             but it was "some text"
					             """);
			}

			[Fact]
			public async Task IgnoringCase_WhenSubjectDoesNotEndWithWithExpected_ShouldSucceed()
			{
				string subject = "some text";
				string expected = "SOME";

				async Task Act()
					=> await That(subject).DoesNotEndWith(expected).IgnoringCase();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task
				Using_WhenSubjectDoesEndWithExpected_ShouldFail()
			{
				string subject = "some arbitrary text";
				string expected = "tExt";

				async Task Act()
					=> await That(subject).DoesNotEndWith(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with "tExt" using IgnoreCaseForVocalsComparer,
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task
				Using_WhenSubjectDoesNotEndWithWithExpected_ShouldSucceed()
			{
				string subject = "some arbitrary text";
				string expected = "TEXT";

				async Task Act()
					=> await That(subject).DoesNotEndWith(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectDoesEndWithExpected_ShouldFail()
			{
				string subject = "some text";
				string expected = "text";

				async Task Act()
					=> await That(subject).DoesNotEndWith(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with "text",
					             but it was "some text"
					             """);
			}

			[Fact]
			public async Task WhenSubjectDoesNotEndWithWithExpected_ShouldSucceed()
			{
				string subject = "some text";
				string expected = "some";

				async Task Act()
					=> await That(subject).DoesNotEndWith(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
