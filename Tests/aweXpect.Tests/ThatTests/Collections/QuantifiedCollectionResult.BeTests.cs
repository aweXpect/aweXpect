namespace Testably.Expectations.Tests.ThatTests.Collections;

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
				=> await That(subject).Should().All().Be<MyClass>();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items of type MyClass,
				             but not all of 3 items were
				             """);
		}

		[Fact]
		public async Task WhenCollectionOnlyContainsEqualValues_ShouldSucceed()
		{
			object[] subject =
			[
				new MyClass(1),
				new SubClass(1),
			];

			async Task Act()
				=> await That(subject).Should().All().Be<MyClass>();

			await That(Act).Should().NotThrow();
		}
	}
}
