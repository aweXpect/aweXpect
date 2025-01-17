using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.Core;

public sealed class ConstraintResultTests
{
	[Theory]
	[AutoData]
	public async Task Failure_WithoutValue_ShouldStoreTexts(string expectationText,
		string resultText)
	{
		ConstraintResult.Failure subject = new(expectationText, resultText);

		await That(subject.ExpectationText).Is(expectationText);
		await That(subject.ResultText).Is(resultText);
	}

	[Theory]
	[AutoData]
	public async Task Failure_WithValue_ShouldStoreValueAndTexts(string expectationText,
		string resultText)
	{
		Dummy value = new() { Value = 1 };

		ConstraintResult.Failure<Dummy> subject = new(value, expectationText, resultText);

		await That(subject.Value).Is().EquivalentTo(value);
		await That(subject.ExpectationText).Is(expectationText);
		await That(subject.ResultText).Is(resultText);
	}

	[Theory]
	[AutoData]
	public async Task Success_WithValue_ShouldStoreValue(string expectationText)
	{
		Dummy value = new() { Value = 1 };

		ConstraintResult.Success<Dummy> subject = new(value, expectationText);

		await That(subject.Value).Is().EquivalentTo(value);
		await That(subject.ExpectationText).Is(expectationText);
	}

	[Theory]
	[AutoData]
	public async Task ToString_Failure_ShouldBeExpectationTextWithPrependedFailed(
		string expectationText)
	{
		ConstraintResult.Failure subject = new(expectationText, "result text");

		string result = subject.ToString();

		await That(result).Is($"FAILED {expectationText}");
	}

	[Theory]
	[AutoData]
	public async Task ToString_Success_ShouldBeExpectationTextWithPrependedSucceeded(
		string expectationText)
	{
		ConstraintResult.Success subject = new(expectationText);

		string result = subject.ToString();

		await That(result).Is($"SUCCEEDED {expectationText}");
	}

	private sealed class Dummy
	{
		public int Value { get; set; }
	}
}
