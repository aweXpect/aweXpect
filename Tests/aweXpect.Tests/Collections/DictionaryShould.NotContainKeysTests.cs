using System.Collections.Generic;

namespace aweXpect.Tests.Collections;

public sealed partial class DictionaryShould
{
	public sealed class NotContainKeys
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAllKeysDoNotExist_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).Should().NotContainKeys(42, 43);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenAtLeastOneKeyExists_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).Should().NotContainKeys(42, 2);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have keys [42, 2],
					             but it did have [
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<string, int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotContainKeys("foo", "bar");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have keys ["foo", "bar"],
					             but it was <null>
					             """);
			}
		}
	}
}
