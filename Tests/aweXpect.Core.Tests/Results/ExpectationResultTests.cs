﻿using System.Collections.Generic;
using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;
using aweXpect.Core.TimeSystem;
using aweXpect.Results;

namespace aweXpect.Core.Tests.Results;

public class ExpectationResultTests
{
	[Fact]
	public async Task GetResult_ShouldIncrementIndexAndHaveCorrectSubjectLine()
	{
		ExpectationResult sut = new(new MyExpectationBuilder("my-subject"));

		Expectation.Result result = await sut.GetResult(3, new Dictionary<int, Outcome>());

		await That(result.Index).IsEqualTo(4);
		await That(result.SubjectLine).IsEqualTo(" [04] Expected that my-subject");
	}

	[Fact]
	public async Task IsMet_InvalidType_ShouldThrowFailException()
	{
		MyExpectationBuilder myBuilder =
			new("my-subject", () => new DummyConstraintResult<string>(Outcome.Success, "foo", "SUCCESS"));
		ExpectationResult<int> sut = new(myBuilder);

		async Task Act() => await sut;

		await That(Act).Throws<FailException>()
			.WithMessage("""
			             The value in DummyConstraintResult<string> did not match expected type int.
			             """);
	}

	[Fact]
	public async Task WithCancellation_ShouldForwardTokenToExpectationBuilder()
	{
		MyExpectationBuilder myBuilder = new("my-subject");
		CancellationTokenSource cts = new();
		CancellationToken token = cts.Token;
		ExpectationResult<int> sut = new(myBuilder);

		_ = sut.WithCancellation(token);

		CancellationToken? receivedToken = await myBuilder.GetRegisteredCancellationToken();
		await That(receivedToken).IsEqualTo(token);
	}

	private sealed class MyExpectationBuilder(string subject, Func<ConstraintResult>? resultBuilder = null)
		: ExpectationBuilder(subject)
	{
		private readonly Func<ConstraintResult> _resultBuilder = resultBuilder ?? DefaultResultBuilder;

		private CancellationToken? _cancellationToken;

		private static ConstraintResult DefaultResultBuilder() => new DummyConstraintResult(Outcome.Success, "SUCCESS");

		public async Task<CancellationToken?> GetRegisteredCancellationToken()
		{
			await IsMet();
			return _cancellationToken;
		}

		internal override Task<ConstraintResult> IsMet(Node rootNode,
			EvaluationContext.EvaluationContext context,
			ITimeSystem timeSystem,
			TimeSpan? timeout,
			CancellationToken cancellationToken)
		{
			_cancellationToken = cancellationToken;
			ConstraintResult result = _resultBuilder();
			return Task.FromResult(result);
		}
	}
}
