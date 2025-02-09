using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Nodes;

public class ExpectationNodeTests
{
	[Fact]
	public async Task AddConstraint_Twice_ShouldThrowInvalidOperationException()
	{
		ExpectationNode node = new();
		node.AddConstraint(new DummyConstraint("foo"));

		void Act() => node.AddConstraint(new DummyConstraint("bar"));

		await That(Act).Throws<InvalidOperationException>()
			.WithMessage(
				"You have to specify how to combine the expectations! Use `And()` or `Or()` in between adding expectations.");
	}

	[Fact]
	public async Task AddConstraint_WithMapping_ShouldForwardToInnerNode()
	{
		ExpectationNode node = new();
		node.AddConstraint(new DummyConstraint("foo"));
		node.AddMapping(MemberAccessor<string, int>.FromFunc(s => s.Length, " with length "));

		node.AddConstraint(new DummyConstraint("bar"));

		await That(node.ToString()).IsEqualTo("foo with length bar");
	}

	[Fact]
	public async Task AddNode_ShouldThrowNotSupportedException()
	{
		ExpectationNode node = new();

		void Act() => node.AddNode(new DummyNode("foo"));

		await That(Act).Throws<NotSupportedException>()
			.WithMessage("Don't specify the inner node for Expectation nodes directly. Use AddMapping() instead!");
	}

	[Fact]
	public async Task IsMetBy_WhenAsyncConstraintReturns_ShouldApplyBecauseReason()
	{
		ExpectationNode node = new();
		node.AddConstraint(new DummyAsyncConstraint<int>(_
			=> Task.FromResult<ConstraintResult>(new ConstraintResult.Success("foo"))));
		node.SetReason(new BecauseReason("my reason"));

		ConstraintResult result = await node.IsMetBy(44, null!, CancellationToken.None);

		await That(result.ExpectationText).IsEqualTo("foo, because my reason");
	}

