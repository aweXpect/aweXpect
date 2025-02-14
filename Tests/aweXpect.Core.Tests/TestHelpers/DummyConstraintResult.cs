using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.TestHelpers;

public class DummyConstraintResult : ConstraintResult
{
	public DummyConstraintResult(Outcome outcome,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		: base(outcome, furtherProcessingStrategy)
	{
	}

	public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
	{
	}

	public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
	{
	}
}
