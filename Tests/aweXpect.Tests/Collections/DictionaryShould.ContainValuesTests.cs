using System.Collections.Generic;

namespace aweXpect.Tests.Collections;

public sealed partial class DictionaryShould
{
	public sealed class ContainValues
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAllValuesExists_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).Should().ContainValues(42, 41);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDictionaryIsNull_ShouldFail()
			{
				IDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ContainValues("foo", "bar");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have values ["foo", "bar"],
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenOneValueIsMissing_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).Should().ContainValues(42, 2);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have values [42, 2],
					             but it did not have [
					               2
					             ] in [
					               41,
					               42,
					               43
					             ]
					             """);
			}
		}
	}
}
