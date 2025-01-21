using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;

namespace aweXpect.Core.Tests.TestHelpers;

internal class DummyAsyncContextConstraint<T>(Func<T, Task<ConstraintResult>> callback) : IAsyncContextConstraint<T>
{
	public Task<ConstraintResult> IsMetBy(T actual, IEvaluationContext context, CancellationToken cancellationToken)
		=> callback(actual);
}
