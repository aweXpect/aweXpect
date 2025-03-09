using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.TestHelpers;

internal class DummyValueConstraint<T>(Func<T, ConstraintResult> callback) : IValueConstraint<T>
{
	public ConstraintResult IsMetBy(T actual) => callback(actual);

	public void AppendExpectation(StringBuilder stringBuilder, string? indentation = null) { }
}
