#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatSpan
{
	public sealed class Tests
	{
		[Fact]
		public async Task ShouldSupportIsEmpty()
		{
			var subject = new[]
			{
				1, 2, 3,
			};
			async Task Act()
				=> await That(subject.AsSpan()).IsEmpty();

			await That(Act).Throws<XunitException>()
				.WithMessage("""
				             Expected that subject.AsSpan()
				             is empty,
				             but it was [
				               1,
				               2,
				               3
				             ]
				             """);
		}

		[Fact]
		public async Task ShouldSupportIsInAscendingOrder()
		{
			async Task Act()
				=> await That(new[]
				{
					1, 2, 3,
				}.AsSpan()).IsInAscendingOrder();

			await That(Act).DoesNotThrow();
		}
	}
}
#endif
