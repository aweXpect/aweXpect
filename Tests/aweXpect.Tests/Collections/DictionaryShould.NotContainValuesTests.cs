using System.Collections.Generic;

namespace aweXpect.Tests.Collections;

public sealed partial class DictionaryShould
{
	public sealed class NotContainValues
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAllValuesDoNotExist_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).Should().NotContainValues(0, 2);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenAtLeastOneValueExists_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).Should().NotContainValues(42, 2);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have values [42, 2],
					             but it did have [
					               42
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotContainValues("foo", "bar");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have values ["foo", "bar"],
					             but it was <null>
					             """);
			}
		}
	}
}
