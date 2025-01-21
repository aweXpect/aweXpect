using System.Threading;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.TestHelpers;

internal class DummyAsyncConstraint<T>(Func<T, Task<ConstraintResult>> callback) : IAsyncConstraint<T>
{
	public Task<ConstraintResult> IsMetBy(T actual, CancellationToken cancellationToken) => callback(actual);
}
