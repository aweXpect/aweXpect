#if NET8_0_OR_GREATER
using System.Collections.Immutable;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class IsEmpty
	{
		public sealed class ImmutableTests
		{
			[Fact]
			public async Task WhenArrayContainsValues_ShouldFail()
			{
				ImmutableArray<string> subject = ["foo",];

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is empty,
					             but it was [
					               "foo"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenArrayIsEmpty_ShouldSucceed()
			{
				ImmutableArray<string> subject = [];

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsValues_ShouldFail()
			{
				ImmutableArray<int> subject = [1, 1, 2,];

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is empty,
					             but it was [
					               1,
					               1,
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldSucceed()
			{
				ImmutableArray<int> subject = [];

				async Task Act()
					=> await That(subject).IsEmpty();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
