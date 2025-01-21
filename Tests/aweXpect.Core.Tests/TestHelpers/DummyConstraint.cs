using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.TestHelpers;

internal class DummyConstraint(string expectationText) : IValueConstraint<int>
{
	public ConstraintResult IsMetBy(int actual) => new ConstraintResult.Success(expectationText);
	public override string ToString() => expectationText;
}
