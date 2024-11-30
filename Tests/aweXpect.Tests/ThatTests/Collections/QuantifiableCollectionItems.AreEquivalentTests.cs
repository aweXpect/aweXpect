namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class QuantifiableCollectionItems
{
	public sealed class AreEquivalentTests
	{
		[Fact]
		public async Task WhenCollectionContainsOtherValues_ShouldFail()
		{
			MyClass[] subject =
			[
				new() { Value = "Foo" },
				new() { Value = "Foo" },
				new() { Value = "Foo" },
				new() { Value = "Bar" }
			];

			MyClass expected = new() { Value = "Foo" };

			async Task Act()
				=> await That(subject).Should().All(x => x.Be(expected).Equivalent());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items be equivalent to expected,
				             but not all were
				             """);
		}

		[Fact]
		public async Task WhenCollectionOnlyContainsEqualValues_ShouldSucceed()
		{
			MyClass[] subject =
			[
				new() { Value = "Foo" },
				new() { Value = "Foo" },
				new() { Value = "Foo" }
			];

			MyClass expected = new() { Value = "Foo" };

			async Task Act()
				=> await That(subject).Should().All(x => x.Be(expected).Equivalent());

			await That(Act).Should().NotThrow();
		}
	}
}
