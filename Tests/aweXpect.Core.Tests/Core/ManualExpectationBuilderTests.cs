using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core;

public class ManualExpectationBuilderTests
{
	[Fact]
	public async Task AddAsyncContextValueConstraint_ShouldAllowGettingExpectationBuilder()
	{
		ManualExpectationBuilder<int> sut = new();
		ExpectationBuilder? expectationBuilder = null;
		sut.AddConstraint((e, _, _) =>
		{
			expectationBuilder = e;
			return new DummyAsyncContextConstraint<int>(_
				=> Task.FromResult<ConstraintResult>(new ConstraintResult.Success("")));
		});

		await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(expectationBuilder).IsSameAs(sut);
	}

	[Fact]
	public async Task AddAsyncValueConstraint_ShouldAllowGettingExpectationBuilder()
	{
		ManualExpectationBuilder<int> sut = new();
		ExpectationBuilder? expectationBuilder = null;
		sut.AddConstraint((e, _, _) =>
		{
			expectationBuilder = e;
			return new DummyAsyncConstraint<int>(_
				=> Task.FromResult<ConstraintResult>(new ConstraintResult.Success("")));
		});

		await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(expectationBuilder).IsSameAs(sut);
	}

	[Fact]
	public async Task AddContextValueConstraint_ShouldAllowGettingExpectationBuilder()
	{
		ManualExpectationBuilder<int> sut = new();
		ExpectationBuilder? expectationBuilder = null;
		sut.AddConstraint((e, _, _) =>
		{
			expectationBuilder = e;
			return new DummyContextConstraint<int>(_ => new ConstraintResult.Success(""));
		});

		await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(expectationBuilder).IsSameAs(sut);
	}

	[Fact]
	public async Task AddValueConstraint_ShouldAllowGettingExpectationBuilder()
	{
		ManualExpectationBuilder<int> sut = new();
		ExpectationBuilder? expectationBuilder = null;
		sut.AddConstraint((e, _, _) =>
		{
			expectationBuilder = e;
			return new DummyConstraint("");
		});

		await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(expectationBuilder).IsSameAs(sut);
	}


	[Fact]
	public async Task IsMet_ShouldThrowNotSupportedException()
	{
		ManualExpectationBuilder<int> sut = new();
		sut.AddConstraint((_, _) => new DummyConstraint<int>(_ => true));

		async Task Act() => await sut.IsMet(
			new ExpectationNode(), null!, new TimeSystemMock(), null, CancellationToken.None);

		await That(Act).Throws<NotSupportedException>()
			.WithMessage("Use IsMetBy for ManualExpectationBuilder!");
	}

	[Fact]
	public async Task IsMetBy_FailingConstraint_ShouldReturnFailure()
	{
		ManualExpectationBuilder<int> sut = new();
		sut.AddConstraint((_, _) => new DummyConstraint<int>(_ => false));

		ConstraintResult result = await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(result).Is<ConstraintResult.Failure>();
	}

	[Fact]
	public async Task IsMetBy_SucceedingConstraint_ShouldReturnSuccess()
	{
		ManualExpectationBuilder<int> sut = new();
		sut.AddConstraint((_, _) => new DummyConstraint<int>(_ => true));

		ConstraintResult result = await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(result).Is<ConstraintResult.Success>();
	}

	[Fact]
	public async Task Subject_ShouldBeEmpty()
	{
		ManualExpectationBuilder<int> sut = new();

		await That(sut.Subject).IsEmpty();
	}

	private class DummyConstraint<T>(Func<T, bool> predicate) : IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (predicate(actual))
			{
				return new ConstraintResult.Success<T>(actual, "");
			}

			return new ConstraintResult.Failure<T>(actual, "", "");
		}
	}
}
