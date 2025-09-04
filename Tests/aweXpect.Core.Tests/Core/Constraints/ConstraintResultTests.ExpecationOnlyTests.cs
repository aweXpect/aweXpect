using aweXpect.Core.Constraints;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Constraints;

public partial class ConstraintResultTests
{
	public sealed class ExpectationOnlyTests
	{
		[Fact]
		public async Task NormalCase_ShouldNotHaveNegatedGrammarsFlagAndSuccessOutcome()
		{
			ConstraintResult sut = new ConstraintResult.ExpectationOnly<int>(ExpectationGrammars.Plural);

			await That(sut.Grammars).HasFlag(ExpectationGrammars.Plural);
			await That(sut.Grammars).DoesNotHaveFlag(ExpectationGrammars.Negated);
			await That(sut.Outcome).IsEqualTo(Outcome.Success);
		}


		[Theory]
		[InlineData(Outcome.Success)]
		[InlineData(Outcome.Failure)]
		[InlineData(Outcome.Undecided)]
		public async Task SetOutcome_ShouldBeForwardedToInner(Outcome outcome)
		{
			MyExpectationOnlyConstraintResult<int> sut = new(
				ExpectationGrammars.None, null, "negated-foo");

			sut.SetOutcome(outcome);

			await That(sut.Outcome).IsEqualTo(outcome);
		}

		[Fact]
		public async Task TryGetValue_ShouldReturnFalse()
		{
			ConstraintResult sut = new ConstraintResult.ExpectationOnly<int>(ExpectationGrammars.None);

			bool result = sut.TryGetValue(out int value);

			await That(result).IsFalse();
			await That(value).IsEqualTo(0);
		}

		[Fact]
		public async Task WhenInverted_ShouldHaveNegatedGrammarsFlagAndSuccessOutcome()
		{
			ConstraintResult sut = new ConstraintResult.ExpectationOnly<int>(ExpectationGrammars.Nested);

			sut = sut.Invert();

			await That(sut.Grammars).HasFlag(ExpectationGrammars.Nested);
			await That(sut.Grammars).HasFlag(ExpectationGrammars.Negated);
			await That(sut.Outcome).IsEqualTo(Outcome.Success);
		}

		[Fact]
		public async Task WhenInverted_WithExpectationText_ShouldAppendNegatedExpectationText()
		{
			ConstraintResult sut = new ConstraintResult.ExpectationOnly<int>(
				ExpectationGrammars.None, "foo", "negated-foo").Invert();

			string expectationText = sut.GetExpectationText();
			string resultText = sut.GetResultText();

			await That(expectationText).IsEqualTo("negated-foo");
			await That(resultText).IsEmpty();
		}

		[Fact]
		public async Task WhenInverted_WithNullAsExpectationText_ShouldAppendNegatedExpectationText()
		{
			ConstraintResult sut = new ConstraintResult.ExpectationOnly<int>(
				ExpectationGrammars.None, "foo").Invert();

			string expectationText = sut.GetExpectationText();
			string resultText = sut.GetResultText();

			await That(expectationText).IsEmpty();
			await That(resultText).IsEmpty();
		}

		[Fact]
		public async Task WithExpectationText_ShouldAppendExpectationText()
		{
			ConstraintResult sut = new ConstraintResult.ExpectationOnly<int>(
				ExpectationGrammars.None, "foo", "negated-foo");

			string expectationText = sut.GetExpectationText();
			string resultText = sut.GetResultText();

			await That(expectationText).IsEqualTo("foo");
			await That(resultText).IsEmpty();
		}

		[Fact]
		public async Task WithNullAsExpectationText_ShouldKeepEmptyExpectationText()
		{
			ConstraintResult sut = new ConstraintResult.ExpectationOnly<int>(
				ExpectationGrammars.None, null, "negated-foo");

			string expectationText = sut.GetExpectationText();
			string resultText = sut.GetResultText();

			await That(expectationText).IsEmpty();
			await That(resultText).IsEmpty();
		}

		private class MyExpectationOnlyConstraintResult<T>(
			ExpectationGrammars grammars,
			string? expectation = null,
			string? negatedExpectation = null)
			: ConstraintResult.ExpectationOnly<T>(grammars, expectation, negatedExpectation)
		{
			public void SetOutcome(Outcome outcome) => Outcome = outcome;
		}
	}
}
