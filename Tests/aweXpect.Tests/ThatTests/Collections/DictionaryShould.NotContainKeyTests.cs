using System.Collections.Generic;

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class DictionaryShould
{
	public sealed class NotContainKey
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenDictionaryIsNull_ShouldFail()
			{
				IDictionary<string, int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotContainKey("foo");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have key "foo",
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenKeyExists_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).Should().NotContainKey(2);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have key 2,
					             but it did
					             """);
			}

			[Fact]
			public async Task WhenKeyIsMissing_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).Should().NotContainKey(42);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
