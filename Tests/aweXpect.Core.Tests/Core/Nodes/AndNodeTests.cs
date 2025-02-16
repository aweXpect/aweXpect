using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class AndNodeTests
{
	[Fact]
	public async Task GetContexts_ShouldIncludeContextsFromLeftAndRight()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success("").WithContext("t1", "c1"));
		DummyNode node2 = new("", () => new ConstraintResult.Success("").WithContext("t2", "c2"));
		AndNode andNode = new(node1);
		andNode.AddNode(node2);
		ConstraintResult constraintResult = await andNode.IsMetBy(0, null!, CancellationToken.None);

		List<ConstraintResult.Context> contexts = constraintResult.GetContexts().ToList();

		await That(contexts)
			.IsEqualTo([
				new ConstraintResult.Context("t1", "c1"),
				new ConstraintResult.Context("t2", "c2"),
			])
			.InAnyOrder();
	}

	[Theory]
	[InlineData(Outcome.Success, Outcome.Success, Outcome.Success)]
	[InlineData(Outcome.Failure, Outcome.Success, Outcome.Failure)]
	[InlineData(Outcome.Success, Outcome.Failure, Outcome.Failure)]
	[InlineData(Outcome.Failure, Outcome.Failure, Outcome.Failure)]
	[InlineData(Outcome.Failure, Outcome.Undecided, Outcome.Failure)]
	[InlineData(Outcome.Undecided, Outcome.Failure, Outcome.Failure)]
	[InlineData(Outcome.Undecided, Outcome.Undecided, Outcome.Undecided)]
	public async Task Outcome_ShouldBeExpected(Outcome node1, Outcome node2, Outcome expectedOutcome)
	{
		AndNode node = new(new DummyNode("", () => new DummyConstraintResult(node1)));
		node.AddNode(new DummyNode("", () => new DummyConstraintResult(node2)));

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		await That(result.Outcome).IsEqualTo(expectedOutcome);
	}

	[Fact]
	public async Task ShouldConsiderFurtherProcessingStrategy()
	{
		AndNode node = new(new DummyNode("",
			() => new ConstraintResult.Failure("foo", "-")));
		node.AddNode(new DummyNode("",
			() => new ConstraintResult.Failure("bar", "-", FurtherProcessingStrategy.IgnoreCompletely)), " my ");
		node.AddNode(new DummyNode("",
			() => new ConstraintResult.Failure("baz", "-")), " is ");
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("foo my bar");
	}

	[Fact]
	public async Task ToString_ShouldCombineAllNodes()
	{
#pragma warning disable CS4014
#pragma warning disable aweXpect0001
		IThat<bool> that = That(true);
		that.IsTrue().And.IsFalse().And.Implies(false);
#pragma warning restore aweXpect0001
#pragma warning restore CS4014

		string expectedResult = "is True and is False and implies False";

		string? result = ((IThatVerb<bool>)that).ExpectationBuilder.ToString();

		await That(result).IsEqualTo(expectedResult);
	}

	[Fact]
	public async Task ToString_WithoutAdditionalNodes_ShouldUseFirstNode()
	{
		AndNode node = new(new DummyNode("foo"));

		string? result = node.ToString();

		await That(result).IsEqualTo("foo");
	}

	[Fact]
	public async Task TryGetValue_WhenLeftHasValue_ShouldReturnLeftValue()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<int>(1, ""));
		DummyNode node2 = new("", () => new ConstraintResult.Success<int>(2, ""));
		AndNode andNode = new(node1);
		andNode.AddNode(node2);
		ConstraintResult constraintResult = await andNode.IsMetBy(0, null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out int value);

		await That(result).IsTrue();
		await That(value).IsEqualTo(1);
	}

	[Fact]
	public async Task TryGetValue_WhenNoneHasValue_ShouldReturnFalse()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success(""));
		DummyNode node2 = new("", () => new ConstraintResult.Success(""));
		AndNode andNode = new(node1);
		andNode.AddNode(node2);
		ConstraintResult constraintResult = await andNode.IsMetBy(0, null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out int? value);

		await That(result).IsFalse();
		await That(value).IsNull();
	}

	[Fact]
	public async Task TryGetValue_WhenOnlyRightHasValue_ShouldReturnRightValue()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success(""));
		DummyNode node2 = new("", () => new ConstraintResult.Success<int>(2, ""));
		AndNode andNode = new(node1);
		andNode.AddNode(node2);
		ConstraintResult constraintResult = await andNode.IsMetBy(0, null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out int value);

		await That(result).IsTrue();
		await That(value).IsEqualTo(2);
	}

	[Fact]
	public async Task WithCustomSeparator_ShouldUseItInsteadOfOr()
	{
		AndNode node = new(new DummyNode("", () => new ConstraintResult.Failure("foo", "-")));
		node.AddNode(new DummyNode("", () => new ConstraintResult.Failure("bar", "-")), " my ");
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("foo my bar");
	}

	[Fact]
	public async Task WithCustomSeparators_ShouldUseItInsteadOfOr()
	{
		AndNode node = new(new DummyNode("", () => new ConstraintResult.Failure("foo", "-")));
		node.AddNode(new DummyNode("", () => new ConstraintResult.Failure("bar", "-")), " my ");
		node.AddNode(new DummyNode("", () => new ConstraintResult.Failure("baz", "-")), " is ");
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("foo my bar is baz");
	}

	[Fact]
	public async Task WithFirstFailedTests_ShouldIncludeSingleFailureInMessage()
	{
		async Task Act()
			=> await That(true).IsFalse().And.IsTrue();

		await That(Act).ThrowsException()
			.WithMessage("""
			             Expected that true
			             is False and is True,
			             but it was True
			             """);
	}

	[Fact]
	public async Task WithMultipleFailedTests_ShouldIncludeAllFailuresInMessage()
	{
		async Task Act()
			=> await That(true).IsFalse().And.IsFalse().And.Implies(false);

		await That(Act).ThrowsException()
			.WithMessage("""
			             Expected that true
			             is False and is False and implies False,
			             but it was True and it did not
			             """);
	}

	[Fact]
	public async Task WithProcessingStrategy_ShouldConsiderIgnoreCompletely()
	{
		AndNode node = new(new DummyNode("",
			() => new ConstraintResult.Success("foo")));
		node.AddNode(new DummyNode("",
			() => new ConstraintResult.Success("bar", FurtherProcessingStrategy.IgnoreCompletely)));
		node.AddNode(new DummyNode("",
			() => new ConstraintResult.Failure("baz", "-")));
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("foo and bar");
		await That(result.Outcome).IsEqualTo(Outcome.Success);
	}

	[Fact]
	public async Task WithSecondFailedTests_ShouldIncludeSingleFailureInMessage()
	{
		async Task Act()
			=> await That(true).IsTrue().And.IsFalse();

		await That(Act).ThrowsException()
			.WithMessage("""
			             Expected that true
			             is True and is False,
			             but it was True
			             """);
	}

	[Fact]
	public async Task WithTwoSuccessfulTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsTrue().And.IsNotEqualTo(false);

		await That(Act).DoesNotThrow();
	}
}
