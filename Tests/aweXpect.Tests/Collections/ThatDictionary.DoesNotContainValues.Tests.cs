using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatDictionary
{
	public sealed class DoesNotContainValues
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAllValuesDoNotExist_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3,], [41, 42, 43,]);

				async Task Act()
					=> await That(subject).DoesNotContainValues(0, 2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAtLeastOneValueExists_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3,], [41, 42, 43,]);

				async Task Act()
					=> await That(subject).DoesNotContainValues(42, 2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain values [42, 2],
					             but it did contain [
					               42
					             ]
					             
					             Dictionary:
					             {[1] = 41, [2] = 42, [3] = 43}
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject).DoesNotContainValues("foo", "bar");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain values ["foo", "bar"],
					             but it was <null>
					             """);
			}
		}
	}
}