	[Fact]
	public async Task IsMetBy_WhenAsyncConstraintThrowsException_ShouldThrowInvalidOperationException()
	{
		MyException exception = new();
		ExpectationNode node = new();
		node.AddConstraint(new DummyAsyncConstraint<int>(_ => Task.FromException<ConstraintResult>(exception)));

		async Task Act() =>
			await node.IsMetBy(44, null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithInner<MyException>(x => x.HasMessage(exception.Message)).And
			.WithMessage("""
			             Error evaluating DummyAsyncConstraint<int> constraint with value 44: IsMetBy_WhenAsyncConstraintThrowsException_ShouldThrowInvalidOperationException
			             """);
	}

	[Fact]
	public async Task IsMetBy_WhenAsyncContextConstraintReturns_ShouldApplyBecauseReason()
	{
		ExpectationNode node = new();
		node.AddConstraint(new DummyAsyncContextConstraint<int>(_
			=> Task.FromResult<ConstraintResult>(new ConstraintResult.Success("foo"))));
		node.SetReason(new BecauseReason("my reason"));

		ConstraintResult result = await node.IsMetBy(44, null!, CancellationToken.None);

		await That(result.ExpectationText).IsEqualTo("foo, because my reason");
	}

	[Fact]
	public async Task IsMetBy_WhenAsyncContextConstraintThrowsException_ShouldThrowInvalidOperationException()
	{
		MyException exception = new();
		ExpectationNode node = new();
		node.AddConstraint(new DummyAsyncContextConstraint<int>(_ => Task.FromException<ConstraintResult>(exception)));

		async Task Act() =>
			await node.IsMetBy(45, null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithInner<MyException>(x => x.HasMessage(exception.Message)).And
			.WithMessage("""
			             Error evaluating DummyAsyncContextConstraint<int> constraint with value 45: IsMetBy_WhenAsyncContextConstraintThrowsException_ShouldThrowInvalidOperationException
			             """);
	}

	[Fact]
	public async Task IsMetBy_WhenConstraintAndInnerFailAndWhenBothFailureMessagesAreIdentical_ShouldOnlyPrintOnce()
	{
		ExpectationNode node = new();
		node.AddConstraint(
			new DummyValueConstraint<int>(v => new ConstraintResult.Failure<int>(v, "foo", "same failure")));
		node.AddMapping(MemberAccessor<int, int>.FromFunc(s => s, " with mapping "));
		node.AddConstraint(new DummyValueConstraint<int>(v
			=> new ConstraintResult.Failure<int>(2 * v, "bar", "same failure")));

		ConstraintResult result = await node.IsMetBy(42, null!, CancellationToken.None);

		await That(result).Is<ConstraintResult.Failure<int>>()
			.Whose(p => p.Value, v => v.IsEqualTo(42))
			.AndWhose(p => p.ExpectationText, e => e.IsEqualTo("foo with mapping bar"))
			.AndWhose(p => p.ResultText, r => r.IsEqualTo("same failure"));
	}

	[Fact]
	public async Task IsMetBy_WhenConstraintAndInnerFail_ShouldCombineFailureMessage()
	{
		ExpectationNode node = new();
		node.AddConstraint(
			new DummyValueConstraint<int>(v => new ConstraintResult.Failure<int>(v, "foo", "outer failure")));
		node.AddMapping(MemberAccessor<int, int>.FromFunc(s => s, " with mapping "));
		node.AddConstraint(new DummyValueConstraint<int>(v
			=> new ConstraintResult.Failure<int>(2 * v, "bar", "inner failure")));

		ConstraintResult result = await node.IsMetBy(42, null!, CancellationToken.None);

		await That(result).Is<ConstraintResult.Failure<int>>()
			.Whose(p => p.Value, v => v.IsEqualTo(42))
			.AndWhose(p => p.ExpectationText, e => e.IsEqualTo("foo with mapping bar"))
			.AndWhose(p => p.ResultText, r => r.IsEqualTo("outer failure and inner failure"));
	}

	[Fact]
	public async Task IsMetBy_WhenContextConstraintReturns_ShouldApplyBecauseReason()
	{
		ExpectationNode node = new();
		node.AddConstraint(new DummyContextConstraint<int>(_ => new ConstraintResult.Success("foo")));
		node.SetReason(new BecauseReason("my reason"));

		ConstraintResult result = await node.IsMetBy(44, null!, CancellationToken.None);

		await That(result.ExpectationText).IsEqualTo("foo, because my reason");
	}

	[Fact]
	public async Task IsMetBy_WhenContextConstraintThrowsException_ShouldThrowInvalidOperationException()
	{
		MyException exception = new();
		ExpectationNode node = new();
		node.AddConstraint(new DummyContextConstraint<int>(_ => throw exception));

		async Task Act() =>
			await node.IsMetBy(43, null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithInner<MyException>(x => x.HasMessage(exception.Message)).And
			.WithMessage("""
			             Error evaluating DummyContextConstraint<int> constraint with value 43: IsMetBy_WhenContextConstraintThrowsException_ShouldThrowInvalidOperationException
			             """);
	}

	[Fact]
	public async Task IsMetBy_WhenValueConstraintReturns_ShouldApplyBecauseReason()
	{
		ExpectationNode node = new();
		node.AddConstraint(new DummyValueConstraint<int>(_ => new ConstraintResult.Success("foo")));
		node.SetReason(new BecauseReason("my reason"));

		ConstraintResult result = await node.IsMetBy(44, null!, CancellationToken.None);

		await That(result.ExpectationText).IsEqualTo("foo, because my reason");
	}

	[Fact]
	public async Task IsMetBy_WhenValueConstraintThrowsException_ShouldThrowInvalidOperationException()
	{
		MyException exception = new();
		ExpectationNode node = new();
		node.AddConstraint(new DummyValueConstraint<string>(_ => throw exception));

		async Task Act() =>
			await node.IsMetBy("42", null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithInner<MyException>(x => x.HasMessage(exception.Message)).And
			.WithMessage("""
			             Error evaluating DummyValueConstraint<string> constraint with value "42": IsMetBy_WhenValueConstraintThrowsException_ShouldThrowInvalidOperationException
			             """);
	}

	[Fact]
	public async Task IsMetBy_WithUnsupportedConstraint_ShouldThrowInvalidOperationException()
	{
		ExpectationNode node = new();
		node.AddConstraint(new UnsupportedConstraint());

		async Task Act() =>
			await node.IsMetBy("42", null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithMessage("The expectation node does not support string with value \"42\"");
	}

	[Fact]
	public async Task ToString_Empty_ShouldReturnEmptyText()
	{
		ExpectationNode node = new();

		string? result = node.ToString();

		await That(result).IsEqualTo("<empty>");
	}

	[Fact]
	public async Task ToString_WithConstraint_ShouldReturnConstraint()
	{
		ExpectationNode node = new();

		node.AddConstraint(new DummyConstraint("foo"));

		string? result = node.ToString();

		await That(result).IsEqualTo("foo");
	}

	[Fact]
	public async Task ToString_WithConstraintAndWithMapping_ShouldReturnBoth()
	{
		ExpectationNode node = new();

		node.AddConstraint(new DummyConstraint("foo"));
		node.AddMapping(MemberAccessor<string, int>.FromFunc(s => s.Length, "with length: "));

		string? result = node.ToString();

		await That(result).IsEqualTo("foowith length: <empty>");
	}

	[Fact]
	public async Task ToString_WithMapping_ShouldReturnMapping()
	{
		ExpectationNode node = new();

		node.AddMapping(MemberAccessor<string, int>.FromFunc(s => s.Length, "with length: "));

		string? result = node.ToString();

		await That(result).IsEqualTo("with length: <empty>");
	}

	private class UnsupportedConstraint : IConstraint;
}
