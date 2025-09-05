﻿using System.Text;
using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class AndNodeTests
{
	[Fact]
	public async Task AddAsyncMapping_ShouldUseCurrentNode()
	{
		MemberAccessor<string, Task<int>> memberAccessor =
			MemberAccessor<string, Task<int>>.FromExpression(x => Task.FromResult(x.Length));
		DummyNode first = new("foo");
		DummyNode second = new("bar");
		AndNode node = new(first);

		node.AddAsyncMapping(memberAccessor);
		node.AddNode(second);

		await That(first.MappingMemberAccessor).IsSameAs(memberAccessor);
		await That(second.MappingMemberAccessor).IsNull();
	}

	[Fact]
	public async Task AddAsyncMapping_ShouldUseSecondNode()
	{
		MemberAccessor<string, Task<int>> memberAccessor =
			MemberAccessor<string, Task<int>>.FromExpression(x => Task.FromResult(x.Length));
		DummyNode first = new("foo");
		DummyNode second = new("bar");
		AndNode node = new(first);
		node.AddNode(second);

		node.AddAsyncMapping(memberAccessor);

		await That(first.MappingMemberAccessor).IsNull();
		await That(second.MappingMemberAccessor).IsSameAs(memberAccessor);
	}

	[Fact]
	public async Task AddMapping_ShouldUseCurrentNode()
	{
		MemberAccessor<string, int> memberAccessor = MemberAccessor<string, int>.FromExpression(x => x.Length);
		DummyNode first = new("foo");
		DummyNode second = new("bar");
		AndNode node = new(first);

		node.AddMapping(memberAccessor);
		node.AddNode(second);

		await That(first.MappingMemberAccessor).IsSameAs(memberAccessor);
		await That(second.MappingMemberAccessor).IsNull();
	}

	[Fact]
	public async Task AddMapping_ShouldUseSecondNode()
	{
		MemberAccessor<string, int> memberAccessor = MemberAccessor<string, int>.FromExpression(x => x.Length);
		DummyNode first = new("foo");
		DummyNode second = new("bar");
		AndNode node = new(first);
		node.AddNode(second);

		node.AddMapping(memberAccessor);

		await That(first.MappingMemberAccessor).IsNull();
		await That(second.MappingMemberAccessor).IsSameAs(memberAccessor);
	}

	[Fact]
	public async Task AppendExpectation_WithAdditionalNodes_ShouldUseAllNodes()
	{
		AndNode node = new(new DummyNode("foo"));
		node.AddNode(new DummyNode("bar"));
		node.AddNode(new DummyNode("baz"));
		StringBuilder sb = new();

		node.AppendExpectation(sb);

		await That(sb.ToString()).IsEqualTo("foo and bar and baz");
	}

	[Fact]
	public async Task AppendExpectation_WithoutAdditionalNodes_ShouldUseFirstNode()
	{
		AndNode node = new(new DummyNode("foo"));
		StringBuilder sb = new();

		node.AppendExpectation(sb);

		await That(sb.ToString()).IsEqualTo("foo");
	}

	[Fact]
	public async Task Equals_IfCurrentNodeIsDifferent_ShouldBeFalse()
	{
		DummyNode innerNode1 = new("1", () => new DummyConstraintResult<string?>(Outcome.Success, "1", ""));
		DummyNode innerNode2 = new("2", () => new DummyConstraintResult<string?>(Outcome.Success, "2", ""));
		AndNode node1 = new(innerNode1);
		AndNode node2 = new(innerNode2);

		bool result = node1.Equals(node2);

		await That(result).IsFalse();
		await That(node1.GetHashCode()).IsNotEqualTo(node2.GetHashCode());
	}

	[Fact]
	public async Task Equals_IfCurrentNodeIsTheSame_ShouldBeTrue()
	{
		DummyNode innerNode1 = new("1", () => new DummyConstraintResult<string?>(Outcome.Success, "1", ""));
		DummyNode innerNode2 = new("1", () => new DummyConstraintResult<string?>(Outcome.Success, "1", ""));
		AndNode node1 = new(innerNode1);
		AndNode node2 = new(innerNode2);

		bool result = node1.Equals(node2);

		await That(result).IsTrue();
		await That(node1.GetHashCode()).IsEqualTo(node2.GetHashCode());
	}

	[Fact]
	public async Task Equals_IfInnerNodesAreDifferent_ShouldBeFalse()
	{
		DummyNode innerNode0 = new("0", () => new DummyConstraintResult<string?>(Outcome.Success, "0", ""));
		DummyNode innerNode1 = new("1", () => new DummyConstraintResult<string?>(Outcome.Success, "1", ""));
		DummyNode innerNode2 = new("2", () => new DummyConstraintResult<string?>(Outcome.Success, "2", ""));
		DummyNode currentNode = new("3", () => new DummyConstraintResult<string?>(Outcome.Success, "3", ""));
		AndNode node1 = new(innerNode0);
		node1.AddNode(innerNode1);
		node1.AddNode(currentNode);
		AndNode node2 = new(innerNode0);
		node2.AddNode(innerNode2);
		node2.AddNode(currentNode);

		bool result = node1.Equals(node2);

		await That(result).IsFalse();
		await That(node1.GetHashCode()).IsNotEqualTo(node2.GetHashCode());
	}

	[Fact]
	public async Task Equals_IfInnerNodesAreSame_ShouldBeTrue()
	{
		DummyNode innerNode1 = new("1", () => new DummyConstraintResult<string?>(Outcome.Success, "1", ""));
		DummyNode innerNode2 = new("1", () => new DummyConstraintResult<string?>(Outcome.Success, "1", ""));
		DummyNode innerNode3 = new("2", () => new DummyConstraintResult<string?>(Outcome.Success, "2", ""));
		DummyNode innerNode4 = new("2", () => new DummyConstraintResult<string?>(Outcome.Success, "2", ""));
		DummyNode currentNode = new("3", () => new DummyConstraintResult<string?>(Outcome.Success, "3", ""));
		AndNode node1 = new(innerNode1);
		node1.AddNode(innerNode3);
		node1.AddNode(currentNode);
		AndNode node2 = new(innerNode2);
		node2.AddNode(innerNode4);
		node2.AddNode(currentNode);

		bool result = node1.Equals(node2);

		await That(result).IsTrue();
		await That(node1.GetHashCode()).IsEqualTo(node2.GetHashCode());
	}

	[Fact]
	public async Task Equals_WhenOtherIsDifferentNode_ShouldBeFalse()
	{
		DummyNode inner = new("foo");
		AndNode node = new(inner);
		object other = new OrNode(inner);

		bool result = node.Equals(other);

		await That(result).IsFalse();
	}

	[Fact]
	public async Task Equals_WhenOtherIsNull_ShouldBeFalse()
	{
		AndNode node = new(new DummyNode("foo"));

		bool result = node.Equals(null);

		await That(result).IsFalse();
	}

	[Theory]
	[InlineData(Outcome.Success, Outcome.Success, Outcome.Failure)]
	[InlineData(Outcome.Failure, Outcome.Success, Outcome.Success)]
	[InlineData(Outcome.Success, Outcome.Failure, Outcome.Success)]
	[InlineData(Outcome.Failure, Outcome.Failure, Outcome.Success)]
	[InlineData(Outcome.Failure, Outcome.Undecided, Outcome.Success)]
	[InlineData(Outcome.Undecided, Outcome.Failure, Outcome.Success)]
	[InlineData(Outcome.Undecided, Outcome.Undecided, Outcome.Undecided)]
	public async Task NegatedOutcome_ShouldBeExpected(Outcome node1, Outcome node2, Outcome expectedOutcome)
	{
		AndNode node = new(new DummyNode("", () => new DummyConstraintResult(node1)));
		node.AddNode(new DummyNode("", () => new DummyConstraintResult(node2)));

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);
		result.Negate();

		await That(result.Outcome).IsEqualTo(expectedOutcome);
	}

	[Fact]
	public async Task NegatedResult_ShouldUseOrAsSeparator()
	{
		AndNode node = new(new DummyNode("", () => new DummyConstraintResult(Outcome.Success, "foo")));
		node.AddNode(new DummyNode("", () => new DummyConstraintResult(Outcome.Success, "bar")));
		StringBuilder sb1 = new();
		StringBuilder sb2 = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);
		result.AppendExpectation(sb1);

		result.Negate();

		result.AppendExpectation(sb2);
		await That(sb1.ToString()).IsEqualTo("foo and bar");
		await That(sb2.ToString()).IsEqualTo("foo or bar");
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
	public async Task SetReason_WithAdditionalNodes_ShouldUseCurrentNode()
	{
		DummyNode node1 = new("node1");
		DummyNode node2 = new("node2");
		DummyNode current = new("current");
		AndNode node = new(node1);
		node.AddNode(node2);
		node.AddNode(current);

		node.SetReason(new BecauseReason("bar"));

		await That(current.ReceivedReason).IsEqualTo(", because bar");
		await That(node1.ReceivedReason).IsNull();
		await That(node2.ReceivedReason).IsNull();
	}

	[Fact]
	public async Task SetReason_WithAdditionalNodes_WhenCurrentNodeIsEmptyExpectationNode_ShouldUseLastNode()
	{
		DummyNode node1 = new("node1");
		DummyNode node2 = new("node2");
		ExpectationNode current = new();
		AndNode node = new(node1);
		node.AddNode(node2);
		node.AddNode(current);

		node.SetReason(new BecauseReason("bar"));

		await That(node1.ReceivedReason).IsNull();
		await That(node2.ReceivedReason).IsEqualTo(", because bar");
	}

	[Fact]
	public async Task SetReason_WithoutAdditionalNodes_ShouldSetReasonForCurrentNode()
	{
		DummyNode current = new("current");
		AndNode node = new(current);

		node.SetReason(new BecauseReason("bar"));

		await That(current.ReceivedReason).IsEqualTo(", because bar");
	}

	[Fact]
	public async Task ShouldConsiderFurtherProcessingStrategy()
	{
		AndNode node = new(new DummyNode("",
			() => new DummyConstraintResult(Outcome.Failure, "foo", "-")));
		node.AddNode(new DummyNode("",
				() => new DummyConstraintResult(Outcome.Failure, "bar", "-",
					FurtherProcessingStrategy.IgnoreCompletely)),
			" my ");
		node.AddNode(new DummyNode("",
			() => new DummyConstraintResult(Outcome.Failure, "baz", "-")), " is ");
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("foo my bar");
	}

	[Fact]
	public async Task TryGetValue_WhenLeftHasValue_ShouldReturnLeftValue()
	{
		DummyNode node1 = new("", () => new DummyConstraintResult<int>(Outcome.Success, 1, ""));
		DummyNode node2 = new("", () => new DummyConstraintResult<int>(Outcome.Success, 2, ""));
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
		DummyNode node1 = new("", () => new DummyConstraintResult(Outcome.Success, ""));
		DummyNode node2 = new("", () => new DummyConstraintResult(Outcome.Success, ""));
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
		DummyNode node1 = new("", () => new DummyConstraintResult(Outcome.Success, ""));
		DummyNode node2 = new("", () => new DummyConstraintResult<int>(Outcome.Success, 2, ""));
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
		AndNode node = new(new DummyNode("", () => new DummyConstraintResult(Outcome.Failure, "foo", "-")));
		node.AddNode(new DummyNode("", () => new DummyConstraintResult(Outcome.Failure, "bar", "-")), " my ");
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(0, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("foo my bar");
	}

	[Fact]
	public async Task WithCustomSeparators_ShouldUseItInsteadOfOr()
	{
		AndNode node = new(new DummyNode("", () => new DummyConstraintResult(Outcome.Failure, "foo", "-")));
		node.AddNode(new DummyNode("", () => new DummyConstraintResult(Outcome.Failure, "bar", "-")), " my ");
		node.AddNode(new DummyNode("", () => new DummyConstraintResult(Outcome.Failure, "baz", "-")), " is ");
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
			() => new DummyConstraintResult(Outcome.Success, "foo")));
		node.AddNode(new DummyNode("",
			() => new DummyConstraintResult(Outcome.Success, "bar", null, FurtherProcessingStrategy.IgnoreCompletely)));
		node.AddNode(new DummyNode("",
			() => new DummyConstraintResult(Outcome.Failure, "baz", "-")));
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
