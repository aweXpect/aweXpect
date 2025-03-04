using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.TestHelpers;

public class DummyConstraintResult : ConstraintResult
{
	private readonly string? _expectationText;
	private readonly string? _failureText;

	public DummyConstraintResult(Outcome outcome,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue,
		string? expectationText = null,
		string? failureText = null)
		: base(furtherProcessingStrategy)
	{
		Outcome = outcome;
		_expectationText = expectationText;
		_failureText = failureText;
	}

	public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
	{
		if (_expectationText != null)
		{
			stringBuilder.Append(_expectationText);
		}
	}

	public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
	{
		if (_failureText != null)
		{
			stringBuilder.Append(_failureText);
		}
	}

	public override ConstraintResult Negate()
	{
		Outcome = Outcome switch
		{
			Outcome.Failure => Outcome.Success,
			Outcome.Success => Outcome.Failure,
			_ => Outcome,
		};
		return this;
	}
}
