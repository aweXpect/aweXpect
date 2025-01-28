namespace aweXpect.Internal.Tests.ThatTests.Collections;

public sealed partial class QuantifiableCollectionItems
{
	public sealed class AreEquivalentTests
	{
		[Fact]
		public async Task WhenCollectionContainsOtherValues_ShouldFail()
		{
			MyClass[] subject =
			[
				new()
				{
					Value = "Foo"
				},
				new()
				{
					Value = "Foo"
				},
				new()
				{
					Value = "Foo"
				},
				new()
				{
					Value = "Bar"
				}
			];

			MyClass expected = new()
			{
				Value = "Foo"
			};

			async Task Act()
				=> await That(subject).All().Are(item => item.IsEquivalentTo(expected));

			await That(Act).Throws<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items be equivalent to expected,
				             but only 3 of 4 were
				             """);
		}

		[Fact]
		public async Task WhenCollectionOnlyContainsEqualValues_ShouldSucceed()
		{
			MyClass[] subject =
			[
				new()
				{
					Value = "Foo"
				},
				new()
				{
					Value = "Foo"
				},
				new()
				{
					Value = "Foo"
				}
			];

			MyClass expected = new()
			{
				Value = "Foo"
			};

			async Task Act()
				=> await That(subject).All().Are(item => item.IsEquivalentTo(expected));

			await That(Act).DoesNotThrow();
		}
	}
}
