using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Internal.Tests.Collections;

public sealed class EnumerableQuantifierTests
{
	public sealed class AllTests
	{
		[Theory]
		[InlineData(1)]
		[InlineData(-1)]
		public async Task WhenMatchingDoesNotEqualTotalItems_ShouldReturnFailure(int difference)
		{
			int matchingItems = 4;
			int totalItems = matchingItems + difference;
			EnumerableQuantifier sut = EnumerableQuantifier.All();
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", matchingItems, 2, totalItems, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(sb.ToString()).IsEqualTo($"only {matchingItems} of {totalItems} were");
		}

		[Fact]
		public async Task WhenMatchingEqualsTotalItems_ShouldReturnSuccess()
		{
			int matchingItems = 4;
			EnumerableQuantifier sut = EnumerableQuantifier.All();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", matchingItems, 0, matchingItems, null);

			await That(result.Outcome).IsEqualTo(Outcome.Success);
		}

		[Fact]
		public async Task WhenNotEnumeratedCompletely_ShouldHaveUndecidedOutcome()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.All();
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 2, 0, null, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Undecided);
			await That(sb.ToString()).IsEqualTo("could not verify, because it was not enumerated completely");
		}
	}

	public sealed class AtLeastTests
	{
		[Theory]
		[InlineData(3)]
		[InlineData(4)]
		public async Task WhenHavingSufficientItems_ShouldReturnSuccess(int foundItems)
		{
			EnumerableQuantifier sut = EnumerableQuantifier.AtLeast(3);

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", foundItems, 2, 7, null);

			await That(result.Outcome).IsEqualTo(Outcome.Success);
		}

		[Fact]
		public async Task WhenHavingTooFewItems_ShouldReturnFailure()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.AtLeast(3);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 2, 6, 8, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(sb.ToString()).IsEqualTo("only 2 of 8 were");
		}

		[Fact]
		public async Task WhenNotEnumeratedCompletely_ShouldHaveUndecidedOutcome()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.AtLeast(3);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 2, 0, null, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Undecided);
			await That(sb.ToString()).IsEqualTo("could not verify, because it was not enumerated completely");
		}
	}

	public sealed class AtMostTests
	{
		[Theory]
		[InlineData(3)]
		[InlineData(4)]
		public async Task WhenHavingSufficientItems_ShouldReturnSuccess(int foundItems)
		{
			EnumerableQuantifier sut = EnumerableQuantifier.AtMost(4);

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", foundItems, 2, 7, null);

			await That(result.Outcome).IsEqualTo(Outcome.Success);
		}

		[Fact]
		public async Task WhenHavingTooManyItems_ShouldReturnFailure()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.AtMost(4);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 5, 2, 8, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(sb.ToString()).IsEqualTo("5 of 8 were");
		}

		[Fact]
		public async Task WhenNotEnumeratedCompletely_ShouldHaveUndecidedOutcome()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.AtMost(4);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 2, 0, null, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Undecided);
			await That(sb.ToString()).IsEqualTo("could not verify, because it was not enumerated completely");
		}
	}

	public sealed class BetweenTests
	{
		[Theory]
		[InlineData(3)]
		[InlineData(4)]
		[InlineData(5)]
		public async Task WhenHavingSufficientItems_ShouldReturnSuccess(int foundItems)
		{
			EnumerableQuantifier sut = EnumerableQuantifier.Between(3, 5);

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", foundItems, 2, 7, null);

			await That(result.Outcome).IsEqualTo(Outcome.Success);
		}

		[Fact]
		public async Task WhenHavingTooFewItems_ShouldReturnFailure()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.Between(3, 5);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 2, 5, 7, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(sb.ToString()).IsEqualTo("only 2 of 7 were");
		}

		[Fact]
		public async Task WhenHavingTooManyItems_ShouldReturnFailure()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.Between(3, 5);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 6, 2, 8, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(sb.ToString()).IsEqualTo("6 of 8 were");
		}

		[Fact]
		public async Task WhenNotEnumeratedCompletely_ShouldHaveUndecidedOutcome()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.Between(3, 5);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 2, 0, null, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Undecided);
			await That(sb.ToString()).IsEqualTo("could not verify, because it was not enumerated completely");
		}
	}

	public sealed class ExactlyTests
	{
		[Fact]
		public async Task WhenHavingSufficientItems_ShouldReturnSuccess()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.Exactly(3);

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 3, 2, 7, null);

			await That(result.Outcome).IsEqualTo(Outcome.Success);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task WhenHavingTooFewItems_ShouldReturnFailure(int foundItems)
		{
			EnumerableQuantifier sut = EnumerableQuantifier.Exactly(3);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", foundItems, 6, 8, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(sb.ToString()).IsEqualTo($"only {foundItems} of 8 were");
		}

		[Theory]
		[InlineData(4)]
		[InlineData(5)]
		public async Task WhenHavingTooManyItems_ShouldReturnFailure(int foundItems)
		{
			EnumerableQuantifier sut = EnumerableQuantifier.Exactly(3);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", foundItems, 6, 8, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(sb.ToString()).IsEqualTo($"{foundItems} of 8 were");
		}

		[Fact]
		public async Task WhenNotEnumeratedCompletely_ShouldHaveUndecidedOutcome()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.Exactly(4);
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 2, 0, null, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Undecided);
			await That(sb.ToString()).IsEqualTo("could not verify, because it was not enumerated completely");
		}
	}

	public sealed class NoneTests
	{
		[Fact]
		public async Task WhenMatchingCountIsGreaterThanZero_ShouldReturnFailure()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.None();
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 1, 3, 4, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(sb.ToString()).IsEqualTo("1 of 4 were");
		}

		[Fact]
		public async Task WhenNotEnumeratedCompletely_ShouldHaveUndecidedOutcome()
		{
			EnumerableQuantifier sut = EnumerableQuantifier.None();
			StringBuilder sb = new();

			ConstraintResult result = sut.GetResult<int[]>([], "it", "_should_", 0, 2, null, null);

			result.AppendResult(sb);
			await That(result.Outcome).IsEqualTo(Outcome.Undecided);
			await That(sb.ToString()).IsEqualTo("could not verify, because it was not enumerated completely");
		}

		[Fact]
		public async Task WhenNotMatchingEqualsTotalItems_ShouldReturnSuccess()
		{
			int notMatchingItems = 4;
			EnumerableQuantifier sut = EnumerableQuantifier.None();

			ConstraintResult result =
				sut.GetResult<int[]>([], "it", "_should_", 0, notMatchingItems, notMatchingItems, null);

			await That(result.Outcome).IsEqualTo(Outcome.Success);
		}
	}
}
