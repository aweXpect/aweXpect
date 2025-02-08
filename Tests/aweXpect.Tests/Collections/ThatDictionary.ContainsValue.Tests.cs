using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatDictionary
{
	public sealed class ContainsValue
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject).ContainsValue("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has value "foo",
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenValueExists_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).ContainsValue(42);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenValueIsMissing_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [41, 42, 43]);

				async Task Act()
					=> await That(subject).ContainsValue(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has value 2,
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
