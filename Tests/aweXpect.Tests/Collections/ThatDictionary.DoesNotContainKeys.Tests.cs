﻿using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatDictionary
{
	public sealed class DoesNotContainKeys
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAllKeysDoNotExist_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).DoesNotContainKeys(42, 43);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenAtLeastOneKeyExists_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).DoesNotContainKeys(42, 2);

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject!).DoesNotContainKeys("foo", "bar");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have keys ["foo", "bar"],
					             but it was <null>
					             """);
			}
		}
	}
}