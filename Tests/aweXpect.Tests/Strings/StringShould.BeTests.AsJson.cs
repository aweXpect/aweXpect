#if NET8_0_OR_GREATER
namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public sealed partial class Be
	{
		public sealed class AsJsonTests
		{
			[Fact]
			public async Task WhenStringsAreDifferentValidJson_ShouldFail()
			{
				string subject = """
				                 { "foo": 1 }
				                 """;
				string expected = """
				                  {
				                    "foo": 2
				                  }
				                  """;

				async Task Act()
					=> await That(subject).Should().Be(expected).AsJson();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be JSON equivalent to {
					               "foo": 2
					             },
					             but it differed in:
					              - .foo was 1 instead of 2
					             """);
			}

			[Fact]
			public async Task WhenStringsAreSameValidJson_ShouldSucceed()
			{
				string subject = """
				                 { "foo": 1 }
				                 """;
				string expected = """
				                  {
				                    "foo": 1
				                  }
				                  """;

				async Task Act()
					=> await That(subject).Should().Be(expected).AsJson();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData(false)]
			[InlineData(true)]
			public async Task WhenSubjectContainsAdditionalMembers_ShouldFail(bool ignoreAdditionalMembers)
			{
				string subject = """
				                 { "foo": 1, "bar" : "xyz" }
				                 """;
				string expected = """
				                  {
				                    "foo": 1
				                  }
				                  """;

				async Task Act()
					=> await That(subject).Should().Be(expected)
						.AsJson(o => o.IgnoringAdditionalMembers(ignoreAdditionalMembers));

				await That(Act).Should().Throw<XunitException>().OnlyIf(!ignoreAdditionalMembers)
					.WithMessage("""
					             Expected subject to
					             be JSON equivalent to {
					               "foo": 1
					             },
					             but it contained unexpected members:
					              - .bar
					             """);
			}
		}
	}
}
#endif
