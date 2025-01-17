#if NET8_0_OR_GREATER
using System.Text.Json;

namespace aweXpect.Tests;

public sealed partial class ThatNullableJsonElement
{
	public sealed class IsObject
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData("[]", "an array")]
			[InlineData("2", "a number")]
			[InlineData("\"foo\"", "a string")]
			public async Task WhenJsonIsNoObject_ShouldFail(string json, string kindString)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).IsObject(o => o.With("foo").Matching(true));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be an object and $.foo match true,
					              but it was {kindString} instead of an object
					              """);
			}
		}

		public sealed class WithTests
		{
			[Fact]
			public async Task WhenMatchFails_ShouldFail()
			{
				JsonElement? subject = FromString("{\"foo\": 1}");

				async Task Act()
					=> await That(subject).IsObject(o => o.With("foo").Matching(2));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be an object and $.foo match 2,
					             but it differed as $.foo was 1 instead of 2
					             """);
			}

			[Fact]
			public async Task WhenMatchSucceeds_ShouldSucceed()
			{
				JsonElement? subject = FromString("{\"foo\": 2}");

				async Task Act()
					=> await That(subject).IsObject(o => o.With("foo").Matching(2));


				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenMultipleMatchesFail_ShouldListAllFailures()
			{
				JsonElement? subject = FromString("{\"foo\": 1, \"bar\": 2}");

				async Task Act()
					=> await That(subject).IsObject(o => o.With("foo").Matching(2).With("bar").Matching(1));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be an object and $.foo match 2 and $.bar match 1,
					             but it differed as
					               $.foo was 1 instead of 2 and
					               $.bar was 2 instead of 1
					             """);
			}

			[Fact]
			public async Task WhenNestedMatchesFail_ShouldListAllFailures()
			{
				JsonElement? subject = FromString("{\"foo\": 1, \"bar\": {\"baz\": 1, \"bat\": 2}}");

				async Task Act()
					=> await That(subject).IsObject(o => o
						.With("foo").Matching(2).And
						.With("bar").AnObject(p => p
							.With("baz").Matching(3).And
							.With("bat").Matching(3)));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be an object and $.foo match 2 and $.bar be an object and $.bar.baz match 3 and $.bar.bat match 3,
					             but it differed as
					               $.foo was 1 instead of 2 and
					               $.bar.baz was 1 instead of 3 and
					               $.bar.bat was 2 instead of 3
					             """);
			}

			[Fact]
			public async Task WhenPropertyDoesNotExist_ShouldFail()
			{
				JsonElement? subject = FromString("{\"foo\": 1}");

				async Task Act()
					=> await That(subject).IsObject(o => o.With("bar").Matching(true));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be an object and $.bar match true,
					             but it differed as property $.bar did not exist
					             """);
			}
		}

		public sealed class WithNumberOfPropertiesTests
		{
			[Fact]
			public async Task WhenNull_ShouldNotAddFailureMessage()
			{
				JsonElement? subject = FromString("{}");

				async Task Act()
					=> await That(subject).IsObject(o => o
						.With("foo").AnObject(a => a
							.With(1).Properties()));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be an object and $.foo be an object with 1 property,
					             but it differed as property $.foo did not exist
					             """);
			}

			[Fact]
			public async Task WhenNumberDiffers_ShouldFail()
			{
				JsonElement? subject = FromString("{\"foo\": 1, \"bar\": 2}");

				async Task Act()
					=> await That(subject).IsObject(o => o
						.With(3).Properties());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be an object with 3 properties,
					             but it differed as $ had 2 properties
					             """);
			}

			[Theory]
			[InlineData("{}", 0)]
			[InlineData("{\"foo\": 1}", 1)]
			[InlineData("{\"foo\": 1, \"bar\": 2, \"baz\": 3}", 3)]
			public async Task WhenNumberMatches_ShouldSucceed(string json, int expected)
			{
				JsonElement? subject = FromString(json);

				async Task Act()
					=> await That(subject).IsObject(o => o
						.With(expected).Properties());

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif
