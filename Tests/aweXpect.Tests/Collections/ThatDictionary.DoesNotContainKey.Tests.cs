using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatDictionary
{
	public sealed class DoesNotContainKey
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenKeyExists_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).DoesNotContainKey(2);

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).DoesNotContainKey(42);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<string, int>? subject = null;

				async Task Act()
					=> await That(subject!).DoesNotContainKey("foo");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have key "foo",
					             but it was <null>
					             """);
			}
		}
	}
}
