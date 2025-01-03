﻿using System.Collections.Generic;

namespace aweXpect.Tests.Collections;

public sealed partial class DictionaryShould
{
	public sealed class ContainValue
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ContainValue("foo");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have value "foo",
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenValueExists_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).Should().ContainValue(42);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenValueIsMissing_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).Should().ContainValue(2);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have value 2,
					             but it contained only [
					               1,
					               2,
					               3
					             ]
					             """);
			}
		}
	}
}
