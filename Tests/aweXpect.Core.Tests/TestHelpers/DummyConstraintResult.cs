using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.TestHelpers;

public class DummyConstraintResult : ConstraintResult
{
	public DummyConstraintResult(Outcome outcome,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		: base(outcome, furtherProcessingStrategy)
	{
	}
}
