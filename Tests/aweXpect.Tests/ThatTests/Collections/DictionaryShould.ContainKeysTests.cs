using System.Collections.Generic;

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class DictionaryShould
{
	public sealed class ContainKeys
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAllKeysExists_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).Should().ContainKeys(2, 1);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDictionaryIsNull_ShouldFail()
			{
				IDictionary<string, int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ContainKeys("foo", "bar");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have keys ["foo", "bar"],
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenOneKeyIsMissing_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).Should().ContainKeys(0, 2);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have keys [0, 2],
					             but it did not have [
					               0
					             ] in [
					               1,
					               2,
					               3
					             ]
					             """);
			}
		}
	}
}
