using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class OrNodeTests
{
	[Fact]
	public async Task GetContexts_ShouldIncludeContextsFromLeftAndRight()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success("").WithContext("t1", "c1"));
		DummyNode node2 = new("", () => new ConstraintResult.Success("").WithContext("t2", "c2"));
		OrNode orNode = new(node1);
		orNode.AddNode(node2);
		ConstraintResult constraintResult = await orNode.IsMetBy(0, null!, CancellationToken.None);

		List<ConstraintResult.Context> contexts = constraintResult.GetContexts().ToList();

		await That(contexts)
			.IsEqualTo([
				new ConstraintResult.Context("t1", "c1"),
				new ConstraintResult.Context("t2", "c2"),
			])
			.InAnyOrder();
	}

	[Fact]
	public async Task ToString_ShouldCombineAllNodes()
	{
#pragma warning disable CS4014
#pragma warning disable aweXpect0001
		IThat<bool> that = That(true);
		that.IsTrue().Or.IsFalse().Or.Implies(false);
#pragma warning restore aweXpect0001
#pragma warning restore CS4014

		string expectedResult = "is True or is False or implies False";

		string? result = ((IThatVerb<bool>)that).ExpectationBuilder.ToString();

		await That(result).IsEqualTo(expectedResult);
	}

	[Fact]
	public async Task ToString_WithoutAdditionalNodes_ShouldUseFirstNode()
	{
		OrNode node = new(new DummyNode("foo"));

		string? result = node.ToString();

		await That(result).IsEqualTo("foo");
	}

	[Fact]
	public async Task TryGetValue_WhenLeftHasValue_ShouldReturnLeftValue()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<int>(1, ""));
		DummyNode node2 = new("", () => new ConstraintResult.Success<int>(2, ""));
		OrNode orNode = new(node1);
		orNode.AddNode(node2);
		ConstraintResult constraintResult = await orNode.IsMetBy(0, null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out int value);

		await That(result).IsTrue();
		await That(value).IsEqualTo(1);
	}

	[Fact]
	public async Task TryGetValue_WhenNoneHasValue_ShouldReturnFalse()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success(""));
		DummyNode node2 = new("", () => new ConstraintResult.Success(""));
		OrNode orNode = new(node1);
		orNode.AddNode(node2);
		ConstraintResult constraintResult = await orNode.IsMetBy(0, null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out int? value);

		await That(result).IsFalse();
		await That(value).IsNull();
	}

	[Fact]
	public async Task TryGetValue_WhenOnlyRightHasValue_ShouldReturnRightValue()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success(""));
		DummyNode node2 = new("", () => new ConstraintResult.Success<int>(2, ""));
		OrNode orNode = new(node1);
		orNode.AddNode(node2);
		ConstraintResult constraintResult = await orNode.IsMetBy(0, null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out int value);

		await That(result).IsTrue();
		await That(value).IsEqualTo(2);
	}

	[Fact]
	public async Task WithCustomSeparator_ShouldUseItInsteadOfOr()
	{
		OrNode node = new(new DummyNode("", () => new ConstraintResult.Failure("foo", "-")));
		node.AddNode(new DummyNode("", () => new ConstraintResult.Failure("bar", "-")), " my ");
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("foo my bar");
	}

	[Fact]
	public async Task WithFirstFailedTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsFalse().Or.IsTrue();

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task WithMultipleFailedTests_ShouldIncludeAllFailuresInMessage()
	{
		async Task Act()
			=> await That(true).IsFalse().Or.IsFalse().Or.Implies(false);

		await That(Act).ThrowsException()
			.WithMessage("""
			             Expected that true
			             is False or is False or implies False,
			             but it was True and it did not
			             """);
	}

	[Fact]
	public async Task WithProcessingStrategy_ShouldConsiderIgnoreCompletely()
	{
		OrNode node = new(new DummyNode("",
			() => new ConstraintResult.Failure("foo", "r1")));
		node.AddNode(new DummyNode("",
			() => new ConstraintResult.Failure("bar", "r2", FurtherProcessingStrategy.IgnoreCompletely)));
		node.AddNode(new DummyNode("",
			() => new ConstraintResult.Success("baz")));
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("foo or bar");
		await That(result.GetResultText()).IsEqualTo("r1 and r2");
		await That(result.Outcome).IsEqualTo(Outcome.Failure);
	}

	[Fact]
	public async Task WithSecondFailedTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsTrue().Or.IsFalse();

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task WithTwoSuccessfulTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsTrue().Or.IsNotEqualTo(false);

		await That(Act).DoesNotThrow();
	}
}
