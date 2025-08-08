using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public class IsNotNullOrEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenArrayContainsValues_ShouldSucceed()
			{
				string[] subject = ["foo",];

				async Task Act()
					=> await That(subject).IsNotNullOrEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenArrayIsEmpty_ShouldFail()
			{
				string[] subject = [];

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
			public async Task WhenEnumerableContainsValues_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 2,]);

				async Task Act()
					=> await That(subject).IsNotNullOrEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable((int[]) []);

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
				IEnumerable<string>? subject = null;

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