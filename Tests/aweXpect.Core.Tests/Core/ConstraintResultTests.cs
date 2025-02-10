using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.Core;

public sealed class ConstraintResultTests
{
	[Theory]
	[AutoData]
	public async Task Failure_WithoutValue_ShouldStoreTexts(string expectationText,
		string resultText)
	{
		StringBuilder sb = new();
		ConstraintResult.Failure subject = new(expectationText, resultText);

		subject.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo(expectationText);
		await That(subject.GetResultText()).IsEqualTo(resultText);
	}

	[Theory]
	[AutoData]
	public async Task Failure_WithValue_ShouldStoreValueAndTexts(string expectationText,
		string resultText)
	{
		Dummy value = new()
		{
			Value = 1,
		};
		StringBuilder sb = new();

		ConstraintResult.Failure<Dummy> subject = new(value, expectationText, resultText);

		subject.AppendExpectation(sb);
		await That(subject.Value).IsEquivalentTo(value);
		await That(sb.ToString()).IsEqualTo(expectationText);
		await That(subject.GetResultText()).IsEqualTo(resultText);
	}

	[Theory]
	[AutoData]
	public async Task Success_WithValue_ShouldStoreValue(string expectationText)
	{
		Dummy value = new()
		{
			Value = 1,
		};
		StringBuilder sb = new();

		ConstraintResult.Success<Dummy> subject = new(value, expectationText);

		subject.AppendExpectation(sb);
		await That(subject.Value).IsEquivalentTo(value);
		await That(sb.ToString()).IsEqualTo(expectationText);
	}

	private sealed class Dummy
	{
		public int Value { get; set; }
	}
}
