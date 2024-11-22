namespace Testably.Expectations.Tests.ThatTests.Booleans;

public sealed partial class BoolShould
{
	public sealed class ImplyTests
	{
		[Fact]
		public async Task WhenAntecedentDoesNotImplyConsequent_ShouldFail()
		{
			bool antecedent = true;
			bool consequent = false;

			async Task Act()
				=> await That(antecedent).Should().Imply(consequent)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected antecedent to
				              imply {Formatter.Format(consequent)}, because we want to test the failure,
				              but it did not
				              """);
		}

		[Theory]
		[InlineData(false, false)]
		[InlineData(false, true)]
		[InlineData(true, true)]
		public async Task WhenAntecedentImpliesConsequent_ShouldSucceed(bool antecedent,
			bool consequent)
		{
			async Task Act()
				=> await That(antecedent).Should().Imply(consequent);

			await That(Act).Should().NotThrow();
		}
	}
}
