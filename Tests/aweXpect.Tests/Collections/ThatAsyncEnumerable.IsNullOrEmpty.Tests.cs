using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public class IsNullOrEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAsyncEnumerableContainsValues_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).IsNullOrEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is null or empty,
					             but it was [
					               1,
					               2,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenAsyncEnumerableIsEmpty_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable((int[]) []);

				async Task Act()
					=> await That(subject).IsNullOrEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).IsNullOrEmpty();

				await That(Act).DoesNotThrow();
			}
		}
	}
}