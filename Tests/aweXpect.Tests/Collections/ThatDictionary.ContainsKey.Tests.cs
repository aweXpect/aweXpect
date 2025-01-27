using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatDictionary
{
	public sealed class ContainsKey
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenKeyExists_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).ContainsKey(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenKeyIsMissing_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).ContainsKey(0);

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).ContainsKey("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have key "foo",
					             but it was <null>
					             """);
			}
		}
	}
}
