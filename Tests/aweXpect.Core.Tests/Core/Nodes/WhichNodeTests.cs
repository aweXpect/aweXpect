using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class WhichNodeTests
{
	[Fact]
	public async Task AddConstraint_WithoutInnerNode_ShouldNotThrow()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string?>("1", ""));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);

		void Act() => whichNode.AddConstraint(
			new DummyConstraint("c2", () => new ConstraintResult.Success<int>(4, "e2")));

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task AddMapping_WithoutInnerNode_ShouldNotThrow()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string?>("1", ""));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);

		void Act() => whichNode.AddMapping(MemberAccessor<string, int>.FromExpression(s => s.Length));

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task GetResult_WhenBothFailed_ShouldUseOnlyFirst()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Failure("1", "r1"));
		DummyNode node2 = new("", () => new ConstraintResult.Failure("2", "r2"));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(node2);
		ConstraintResult constraintResult = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(constraintResult.GetResultText()).IsEqualTo("r1");
	}

	[Fact]
	public async Task GetResult_WhenBothHaveSameFailureText_ShouldOnlyIncludeOnce()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Failure("1", "same result"));
		DummyNode node2 = new("", () => new ConstraintResult.Failure("2", "same result"));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(node2);
		ConstraintResult constraintResult = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(constraintResult.GetResultText()).IsEqualTo("same result");
	}

	[Fact]
	public async Task GetResult_WithIgnoreResultFurtherProcessingStrategy_ShouldOnlyIncludeFirstFailure()
	{
		DummyNode node1 = new("",
			() => new ConstraintResult.Failure("1", "r1", FurtherProcessingStrategy.IgnoreResult));
		DummyNode node2 = new("", () => new ConstraintResult.Failure("2", "r2"));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(node2);
		ConstraintResult constraintResult = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(constraintResult.GetResultText()).IsEqualTo("r1");
	}

	[Fact]
	public async Task IsMetBy_EmptyExpectationNode_ShouldThrowInvalidOperationException()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string?>("1", ""));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(new ExpectationNode());
		Task<ConstraintResult> Act() => whichNode.IsMetBy("foo", null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithMessage("The expectation node does not support int with value 3");
	}

	[Fact]
	public async Task IsMetBy_ExpectationNodeWithConstraint_ShouldApplyConstraint()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string?>("1", "e1"));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(new ExpectationNode());
		whichNode.AddConstraint(new DummyConstraint("c2", () => new ConstraintResult.Success<int>(4, "e2")));
		StringBuilder sb = new();

		ConstraintResult result = await whichNode.IsMetBy("foo", null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(result.Outcome).IsEqualTo(Outcome.Success);
		await That(sb.ToString()).IsEqualTo("e1e2");
	}

	[Fact]
	public async Task IsMetBy_WhenTypeDoesNotMatch_ShouldThrowInvalidOperationException()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string?>("1", ""));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(new ExpectationNode());
		whichNode.AddConstraint(new DummyConstraint("c2", () => new ConstraintResult.Success<int>(4, "e2")));
		StringBuilder sb = new();

		async Task Act()
			=> await whichNode.IsMetBy(DateTime.Now, null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithMessage("""
			             The member type for the actual value in the which node did not match.
			                  Found: DateTime
			               Expected: string
			             """);
	}

	[Fact]
	public async Task IsMetBy_WithoutInnerNode_ShouldThrowInvalidOperationException()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string?>("1", ""));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		Task<ConstraintResult> Act() => whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithMessage("No inner node specified for the which node.");
	}

	[Fact]
	public async Task IsMetBy_WithoutParent_ShouldUseNodeResult()
	{
		WhichNode<string, int> whichNode = new(null, s => s.Length);
		whichNode.AddNode(new ExpectationNode());
		whichNode.AddConstraint(new DummyConstraint("c2", () => new ConstraintResult.Success<int>(4, "e2")));
		StringBuilder sb = new();

		ConstraintResult result = await whichNode.IsMetBy("foo", null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(result.Outcome).IsEqualTo(Outcome.Success);
		await That(sb.ToString()).IsEqualTo("e2");
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
		WhichNode<string, int> whichNode = new(new DummyNode("", () => new DummyConstraintResult(node1)),
			s => s.Length);
		whichNode.AddNode(new ExpectationNode());
		whichNode.AddConstraint(new DummyConstraint("", () => new DummyConstraintResult(node2)));

		ConstraintResult result = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(result.Outcome).IsEqualTo(expectedOutcome);
	}

	[Fact]
	public async Task SetReason_ShouldBeForwardedToInnerNode()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string?>("1", "e1"));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(new ExpectationNode());
		whichNode.AddConstraint(new DummyConstraint("c2", () => new ConstraintResult.Success<int>(4, "e2")));
		whichNode.SetReason(new BecauseReason("bc"));
		StringBuilder sb = new();

		ConstraintResult result = await whichNode.IsMetBy("foo", null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(result.Outcome).IsEqualTo(Outcome.Success);
		await That(sb.ToString()).IsEqualTo("e1e2, because bc");
	}

	[Fact]
	public async Task TryGetValue_WhenLeftHasValue_ShouldReturnLeftValue()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string?>("1", ""));
		DummyNode node2 = new("", () => new ConstraintResult.Success<string?>("2", ""));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(node2);
		ConstraintResult constraintResult = await whichNode.IsMetBy("", null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out string? value);

		await That(result).IsTrue();
		await That(value).IsEqualTo("1");
	}

	[Fact]
	public async Task TryGetValue_WhenMemberAccessorHasCorrectValue_ShouldReturnMemberValue()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success<string>("1", ""));
		DummyNode node2 = new("", () => new ConstraintResult.Success<string>("2", ""));
		WhichNode<string, int> whichNode = new(node1, _ => 3);
		whichNode.AddNode(node2);
		ConstraintResult constraintResult = await whichNode.IsMetBy("", null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out int value);

		await That(result).IsTrue();
		await That(value).IsEqualTo(3);
	}

	[Fact]
	public async Task TryGetValue_WhenNoneHasValue_ShouldReturnFalse()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success(""));
		DummyNode node2 = new("", () => new ConstraintResult.Success(""));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(node2);
		ConstraintResult constraintResult = await whichNode.IsMetBy("", null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out string? value);

		await That(result).IsFalse();
		await That(value).IsNull();
	}

	[Fact]
	public async Task TryGetValue_WhenOnlyRightHasValue_ShouldReturnRightValue()
	{
		DummyNode node1 = new("", () => new ConstraintResult.Success(""));
		DummyNode node2 = new("", () => new ConstraintResult.Success<string>("2", ""));
		WhichNode<string, int> whichNode = new(node1, s => s.Length);
		whichNode.AddNode(node2);
		ConstraintResult constraintResult = await whichNode.IsMetBy("", null!, CancellationToken.None);

		bool result = constraintResult.TryGetValue(out string? value);

		await That(result).IsTrue();
		await That(value).IsEqualTo("2");
	}

	[Fact]
	public async Task WhenBothAreFailure_ShouldOnlyIncludeLeftResult()
	{
		WhichNode<string, int> whichNode = new(new DummyNode("",
			() => new ConstraintResult.Failure("foo", "r1")), _ => 3);
		whichNode.AddNode(new DummyNode("",
			() => new ConstraintResult.Failure("bar", "r2")));

		ConstraintResult result = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(result.GetResultText()).IsEqualTo("r1");
	}

	[Fact]
	public async Task WhenBothAreSuccess_ShouldHaveEmptyResultText()
	{
		WhichNode<string, int> whichNode = new(new DummyNode("",
			() => new ConstraintResult.Success("foo")), _ => 3);
		whichNode.AddNode(new DummyNode("",
			() => new ConstraintResult.Success("bar")));

		ConstraintResult result = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(result.GetResultText()).IsEmpty();
	}

	[Fact]
	public async Task WhenLeftIsFailureAndHasIgnoreResultFurtherProcessingStrategy_ShouldExcludeRightResultText()
	{
		WhichNode<string, int> whichNode = new(new DummyNode("",
			() => new ConstraintResult.Failure("foo", "r1", FurtherProcessingStrategy.IgnoreResult)), _ => 3);
		whichNode.AddNode(new DummyNode("",
			() => new ConstraintResult.Failure("bar", "r2", FurtherProcessingStrategy.IgnoreResult)));

		ConstraintResult result = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(result.GetResultText()).IsEqualTo("r1");
	}

	[Fact]
	public async Task WhenLeftIsSuccessAndHasIgnoreResultFurtherProcessingStrategy_ShouldStillIncludeRightResultText()
	{
		WhichNode<string, int> whichNode = new(new DummyNode("",
			() => new ConstraintResult.Success("foo", FurtherProcessingStrategy.IgnoreResult)), _ => 3);
		whichNode.AddNode(new DummyNode("",
			() => new ConstraintResult.Failure("bar", "r2", FurtherProcessingStrategy.IgnoreResult)));

		ConstraintResult result = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(result.GetResultText()).IsEqualTo("r2");
	}

	[Fact]
	public async Task WhenOnlyRightHasFailure_ShouldIncludeRightResultText()
	{
		WhichNode<string, int> whichNode = new(new DummyNode("",
			() => new ConstraintResult.Success("foo")), _ => 3);
		whichNode.AddNode(new DummyNode("",
			() => new ConstraintResult.Failure("bar", "r2")));

		ConstraintResult result = await whichNode.IsMetBy("", null!, CancellationToken.None);

		await That(result.GetResultText()).IsEqualTo("r2");
	}

	[Fact]
	public async Task WhichCreatesGoodMessage()
	{
		Dummy subject = new()
		{
			Inner = new Dummy.Nested
			{
				Id = 1,
			},
			Value = "foo",
		};

		async Task Act()
			=> await That(subject).Is<Dummy>()
				.Whose(p => p.Value, e => e.IsEqualTo("bar"));

		await That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected that subject
			             is type Dummy whose .Value is equal to "bar",
			             but .Value was "foo" which differs at index 0:
			                ↓ (actual)
			               "foo"
			               "bar"
			                ↑ (expected)
			             
			             Actual:
			             foo
			             """);
	}

	private sealed class Dummy
	{
		public Nested? Inner { get; set; }
		public string? Value { get; set; }

		public class Nested
		{
#pragma warning disable CS0649
			public int Field;
#pragma warning restore CS0649
			public int Id { get; set; }

			public int Method() => Id + 1;
		}
	}
}
