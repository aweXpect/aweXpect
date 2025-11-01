using System.Text;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;

namespace aweXpect.Core.Tests.TestHelpers;

internal class DummyContextConstraint<T>(Func<T, ConstraintResult> callback) : IContextConstraint<T>
{
	public ConstraintResult IsMetBy(T actual, IEvaluationContext context) => callback(actual);

	public void AppendExpectation(StringBuilder stringBuilder, string? indentation = null) { }
}
