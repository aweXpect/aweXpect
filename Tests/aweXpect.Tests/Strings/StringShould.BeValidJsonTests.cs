#if NET8_0_OR_GREATER
namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public class BeValidJson
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task ShouldUseProvidedOptions(bool allowTrailingCommas)
			{
				string subject = "[1, 2,]";

				async Task Act()
					=> await That(subject).Should().BeValidJson(o => o with
					{
						AllowTrailingCommas = allowTrailingCommas
					});

				await That(Act).Should().Throw<XunitException>().OnlyIf(!allowTrailingCommas);
			}

			[Fact]
			public async Task WhenActualIsEmpty_ShouldFail()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).Should().BeValidJson();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be valid JSON,
					             but it could not be parsed: The input does not contain any JSON tokens.*
					             """).AsWildcard();
			}

			[Theory]
			[InlineData("True", "'T' is an invalid start of a value")]
			[InlineData("False", "'F' is an invalid start of a value")]
			[InlineData("Null", "'N' is an invalid start of a value")]
			[InlineData("{\"foo\":1}}", "'}' is invalid after a single JSON value. Expected end of data")]
			[InlineData("{}{\"foo\":1}", "'{' is invalid after a single JSON value. Expected end of data")]
			[InlineData("[",
				"Expected depth to be zero at the end of the JSON payload. There is an open JSON object or array that should be closed")]
			public async Task WhenActualIsInvalidJson_ShouldFail(string subject, string errorMessage)
			{
				async Task Act()
					=> await That(subject).Should().BeValidJson();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be valid JSON,
					              but it could not be parsed: {errorMessage}*
					              """).AsWildcard();
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldFail()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).Should().BeValidJson();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be valid JSON,
					             but it was <null>
					             """);
			}

			[Theory]
			[InlineData("42")]
			[InlineData("\"foo\"")]
			[InlineData("true")]
			[InlineData("false")]
			[InlineData("null")]
			[InlineData("[]")]
			[InlineData("[1]")]
			[InlineData("[\"foo\", \"bar\"]")]
			[InlineData("{}")]
			[InlineData("{\"foo\": null}")]
			[InlineData("{\"foo\": {\"bar\":[1,2,3], \"baz\": true,}}")]
			public async Task WhenActualIsValidJson_ShouldSucceed(string subject)
			{
				async Task Act()
					=> await That(subject).Should().BeValidJson();

				await That(Act).Should().NotThrow();
			}
		}

		public sealed class WhichTests
		{
			[Fact]
			public async Task WhenExpectationInWhichFails_ShouldFail()
			{
				string subject = "[1, 2]";

				async Task Act()
					=> await That(subject).Should().BeValidJson().Which(d => d.Should().HaveCount(3));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be valid JSON which should have 3 items,
					             but it had 2
					             """);
			}

			[Fact]
			public async Task WhenExpectationInWhichIsSatisfied_ShouldSucceed()
			{
				string subject = "[1, 2]";

				async Task Act()
					=> await That(subject).Should().BeValidJson().Which(d => d.Should().HaveCount(2));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
#endif
