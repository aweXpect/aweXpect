namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	public sealed class CompliesWith
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(1, true)]
			[InlineData(2, false)]
			public async Task WhenValueIsDifferent_ShouldFail(int expectedValue, bool expectSuccess)
			{
				Other subject = new()
				{
					Value = 1,
				};

				async Task Act()
					=> await That(subject).CompliesWith(x => x.IsEquivalentTo(new
					{
						Value = expectedValue,
					}));

				await That(Act).Throws<XunitException>()
					.OnlyIf(!expectSuccess)
					.WithMessage("""
					             Expected that subject
					             is equivalent to new
					             					{
					             						Value = expectedValue,
					             					},
					             but it was Other {
					               Value = 1
					             }
					             """);
			}
		}
	}
}
