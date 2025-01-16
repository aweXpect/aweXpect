using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class DictionaryShould
{
	public sealed class NotContainValue
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotContainValue("foo");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have value "foo",
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenValueExists_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).Should().NotContainValue(42);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have value 42,
					             but it did
					             """);
			}

			[Fact]
			public async Task WhenValueIsMissing_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).Should().NotContainValue(2);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
