namespace aweXpect.Internal.Tests.ThatTests.Collections;

public sealed partial class QuantifiedCollectionResult
{
	public sealed class BeTests
	{
		[Fact]
		public async Task WhenCollectionContainsOtherValues_ShouldFail()
		{
			object[] subject =
			[
				new MyClass(1),
				new SubClass(1),
				new OtherClass(1)
			];

			async Task Act()
				=> await That(subject).All().Are<MyClass>();

			await That(Act).Throws<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items be of type MyClass,
				             but only 2 of 3 were
				             """);
		}

		[Fact]
		public async Task WhenCollectionOnlyContainsEqualValues_ShouldSucceed()
		{
			object[] subject =
			[
				new MyClass(1),
				new SubClass(1)
			];

			async Task Act()
				=> await That(subject).All().Are<MyClass>();

			await That(Act).DoesNotThrow();
		}
	}
}
