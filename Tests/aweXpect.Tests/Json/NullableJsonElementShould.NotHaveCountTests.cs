#if NET8_0_OR_GREATER
using System.Text.Json;

namespace aweXpect.Tests.Json;

public sealed partial class NullableJsonElementShould
{
	public sealed class NotHaveCount
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				JsonElement? subject = FromString("[]");

				async Task Act()
					=> await That(subject).Should().NotHaveCount(-1);

				await That(Act).Should().Throw<ArgumentOutOfRangeException>()
					.WithMessage("*must be a non-negative value*").AsWildcard().And
					.WithParamName("unexpected");
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				JsonElement? subject = null;

				async Task Act()
					=> await That(subject).Should().NotHaveCount(0);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have 0 items,
					             but it was <null>
					             """);
			}

			[Theory]
			[InlineData("1", "had type Number")]
			[InlineData("true", "had type True")]
			[InlineData("false", "had type False")]
			[InlineData("null", "had type Null")]
			[InlineData("\"foo\"", "had type String")]
			public async Task WhenTypeIsNotArrayOrObject_ShouldFail(string json, string failureMessage)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().NotHaveCount(1);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have 1 items,
					              but it {failureMessage}
					              """);
			}
		}

		public sealed class ArrayTests
		{
			[Theory]
			[InlineData("[]", 1)]
			[InlineData("[1]", 0)]
			[InlineData("[1, 2, 3]", 2)]
			public async Task WhenCountDiffersFromTheNumberOfArrayItems_ShouldSucceed(string json, int unexpectedCount)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().NotHaveCount(unexpectedCount);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData("[]", 0)]
			[InlineData("[1]", 1)]
			[InlineData("[1, 2, 3]", 3)]
			public async Task WhenCountMatchesTheNumberOfArrayItems_ShouldFail(string json, int unexpectedCount)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().NotHaveCount(unexpectedCount);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have {unexpectedCount} items,
					              but it did
					              """);
			}
		}

		public sealed class ObjectTests
		{
			[Theory]
			[InlineData("{}", 1)]
			[InlineData("{\"foo\": 1}", 0)]
			[InlineData("{\"foo\": 1, \"bar\": 2, \"baz\": 3}", 2)]
			public async Task WhenCountDiffersFromTheNumberOfObjectElements_ShouldSucceed(
				string json, int unexpectedCount)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().NotHaveCount(unexpectedCount);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData("{}", 0)]
			[InlineData("{\"foo\": 1}", 1)]
			[InlineData("{\"foo\": 1, \"bar\": 2, \"baz\": 3}", 3)]
			public async Task WhenCountMatchesTheNumberOfObjectElements_ShouldFail(string json, int unexpectedCount)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Should().NotHaveCount(unexpectedCount);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have {unexpectedCount} items,
					              but it did
					              """);
			}
		}
	}
}
#endif
