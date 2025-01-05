#if NET8_0_OR_GREATER
using System.Text.Json;

namespace aweXpect.Tests.Json;

public sealed partial class JsonElementShould
{
	public sealed class HaveCount
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				JsonElement subject = FromString("[]");

				async Task Act()
					=> await That(subject).Should().HaveCount(-1);

				await That(Act).Should().Throw<ArgumentOutOfRangeException>()
					.WithMessage("*must be a non-negative value*").AsWildcard().And
					.WithParamName("expected");
			}

			[Theory]
			[InlineData("1", "had type Number")]
			[InlineData("true", "had type True")]
			[InlineData("false", "had type False")]
			[InlineData("null", "had type Null")]
			[InlineData("\"foo\"", "had type String")]
			public async Task WhenTypeIsNotArrayOrObject_ShouldFail(string json, string failureMessage)
			{
				JsonElement subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().HaveCount(1);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have 1 items,
					              but it {failureMessage}
					              """);
			}
		}

		public sealed class ArrayTests
		{
			[Theory]
			[InlineData("[]", 0, 1)]
			[InlineData("[1]", 1, 0)]
			[InlineData("[1, 2, 3]", 3, 2)]
			public async Task WhenCountDiffersFromTheNumberOfArrayItems_ShouldFail(string json, int actualCount,
				int expectedCount)
			{
				JsonElement subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().HaveCount(expectedCount);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have {expectedCount} items,
					              but it had {actualCount}
					              """);
			}

			[Theory]
			[InlineData("[]", 0)]
			[InlineData("[1]", 1)]
			[InlineData("[1, 2, 3]", 3)]
			public async Task WhenCountMatchesTheNumberOfArrayItems_ShouldSucceed(string json, int expectedCount)
			{
				JsonElement subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().HaveCount(expectedCount);

				await That(Act).Should().NotThrow();
			}
		}

		public sealed class ObjectTests
		{
			[Theory]
			[InlineData("{}", 0, 1)]
			[InlineData("{\"foo\": 1}", 1, 0)]
			[InlineData("{\"foo\": 1, \"bar\": 2, \"baz\": 3}", 3, 2)]
			public async Task WhenCountDiffersFromTheNumberOfObjectElements_ShouldFail(string json, int actualCount,
				int expectedCount)
			{
				JsonElement subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().HaveCount(expectedCount);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have {expectedCount} items,
					              but it had {actualCount}
					              """);
			}

			[Theory]
			[InlineData("{}", 0)]
			[InlineData("{\"foo\": 1}", 1)]
			[InlineData("{\"foo\": 1, \"bar\": 2, \"baz\": 3}", 3)]
			public async Task WhenCountMatchesTheNumberOfObjectElements_ShouldSucceed(string json, int expectedCount)
			{
				JsonElement subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().HaveCount(expectedCount);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
#endif
