using System.Threading;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.Core;

public class ManualExpectationBuilderTests
{
	[Fact]
	public async Task IsMetBy_FailingConstraint_ShouldReturnFailure()
	{
		ManualExpectationBuilder<int> sut = new();
		sut.AddConstraint(_ => new DummyConstraint<int>(_ => false));

		ConstraintResult result = await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(result).Is<ConstraintResult.Failure>();
	}

	[Fact]
	public async Task IsMetBy_SucceedingConstraint_ShouldReturnSuccess()
	{
		ManualExpectationBuilder<int> sut = new();
		sut.AddConstraint(_ => new DummyConstraint<int>(_ => true));

		ConstraintResult result = await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(result).Is<ConstraintResult.Success>();
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
