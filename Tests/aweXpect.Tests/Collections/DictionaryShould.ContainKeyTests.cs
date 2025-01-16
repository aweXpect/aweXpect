using System.Collections.Generic;

namespace aweXpect.Tests.Collections;

public sealed partial class DictionaryShould
{
	public sealed class ContainKey
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenKeyExists_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).Should().ContainKey(2);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenKeyIsMissing_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).Should().ContainKey(0);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have key 0,
					             but it contained only [
					               1,
					               2,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<string, int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ContainKey("foo");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have key "foo",
					             but it was <null>
					             """);
			}
		}
	}
}
