using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumerable

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public class IsNotNullOrEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAsyncEnumerableContainsValues_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).IsNotNullOrEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAsyncEnumerableIsEmpty_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable((int[]) []);

				async Task Act()
					=> await That(subject).IsNotNullOrEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not null or empty,
					             but it was []
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).IsNotNullOrEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not null or empty,
					             but it was <null>
					             """);
			}
		}
	}
}