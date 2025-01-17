﻿#if NET8_0_OR_GREATER
using System.Text.Json;

namespace aweXpect.Tests;

public sealed partial class ThatNullableJsonElement
{
	public sealed class Matches
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData("true", true, true)]
			[InlineData("true", false, false)]
			[InlineData("false", true, false)]
			[InlineData("false", false, true)]
			public async Task BooleanValue_ShouldSucceedWhenMatching(string json, bool expected, bool isMatch)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(expected);

				await That(Act).Does().Throw<XunitException>().OnlyIf(!isMatch)
					.WithMessage($"""
					              Expected subject to
					              match expected,
					              but it differed as $ was {subject} instead of {expected}
					              """);
			}

			[Theory]
			[InlineData("42.1", 42.1, true)]
			[InlineData("1.2", 2.1, false)]
			public async Task DoubleValue_ShouldSucceedWhenMatching(string json, double expected, bool isMatch)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(expected);

				await That(Act).Does().Throw<XunitException>().OnlyIf(!isMatch)
					.WithMessage($"""
					              Expected subject to
					              match expected,
					              but it differed as $ was {json} instead of {Formatter.Format(expected)}
					              """);
			}

			[Theory]
			[InlineData("42", 42, true)]
			[InlineData("1", 2, false)]
			public async Task IntegerValue_ShouldSucceedWhenMatching(string json, int expected, bool isMatch)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(expected);

				await That(Act).Does().Throw<XunitException>().OnlyIf(!isMatch)
					.WithMessage($"""
					              Expected subject to
					              match expected,
					              but it differed as $ was {json} instead of {expected}
					              """);
			}

			[Theory]
			[InlineData("null", true)]
			[InlineData("{}", false)]
			public async Task NullValue_ShouldSucceedWhenMatching(string json, bool isMatch)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(null);

				await That(Act).Does().Throw<XunitException>().OnlyIf(!isMatch)
					.WithMessage($"""
					              Expected subject to
					              match null,
					              but it differed as $ was object {json} instead of Null
					              """);
			}

			[Theory]
			[InlineData("\"foo\"", "foo", true)]
			[InlineData("\"foo\"", "bar", false)]
			public async Task StringValue_ShouldSucceedWhenMatching(string json, string expected, bool isMatch)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(expected);

				await That(Act).Does().Throw<XunitException>().OnlyIf(!isMatch)
					.WithMessage($"""
					              Expected subject to
					              match expected,
					              but it differed as $ was {json} instead of "{expected}"
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				JsonElement? subject = null;

				async Task Act()
					=> await That(subject).Matches(new object());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match new object(),
					             but it was <null>
					             """);
			}
		}
		public sealed class ArrayTests
		{
			[Theory]
			[MemberData(nameof(MatchingArrayValues))]
			public async Task MatchingValues_ShouldSucceed(object expected, string json)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(expected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[MemberData(nameof(NotMatchingArrayValues))]
			public async Task NotMatchingValues_ShouldFail(object expected, string json, string errorMessage)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              match expected,
					              but it differed {errorMessage}
					              """);
			}

			[Fact]
			public async Task WhenElementsAreInDifferentOrder_ShouldFail()
			{
				JsonElement? subject = FromString("[1, 2]");

				async Task Act()
					=> await That(subject).Matches([2, 1]);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match [2, 1],
					             but it differed as
					               $[0] was 1 instead of 2 and
					               $[1] was 2 instead of 1
					             """);
			}

			[Fact]
			public async Task WhenExpectedContainsAdditionalElements_ShouldFail()
			{
				JsonElement? subject = FromString("[1, 2]");

				async Task Act()
					=> await That(subject).Matches([1, 2, 3]);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match [1, 2, 3],
					             but it differed as $[2] had missing 3
					             """);
			}

			[Fact]
			public async Task WhenSubjectContainsAdditionalElements_ShouldSucceed()
			{
				JsonElement? subject = FromString("[1, 2, 3]");

				async Task Act()
					=> await That(subject).Matches([1, 2]);

				await That(Act).Does().NotThrow();
			}

			public static TheoryData<object, string> MatchingArrayValues
				=> new()
				{
					{
						Array.Empty<string>(), "[]"
					},
					{
						Array.Empty<string>(), "[\"foo\"]"
					},
					{
						Array.Empty<int>(), "[]"
					},
					{
						Array.Empty<int>(), "[1, 2]"
					},
					{
						new[]
						{
							"foo", "bar"
						},
						"[\"foo\", \"bar\"]"
					}
				};

			public static TheoryData<object, string, string> NotMatchingArrayValues
				=> new()
				{
					{
						new[]
						{
							"foo"
						},
						"[]", "as $[0] had missing \"foo\""
					},
					{
						new[]
						{
							"bar", "foo"
						},
						"[\"foo\", \"bar\"]", """
						                      as
						                        $[0] was "foo" instead of "bar" and
						                        $[1] was "bar" instead of "foo"
						                      """
					}
				};
		}

		public sealed class ObjectTests
		{
			[Theory]
			[InlineData("{}", "$.foo was missing")]
			[InlineData("{\"foo\": 2}")]
			public async Task ShouldFailIfPropertyIsMissing(string json,
				string? errorMessage = null)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(new
					{
						foo = 2
					});

				await That(Act).Does().Throw<XunitException>().OnlyIf(errorMessage != null)
					.WithMessage($$"""
					               Expected subject to
					               match new
					               					{
					               						foo = 2
					               					},
					               but it differed as {{errorMessage}}
					               """);
			}

			[Theory]
			[InlineData("{}")]
			[InlineData("{\"foo\": 1}")]
			public async Task WhenExpectedIsEmpty_ShouldSucceed(string json)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).Matches(new object());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenPropertyHasDifferentValue_ShouldFail()
			{
				JsonElement? subject = FromString("{\"bar\": 2}");

				async Task Act()
					=> await That(subject).Matches(new
					{
						bar = 3
					});

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match new
					             					{
					             						bar = 3
					             					},
					             but it differed as $.bar was 2 instead of 3
					             """);
			}

			[Fact]
			public async Task WhenSubjectHasAdditionalProperties_ShouldSucceed()
			{
				JsonElement? subject = FromString("{\"foo\": null, \"bar\": 2}");

				async Task Act()
					=> await That(subject).Matches(new
					{
						bar = 2
					});

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif