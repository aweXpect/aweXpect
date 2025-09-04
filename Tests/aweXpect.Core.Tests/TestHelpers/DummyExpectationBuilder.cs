using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Nodes;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Core.Tests.TestHelpers;

public class DummyExpectationBuilder() : ExpectationBuilder("")
{
	internal override Task<ConstraintResult> IsMet(Node rootNode, EvaluationContext.EvaluationContext context,
		ITimeSystem timeSystem, TimeSpan? timeout,
		CancellationToken cancellationToken)
		=> Task.FromResult<ConstraintResult>(new DummyConstraintResult(Outcome.Success));
}
