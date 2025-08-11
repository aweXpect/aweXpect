﻿namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class Throws
	{
		public class WhichTests
		{
			[Fact(Skip = "TODO: Remove after core version update")]
			public async Task ShouldGiveAccessToThrowsException()
			{
				MyException exception = new();
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Throws<MyException>()
						.Which.IsSameAs(exception);

				await That(Act).DoesNotThrow();
			}

			[Fact(Skip = "TODO: Remove after core version update")]
			public async Task ShouldIncludeWhichInErrorMessage()
			{
				MyException exception = new();
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Throws<MyException>()
						.Which.For(h => h.Message, r => r.IsEqualTo("foo"));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that Delegate
					             throws a MyException which for .Message is equal to "foo",
					             but .Message was "ShouldIncludeWhichInErrorMessa…" which differs at index 0:
					                ↓ (actual)
					               "ShouldIncludeWhichInErrorMessage"
					               "foo"
					                ↑ (expected)

					             Actual:
					             ShouldIncludeWhichInErrorMessage
					             """);
			}

			[Fact(Skip = "TODO: Remove after core version update")]
			public async Task ShouldSupportWhichSatisfies()
			{
				MyException exception = new();
				void Act() => throw exception;
				await That(Act)
					.Throws<MyException>().Which
					.Satisfies(x => x.Message == nameof(ShouldSupportWhichSatisfies));
			}
		}
	}
}
