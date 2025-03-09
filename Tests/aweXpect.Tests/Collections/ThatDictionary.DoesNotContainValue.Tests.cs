using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatDictionary
{
	public sealed class DoesNotContainValue
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject).DoesNotContainValue("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain value "foo",
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenValueExists_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3,], [41, 42, 43,]);

				async Task Act()
					=> await That(subject).DoesNotContainValue(42);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain value 42,
					             but it did
					             """);
			}

			[Fact]
			public async Task WhenValueIsMissing_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3,], [41, 42, 43,]);

				async Task Act()
					=> await That(subject).DoesNotContainValue(2);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
