using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect.Core.Tests;

public class ExpectTests
{
	private class MyExpectation(Expectation.Result result) : Expectation
	{
		internal override Task<Result> GetResult(int index) => Task.FromResult(result);
	}

	[Fact]
	public async Task Context_ShouldBeIncludedInMessage()
	{
		Expectation.Result result = new(1, "foo", new ConstraintResult.Failure("expectation", "result")
			.WithContext("context-title", "contest-content"));

		async Task Act()
			=> await ThatAll(new MyExpectation(result));

		await That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected all of the following to succeed:
			             foo expectation
			             but
			              [01] result

			             context-title:
			             contest-content
			             """);
	}

	[Fact]
	public async Task Context_Multiple_ShouldBeIncludedInMessage()
	{
		Expectation.Result result = new(1, "foo", new ConstraintResult.Failure("expectation", "result")
			.WithContexts(new ConstraintResult.Context("t1", "c1"),
				new ConstraintResult.Context("t2", "c2"),
				new ConstraintResult.Context("t3", "c3")));

		async Task Act()
			=> await ThatAll(new MyExpectation(result));

		await That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected all of the following to succeed:
			             foo expectation
			             but
			              [01] result

			             t1:
			             c1

			             t2:
			             c2

			             t3:
			             c3
			             """);
	}

	[Fact]
	public async Task Context_FromSuccess_ShouldNotBeIncludedInMessage()
	{
		Expectation.Result result1 = new(1, "foo1", new ConstraintResult.Failure("expectation1", "result1")
			.WithContext("context-title1", "contest-content1"));
		Expectation.Result result2 = new(1, "foo2", new ConstraintResult.Success("expectation2")
			.WithContext("context-title2", "contest-content2"));

		async Task Act()
			=> await ThatAll(new MyExpectation(result1), new MyExpectation(result2));

		await That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected all of the following to succeed:
			             foo1 expectation1
			             foo2 expectation2
			             but
			              [01] result1

			             context-title1:
			             contest-content1
			             """);
	}

	[Fact]
	public async Task ShouldSupportCollectionExpressionsAsSubject()
	{
		async Task Act()
			=> await That([1, 2, 3]).IsInAscendingOrder();

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task ShouldSupportTaskAsSubject()
	{
		Task<int> sut = Task.FromResult(42);

		async Task Act()
			=> await That(sut).IsGreaterThan(41);

		await That(Act).DoesNotThrow();
	}

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportValueTaskAsSubject()
	{
		ValueTask<int> sut = ValueTask.FromResult(42);

		async Task Act()
			=> await That(sut).IsGreaterThan(41);

		await That(Act).DoesNotThrow();
	}
#endif
}
