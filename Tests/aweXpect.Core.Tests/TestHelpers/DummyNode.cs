using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;

namespace aweXpect.Core.Tests.TestHelpers;
internal class DummyNode(string name) : Node
{
	public override string ToString()
		=> name;

	public override void AddConstraint(IConstraint constraint)
		=> throw new NotSupportedException();

	public override Node? AddMapping<TValue, TTarget>(
		MemberAccessor<TValue, TTarget?> memberAccessor,
		Func<MemberAccessor, string, string>? expectationTextGenerator = null)
		where TTarget : default
		=> throw new NotSupportedException();

	public override void AddNode(Node node, string? separator = null)
		=> throw new NotSupportedException();

	public override Task<ConstraintResult> IsMetBy<TValue>(
		TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken)
		where TValue : default
		=> throw new NotSupportedException();

	public override void SetReason(BecauseReason becauseReason)
		=> throw new NotSupportedException();
}
