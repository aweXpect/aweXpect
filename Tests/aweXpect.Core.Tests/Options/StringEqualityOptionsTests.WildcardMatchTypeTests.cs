namespace aweXpect.Core.Tests.Options;

public sealed partial class StringEqualityOptionsTests
{
	public sealed class WildcardMatchTypeTests
	{
		[Fact]
		public async Task ShouldCompareCaseSensitive()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be("FOO").AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match "FOO",
				             but it did not match
				               ↓ (actual)
				               "foo"
				               "FOO"
				               ↑ (wildcard pattern)
				             """);
		}

		[Fact]
		public async Task ShouldDisplayActualAndPatternUnderneathEachOther()
		{
			string sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be("bar").AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match "bar",
				             but it did not match
				               ↓ (actual)
				               "foo"
				               "bar"
				               ↑ (wildcard pattern)
				             """);
		}

		[Fact]
		public async Task ShouldReplaceNewlines()
		{
			string sut = "foo\nbar";

			async Task Act()
				=> await That(sut).Should().Be("\tsomething\r\nelse").AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match "\tsomething\r\nelse",
				             but it did not match
				               ↓ (actual)
				               "foo\nbar"
				               "\tsomething\r\nelse"
				               ↑ (wildcard pattern)
				             """);
		}

		[Fact]
		public async Task WhenIgnoringCase_ShouldCompareCaseInsensitive()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be("FOO").AsWildcard().IgnoringCase();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenPatternIsNull_ShouldFail()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be(null).AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match <null>,
				             but it did not match
				               ↓ (actual)
				               "foo"
				               <null>
				               ↑ (wildcard pattern)
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			string? sut = null;

			async Task Act()
				=> await That(sut).Should().Be("*").AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match "*",
				             but it did not match
				               ↓ (actual)
				               <null>
				               "*"
				               ↑ (wildcard pattern)
				             """);
		}
	}
}
