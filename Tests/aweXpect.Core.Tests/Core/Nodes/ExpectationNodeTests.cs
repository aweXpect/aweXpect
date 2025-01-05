using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Nodes;

public class ExpectationNodeTests
{
	[Fact]
	public async Task WhenAsyncConstraintThrowsException_ShouldThrowInvalidOperationException()
	{
		MyException exception = new();
		ExpectationNode node = new();
		node.AddConstraint(new DummyAsyncConstraint<int>(_ => Task.FromException<ConstraintResult>(exception)));

		async Task Act() =>
			await node.IsMetBy(44, null!, CancellationToken.None);

		await That(Act).Should().Throw<InvalidOperationException>()
			.WithMessage("""
			             Error evaluating DummyAsyncConstraint<int> constraint with value 44: WhenAsyncConstraintThrowsException_ShouldThrowInvalidOperationException
			             """).And
			.WithInner<MyException>(x => x.HaveMessage(exception.Message));
	}

	[Fact]
	public async Task WhenAsyncContextConstraintThrowsException_ShouldThrowInvalidOperationException()
	{
		MyException exception = new();
		ExpectationNode node = new();
		node.AddConstraint(new DummyAsyncContextConstraint<int>(_ => Task.FromException<ConstraintResult>(exception)));

		async Task Act() =>
			await node.IsMetBy(45, null!, CancellationToken.None);

		await That(Act).Should().Throw<InvalidOperationException>()
			.WithMessage("""
			             Error evaluating DummyAsyncContextConstraint<int> constraint with value 45: WhenAsyncContextConstraintThrowsException_ShouldThrowInvalidOperationException
			             """).And
			.WithInner<MyException>(x => x.HaveMessage(exception.Message));
	}

	[Fact]
	public async Task WhenContextConstraintThrowsException_ShouldThrowInvalidOperationException()
	{
		MyException exception = new();
		ExpectationNode node = new();
		node.AddConstraint(new DummyContextConstraint<int>(_ => throw exception));

		async Task Act() =>
			await node.IsMetBy(43, null!, CancellationToken.None);

		await That(Act).Should().Throw<InvalidOperationException>()
			.WithMessage("""
			             Error evaluating DummyContextConstraint<int> constraint with value 43: WhenContextConstraintThrowsException_ShouldThrowInvalidOperationException
			             """).And
			.WithInner<MyException>(x => x.HaveMessage(exception.Message));
	}

	[Fact]
	public async Task WhenValueConstraintThrowsException_ShouldThrowInvalidOperationException()
	{
		MyException exception = new();
		ExpectationNode node = new();
		node.AddConstraint(new DummyValueConstraint<string>(_ => throw exception));

		async Task Act() =>
			await node.IsMetBy("42", null!, CancellationToken.None);

		await That(Act).Should().Throw<InvalidOperationException>()
			.WithMessage("""
			             Error evaluating DummyValueConstraint<string> constraint with value "42": WhenValueConstraintThrowsException_ShouldThrowInvalidOperationException
			             """).And
			.WithInner<MyException>(x => x.HaveMessage(exception.Message));
	}

	private class DummyValueConstraint<T>(Func<T, ConstraintResult> callback) : IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual) => callback(actual);
	}

	private class DummyContextConstraint<T>(Func<T, ConstraintResult> callback) : IContextConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual, IEvaluationContext context) => callback(actual);
	}

	private class DummyAsyncConstraint<T>(Func<T, Task<ConstraintResult>> callback) : IAsyncConstraint<T>
	{
		public Task<ConstraintResult> IsMetBy(T actual, CancellationToken cancellationToken) => callback(actual);
	}

	private class DummyAsyncContextConstraint<T>(Func<T, Task<ConstraintResult>> callback) : IAsyncContextConstraint<T>
	{
		public Task<ConstraintResult> IsMetBy(T actual, IEvaluationContext context, CancellationToken cancellationToken)
			=> callback(actual);
	}
}
