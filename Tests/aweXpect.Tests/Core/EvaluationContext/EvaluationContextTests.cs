using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Results;

namespace Testably.Expectations.Tests.Core.EvaluationContext;

public class EvaluationContextTests
{
	[Fact]
	public async Task CanStoreMultipleValueInParallel()
	{
		IEvaluationContext context = await GetSut();

		context.Store("foo", "foo-value");
		context.Store("bar", "bar-value");

		context.TryReceive("foo", out string? fooResult);
		await That(fooResult).Should().Be("foo-value");
		context.TryReceive("bar", out string? barResult);
		await That(barResult).Should().Be("bar-value");
	}

	[Fact]
	public async Task WhenNotStoredPreviously_ShouldReturnFalse()
	{
		IEvaluationContext context = await GetSut();

		bool result = context.TryReceive("foo", out string? fooResult);
		await That(result).Should().BeFalse();
		await That(fooResult).Should().BeNull();
	}

	[Fact]
	public async Task WhenTypeDoesNotMatch_ShouldReturnFalse()
	{
		IEvaluationContext context = await GetSut();

		context.Store("foo", 42);

		bool result = context.TryReceive("foo", out string? fooResult);
		await That(result).Should().BeFalse();
		await That(fooResult).Should().BeNull();
	}

	[Fact]
	public async Task WhenTypeMatches_ShouldReturnTrue()
	{
		IEvaluationContext context = await GetSut();

		context.Store("foo", "bar");

		bool result = context.TryReceive("foo", out string? fooResult);
		await That(result).Should().BeTrue();
		await That(fooResult).Should().Be("bar");
	}

	#region Helpers

	private static async Task<IEvaluationContext> GetSut()
	{
		IThat<bool> that = That(true).Should();
		MyContextConstraint constraint = new();
		await new AndOrResult<bool, IThat<bool>>(
			that.ExpectationBuilder
				.AddConstraint(_ => constraint),
			that);

		return constraint.Context!;
	}

	#endregion

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
