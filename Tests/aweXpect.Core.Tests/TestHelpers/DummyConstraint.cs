using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.TestHelpers;

internal class DummyConstraint(string expectationText, Func<ConstraintResult>? constraintResult = null)
	: IValueConstraint<int>
{
	public ConstraintResult IsMetBy(int actual)
		=> constraintResult == null
			? new DummyConstraintResult(Outcome.Success, expectationText)
			: constraintResult();

	public void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		=> stringBuilder.Append(expectationText);
}
