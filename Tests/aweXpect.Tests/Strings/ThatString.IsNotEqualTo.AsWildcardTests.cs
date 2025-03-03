namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public sealed partial class IsNotEqualTo
	{
		public sealed class AsWildcardTests
		{
			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task WhenIgnoringCase_ShouldIgnoreCase(
				bool ignoreCase)
			{
				string subject = "some message";
				string pattern = "*ME ME*";

				async Task Act()
					=> await That(subject).IsNotEqualTo(pattern)
						.AsWildcard().IgnoringCase(ignoreCase);

				await That(Act).ThrowsException().OnlyIf(ignoreCase)
					.WithMessage("""
					             Expected that subject
					             does not match "*ME ME*",
					             but it did match
					             
					             Actual:
					             some message
					             """);
			}
		}
	}
}
