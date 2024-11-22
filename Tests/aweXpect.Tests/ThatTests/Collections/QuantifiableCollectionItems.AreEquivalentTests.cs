namespace Testably.Expectations.Tests.ThatTests.Collections;

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
				=> await That(subject).Should().All().BeEquivalentTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items equivalent to expected,
				             but not all of 4 items were equivalent
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
				=> await That(subject).Should().All().BeEquivalentTo(expected);

			await That(Act).Should().NotThrow();
		}
	}
}
