﻿using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Results;

namespace aweXpect.Core.Tests.Core.EvaluationContext;

public class EvaluationContextTests
{
	[Fact]
	public async Task CanStoreMultipleValueInParallel()
	{
		IEvaluationContext context = await GetSut();

		context.Store("foo", "foo-value");
		context.Store("bar", "bar-value");

		context.TryReceive("foo", out string? fooResult);
		await That(fooResult).Is("foo-value");
		context.TryReceive("bar", out string? barResult);
		await That(barResult).Is("bar-value");
	}

	[Fact]
	public async Task WhenNotStoredPreviously_ShouldReturnFalse()
	{
		IEvaluationContext context = await GetSut();

		bool result = context.TryReceive("foo", out string? fooResult);
		await That(result).IsFalse();
		await That(fooResult).IsNull();
	}

	[Fact]
	public async Task WhenTypeDoesNotMatch_ShouldReturnFalse()
	{
		IEvaluationContext context = await GetSut();

		context.Store("foo", 42);

		bool result = context.TryReceive("foo", out string? fooResult);
		await That(result).IsFalse();
		await That(fooResult).IsNull();
	}

	[Fact]
	public async Task WhenTypeMatches_ShouldReturnTrue()
	{
		IEvaluationContext context = await GetSut();

		context.Store("foo", "bar");

		bool result = context.TryReceive("foo", out string? fooResult);
		await That(result).IsTrue();
		await That(fooResult).Is("bar");
	}

	private static async Task<IEvaluationContext> GetSut()
	{
		IThatVerb<bool> that = (IThatVerb<bool>)That(true);
		MyContextConstraint constraint = new();
		await new AndOrResult<bool, IThatVerb<bool>>(
			that.ExpectationBuilder
				.AddConstraint(_ => constraint),
			that);

		return constraint.Context!;
	}

	private sealed class MyContextConstraint : IContextConstraint<bool>
	{
		public IEvaluationContext? Context { get; private set; }

		#region IContextConstraint<bool> Members

		/// <inheritdoc />
		public ConstraintResult IsMetBy(bool actual, IEvaluationContext context)
		{
			Context = context;
			return new ConstraintResult.Success<bool>(actual, "");
		}

		#endregion
	}
}
